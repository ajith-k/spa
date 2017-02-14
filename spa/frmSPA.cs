using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemMonitor;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections;

namespace spa
{
    public partial class frmSPA : Form
    {
        private IntPtr hPdhLib;
        private Cell[][] _arrayTable;
        private readonly int _widthOfSpace = 3;
        private readonly String _spacer = "        ";
        public Analysis analyser;

        delegate void CtrAdd_Callback();
        delegate void EnumMachines_Callback();
        delegate void QkAnalysis_Callback();
        delegate void C2C_Callback();
        delegate void C4MSS_Callback();

        [DllImport("pdh.dll")]
        public static extern ulong PdhEnumMachines(string szDataSource,
                                                      byte[] mszMachineNameList,
                                                       out ulong pcchBufferLength
                                                  );

        [DllImport("kernel32", SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("Kernel32.dll", SetLastError=true)]
        static extern uint FormatMessage( uint dwFlags, IntPtr lpSource, 
           uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, 
           uint nSize, IntPtr pArguments);

        public void showExBox(Exception pdhex, string moreInfo)
        {
            int nLastError = Marshal.GetLastWin32Error();
            IntPtr lpMsgBuf = IntPtr.Zero;

            if (pdhex.Message.Contains("Exception from HRESULT"))
            {
                string strTemp = pdhex.Message.Substring(pdhex.Message.LastIndexOf("0x")+2,8);
                Int32.TryParse(strTemp
                    , System.Globalization.NumberStyles.HexNumber
                    , (IFormatProvider)null
                    , out nLastError);
                //MessageBox.Show(pdhex.Message + "\r\n" + strTemp + " : "  + nLastError.ToString(), "Found match");
            }

            if (hPdhLib == IntPtr.Zero)
                hPdhLib = LoadLibrary("pdh.dll");
            
            uint dwChars= FormatMessage( 
                0x00000100 //FORMAT_MESSAGE_ALLOCATE_BUFFER 
                | 0x00001000 //FORMAT_MESSAGE_FROM_SYSTEM 
                | 0x00000800 //FORMAT_MESSAGE_FROM_HMODULE
                | 0x00000200 //FORMAT_MESSAGE_IGNORE_INSERTS
                ,hPdhLib
                ,(uint)nLastError
                ,0 // Default language
                ,ref lpMsgBuf
                , 0
                , IntPtr.Zero
                );

            string sMessage = Marshal.PtrToStringAnsi(lpMsgBuf);
            
            MessageBox.Show(moreInfo
                        + "\n=========================================\n"
                        + (nLastError == 0 ? pdhex.Message : sMessage)
                        + (nLastError == 0 ? "" : "\nError Number : " + nLastError.ToString())
                        + "\n=========================================\n"
                        + (nLastError == 0 ? "" : "\nSource     : " + pdhex.Source)
                        + "\n\nCall Stack : \n" + pdhex.StackTrace
                    , "Error : " + pdhex.Message,MessageBoxButtons.OK,MessageBoxIcon.Error);
            
            lpMsgBuf = LocalFree(lpMsgBuf);

            if (pdhex.InnerException != null)
                showExBox(pdhex.InnerException,"Inner Exception");

        }


        public frmSPA()
        {
            InitializeComponent();
            this.hPdhLib = IntPtr.Zero;
            initAnalysisScenarios();
            sysmon.MonitorDuplicateInstances = true;
            sysmon.Highlight = true;
        }

        public frmSPA(string[] args)
        {
            InitializeComponent();
            initAnalysisScenarios();
            sysmon.MonitorDuplicateInstances = true;
            sysmon.Highlight = true;
            tbPFile.Text = args[1];
            tslblDoingWhat.Text = "Enumerating computer names in file...";
            if (System.IO.File.Exists(tbPFile.Lines[0]))
            {
                this.bgWrkr.RunWorkerAsync("Enum_Machines");
            }
            tslblDoingWhat.Text = "";

        }

        public void initAnalysisScenarios()
        {
            string tmpFile = Path.GetTempFileName();
            string strBaseScenarios = "<spa_settings><spa_scenarios><scenario name=\"CPU Overall\"><counter path=\"Processor(*)\\% Processor Time\"/><counter path=\"Processor(*)\\% Privileged Time\"/><counter path=\"Processor(*)\\% User Time\"/></scenario><scenario name=\"CPU : SQL* Processes\"><counter path=\"Process(*Total)\\% Processor Time\"/><counter path=\"Process(*Total)\\% Privileged Time\"/><counter path=\"Process(*Total)\\% User Time\"/><counter path=\"Process(sqlservr*)\\% Processor Time\"/><counter path=\"Process(sqlservr*)\\% Privileged Time\"/><counter path=\"Process(sqlservr*)\\% User Time\"/><counter path=\"Process(sqlservr*)\\ID Process\"/></scenario><scenario name=\"CPU : SQLServer Counters\"><counter path=\"*:SQL Statistics\\Batch Requests/sec\"/><counter path=\"*:SQL Statistics\\SQL Compilations/sec\"/><counter path=\"*:SQL Statistics\\SQL Re-Compilations/sec\"/><counter path=\"*:Access Methods\\Workfiles Created/sec\"/><counter path=\"*:Access Methods\\Worktables Created/sec\"/></scenario><scenario name=\"CPU : All Process % Processor Time Only\"> <counter path=\"Process(*)\\% Processor Time\"/> </scenario><scenario name=\"Memory Overall\"><counter path=\"Memory\\Available MBytes\"/><counter path=\"Memory\\Free System Page Table Entries\"/><counter path=\"Memory\\Pages/sec\"/><counter path=\"Memory\\System Cache Resident Bytes\"/><counter path=\"Memory\\Pool Paged Bytes\"/><counter path=\"Memory\\Pool Nonpaged Bytes\"/></scenario><scenario name=\"IO : Physical Disk\"><counter path=\"PhysicalDisk(*)\\Avg. Disk Queue Length\"/><counter path=\"PhysicalDisk(*)\\Avg. Disk sec/Transfer\"/><counter path=\"PhysicalDisk(*)\\Disk Bytes/sec\"/><counter path=\"PhysicalDisk(*)\\Split IO/sec\"/><counter path=\"PhysicalDisk(*)\\Disk Transfers/sec\"/></scenario><scenario name=\"IO : Logical Disk\"><counter path=\"LogicalDisk(*)\\Avg. Disk Queue Length\"/><counter path=\"LogicalDisk(*)\\Avg. Disk sec/Transfer\"/><counter path=\"LogicalDisk(*)\\Disk Bytes/sec\"/><counter path=\"LogicalDisk(*)\\Split IO/sec\"/><counter path=\"LogicalDisk(*)\\Disk Transfers/sec\"/></scenario><scenario name=\"Repl : Merge Counters\"><counter path=\"*:Replication Merge(*)\\Uploaded Changes/sec\"/><counter path=\"*:Replication Merge(*)\\Downloaded Changes/sec\"/><counter path=\"*:Replication Merge(*)\\Conflicts/sec\"/><counter path=\"*:Replication Agents(Merge)\\Running\"/></scenario><scenario name=\"Repl : Snapshot Counters\"><counter path=\"*:Replication Snapshot(*)\\Snapshot:Delivered Cmds/sec\"/><counter path=\"*:Replication Snapshot(*)\\Snapshot:Delivered Trans/sec\"/><counter path=\"*:Replication Agents(Snapshot)\\Running\"/></scenario><scenario name=\"Repl : Tran Counters\"><counter path=\"*:Replication Logreader(*)\\Logreader:Delivered Cmds/sec\"/><counter path=\"*:Replication Logreader(*)\\Logreader:Delivered Trans/sec\"/><counter path=\"*:Replication Logreader(*)\\Logreader:Delivery Latency\"/><counter path=\"*:Replication Dist.(*)\\Dist:Delivered Cmds/sec\"/><counter path=\"*:Replication Dist.(*)\\Dist:Delivered Trans/sec\"/><counter path=\"*:Replication Dist.(*)\\Dist:Delivery Latency\"/><counter path=\"*:Replication Agents(Logreader)\\Running\"/><counter path=\"*:Replication Agents(Distribution)\\Running\"/><counter path=\"*:Replication Agents(Queuereader)\\Running\"/></scenario></spa_scenarios></spa_settings>";
            try
            {
                if (File.Exists(Application.StartupPath + "\\spa.xml"))
                    analyser = new Analysis(Application.StartupPath + "\\spa.xml");
                else
                {
                    File.WriteAllText(tmpFile, strBaseScenarios);
                    analyser = new Analysis(tmpFile);
                    File.Delete(tmpFile);
                }
            }
            catch (Exception exAnalysis)
            {
                showExBox(exAnalysis, "Error parsing config file. Will continue with base counters");

                // Hack!! Repeat code from above for spa.xml not found. Need to continue even if the config file parse fails. 
                // Presumes analysis of base counter string will not fail. 640K of memory ought to be enough for everybody. Right?   
                File.WriteAllText(tmpFile, strBaseScenarios);
                analyser = new Analysis(tmpFile);
                File.Delete(tmpFile);

            }

            this.cmbCtrs.Items.Clear();
            cmbCtrs.Text = "None";
            cmbCtrs.Items.Add("None");
            cmbCtrs.Items.Add("Custom");
            foreach (Scenario scenario_item in this.analyser.Scenarios)
            {
                cmbCtrs.Items.Add(scenario_item.strScenario);
            }
        }
        
        private void CtrAdd()
        {
            if (this.btnCtrAdd.InvokeRequired)
            {
                CtrAdd_Callback cbk = new CtrAdd_Callback(CtrAdd);
                this.Invoke(cbk, null);
            }
            else
            {
                this.btnPFile.Enabled = false; 
                this.btnCtrAdd.Enabled = false;
                this.tspbarProgress.Value = 0;
                this.tslblDoingWhat.Text = "Adding Counters...";
                try
                {
                    //if (sysmon.LogFiles[1].Path != tbPFile.Lines[0])
                    //{
                    //    MessageBox.Show("Control has a custom log file set!","Warning!",MessageBoxButtons.OK);
                    //}
                    if (cmbCtrs.Text != "None" && cmbCtrs.Text != "Custom")
                    {
                        bool bFoundScenario = false;
                        foreach (Scenario scen in this.analyser.Scenarios)
                        {
                            if (scen.strScenario == cmbCtrs.Text)
                            {
                                this.tspbarProgress.Step = (100 / scen.strCtrs.Count);
                                foreach (string ctr in scen.strCtrs)
                                {
                                    this.sysmon.Counters.Add(cmbServer.Text + ctr);
                                    this.tspbarProgress.PerformStep();
                                }
                                bFoundScenario = true;
                                break;
                            }
                        }
                        if (!bFoundScenario)
                        {
                            this.sysmon.Counters.Add(cmbServer.Text+cmbCtrs.Text);
                        }
                    }
                }
                catch (Exception exSysmon)
                {
                    showExBox(exSysmon,"Error while adding counters!");
                }
                
                try
                {
                    if (cmbCtrs.Text == "Custom")
                            sysmon.BrowseCounters();
                }
                catch (Exception exSysmon)
                {
                    if (cmbCtrs.Text != "Custom")
                        showExBox(exSysmon,"Error adding counters.");
                }
                this.tspbarProgress.Value = 100;
                this.tslblDoingWhat.Text = "";
                this.btnCtrAdd.Enabled = true;
                this.btnPFile.Enabled = true;
            }
        }

        private void fillMachineList()
        {
            if (this.tbPFile.InvokeRequired)
            {
                EnumMachines_Callback cbk = new EnumMachines_Callback(fillMachineList);
                this.Invoke(cbk, null);
            }
            else
            {
                ulong pdhStatus;
                ulong dwBufSize = 0;
                int macCount = 0;
                byte[] macList = null;
                string[] machineNameList = null;

                this.btnPFile.Enabled = false;
                this.btnCtrAdd.Enabled = false;
                //this.tslblDoingWhat.Text = "Enumerating computer names in file...";
                this.tspbarProgress.Value = 20;
                //this.cmbServer.Items.Clear();
                //this.cmbServer.Items.Add(@"Enumerating...");
                //this.cmbServer.SelectedIndex = 0;
                try
                {
                    pdhStatus = PdhEnumMachines(tbPFile.Lines[0], macList, out dwBufSize);
                    pdhStatus = pdhStatus & 0x0FFFFFFFFL;
                    this.tspbarProgress.Value = 40;
                    if (pdhStatus == 0x800007D2L)
                    {
                        Encoding ascii = Encoding.ASCII;
                        macList = new byte[dwBufSize];
                        pdhStatus = PdhEnumMachines(tbPFile.Lines[0], macList, out dwBufSize);
                        machineNameList = ascii.GetString(macList, 0, macList.Length).Replace("\0\0", "").Split('\0');

                        this.cmbServer.Items.Clear();
                        this.cmbServer.Sorted = true;
                        //this.cmbServer.Items.Add(@"\\*\");
                        for (macCount = 0; macCount < machineNameList.Length; macCount++) //each (string st in str.Split('\0'))
                            this.cmbServer.Items.Add(machineNameList[macCount] + "\\");
                        this.cmbServer.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("PdhEnumMachines failed with " + pdhStatus.ToString("x") + "!! Bad bad counter file!", "Uh oh!");
                        this.cmbServer.Items.Clear();
                        this.cmbServer.Items.Add(@"\\*\");
                        this.cmbServer.SelectedIndex = 0;
                    }
                    if (sysmon.LogFiles.Count == 0 || sysmon.LogFiles[1].Path != tbPFile.Lines[0])
                    {
                        this.tspbarProgress.Value = 60;
                        sysmon.SuspendLayout();
                        sysmon.DataSourceType = DataSourceTypeConstants.sysmonNullDataSource;
                        sysmon.DataSourceType = DataSourceTypeConstants.sysmonLogFiles;
                        for (int logIter = sysmon.LogFiles.Count; logIter > 0; logIter--)
                            sysmon.LogFiles.Remove(logIter);
                        this.tspbarProgress.Value = 80;
                        for (int logIter = 0; logIter < tbPFile.Lines.Length; logIter++)
                            sysmon.LogFiles.Add(tbPFile.Lines[logIter]);
                        this.sysmon.ResumeLayout();
                    }
                }
                catch (Exception ex)
                {
                    showExBox(ex,"Failed to enumerate machines in the log file!!");
                }
                this.tspbarProgress.Value = 100;
                this.tslblDoingWhat.Text = "";
                this.btnCtrAdd.Enabled = true;
                this.btnPFile.Enabled = true;

            }
        }
        
        // Copy to Clipboard
        private void C2C()
        {
            if (this.tbPFile.InvokeRequired)
            {
                C2C_Callback cbk = new C2C_Callback(C2C);
                this.Invoke(cbk, null);
            }
            else
            {
                this.tspbarProgress.Value = 50;
                this.btnC2C.Enabled = false;
                string strTemp, strServer, strCtr;
                string[] strPath;
                int iCtrNmColWidth = 4, iSvrColWidth = 3, iNumColWidth = 20;
                double dblMax, dblMin, dblAvg;
                int iStatus;
                bool fShowServerCol = false;

                try
                {
                    for (int i = 1; i <= sysmon.Counters.Count; i++)
                    {
                        strPath = sysmon.Counters[i].Path.Split(@"\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        strServer = @"\\" + strPath[0] + @"\";
                        strCtr = strPath[1];
                        for (int iSplitPath = 2; iSplitPath < strPath.Length; iSplitPath++)
                            strCtr += @"\" + strPath[iSplitPath];
                        if (strCtr.Length > iCtrNmColWidth)
                            iCtrNmColWidth = strCtr.Length;
                        if (strServer.Length > iSvrColWidth)
                            iSvrColWidth = strServer.Length;
                        if (strServer != this.cmbServer.Text)
                            fShowServerCol = true;
                    }
                    iCtrNmColWidth += 4; iSvrColWidth += 4;
                }
                catch (Exception exSysmon)
                {
                    // assume values and proceed.
                    iCtrNmColWidth = 70; iSvrColWidth = 50;
                }

                try
                {
                    for (int i = 1; i <= sysmon.Counters.Count; i++) ;
                        //sysmon.Counters[1].
                    strTemp = "SQL Perfmon Analyzer v2.0"
                        + "\r\n========================="
                        + "\r\n File(s) : ";
                    for(int logIter = 1 ; logIter <= this.sysmon.LogFiles.Count; logIter++)
                        strTemp += "\r\n      " + this.sysmon.LogFiles[logIter].Path; 
                    strTemp += "\r\n Capture Start Time : " + this.sysmon.LogViewStart.ToString("yyyy-MM-dd HH:mm:ss")
                        + "\r\n Capture Stop Time  : " + this.sysmon.LogViewStop.ToString("yyyy-MM-dd HH:mm:ss")
                        + "\r\n Capture Duration   : " + this.sysmon.LogViewStop.Subtract(this.sysmon.LogViewStart).ToString()
                        + (fShowServerCol? "" : "\r\n Server Name        : " + this.cmbServer.Text);
                    strTemp += "\r\n\r\n" + "Counter Name".PadRight(iCtrNmColWidth, ' ')
                        + " " + "Avg".PadLeft(iNumColWidth, ' ')
                        + " " + "Max".PadLeft(iNumColWidth, ' ')
                        + " " + "Min".PadLeft(iNumColWidth, ' ')
                        + (fShowServerCol ? " " + "Server".PadRight(iSvrColWidth, ' ') : "");
                        //+ " " + "Status";
                    strTemp += "\r\n" + "-".PadRight(iCtrNmColWidth, '-')
                        + " " + "-".PadLeft(iNumColWidth, '-')
                        + " " + "-".PadLeft(iNumColWidth, '-')
                        + " " + "-".PadLeft(iNumColWidth, '-')
                        + (fShowServerCol ? " " + "-".PadRight(iSvrColWidth, '-') : "");
                        //+ " " + "-".PadLeft(19, '-');

                    for (int i = 1; i <= sysmon.Counters.Count; i++)
                    {
                        if (sysmon.Counters[i].Visible)
                        {
                            sysmon.Counters[i].GetStatistics(out dblMax, out dblMin, out dblAvg, out iStatus);
                            strPath = sysmon.Counters[i].Path.Split(@"\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            strServer = @"\\" + strPath[0] + @"\";
                            strCtr = strPath[1];
                            for (int iSplitPath = 2; iSplitPath < strPath.Length; iSplitPath++)
                                strCtr += @"\" + strPath[iSplitPath]; 
                            strTemp += "\r\n" + strCtr.PadRight(iCtrNmColWidth).Substring(0, iCtrNmColWidth)
                                + " " + dblAvg.ToString("0.000").PadLeft(iNumColWidth)
                                + " " + dblMax.ToString("0.000").PadLeft(iNumColWidth)
                                + " " + dblMin.ToString("0.000").PadLeft(iNumColWidth)
                                + (fShowServerCol ? " " + strServer.PadRight(iSvrColWidth).Substring(0, iSvrColWidth) : "");
                                //+ " " + iStatus.ToString();
                        }
                    }
                    Clipboard.Clear();
                    Clipboard.SetText(strTemp + "\r\n");
                }
                catch (Exception exSysmon)
                {
                    showExBox(exSysmon,"Error saving data to clipboard");
                }
                this.btnC2C.Enabled = true;
                this.tspbarProgress.Value = 100;
                this.tslblDoingWhat.Text = "";
            }
        }

        // Copy with spacing adjusted for variable width text
        private void C4MSS()
        {
            if (this.tbPFile.InvokeRequired)
            {
                C4MSS_Callback cbk = new C4MSS_Callback(C4MSS);
                this.Invoke(cbk, null);
            }
            else
            {
                this.tspbarProgress.Value = 50;
                this.btnC2C.Enabled = false;
                string strHeader, strTemp, strServer, strCtr;
                string[] strPath;
                int iCtrNmColWidth = 4, iSvrColWidth = 3, iNumColWidth = 20;
                double dblMax, dblMin, dblAvg;
                int iStatus;
                bool fShowServerCol = false;

                try
                {
                    for (int i = 1; i <= sysmon.Counters.Count; i++)
                    {
                        strPath = sysmon.Counters[i].Path.Split(@"\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        strServer = @"\\" + strPath[0] + @"\";
                        strCtr = strPath[1];
                        for (int iSplitPath = 2; iSplitPath < strPath.Length; iSplitPath++)
                            strCtr += @"\" + strPath[iSplitPath];
                        if (strCtr.Length > iCtrNmColWidth)
                            iCtrNmColWidth = strCtr.Length;
                        if (strServer.Length > iSvrColWidth)
                            iSvrColWidth = strServer.Length;
                        if (strServer != this.cmbServer.Text)
                            fShowServerCol = true;
                    }
                    iCtrNmColWidth += 4; iSvrColWidth += 4;
                }
                catch (Exception exSysmon)
                {
                    // assume values and proceed.
                    iCtrNmColWidth = 70; iSvrColWidth = 50;
                }

                try
                {
                    for (int i = 1; i <= sysmon.Counters.Count; i++) ;
                    //sysmon.Counters[1].
                    strHeader = "SQL Perfmon Analyzer v2.0"
                        + "\r\n========================="
                        + "\r\n File(s) : ";
                    for (int logIter = 1; logIter <= this.sysmon.LogFiles.Count; logIter++)
                        strHeader += "\r\n      " + this.sysmon.LogFiles[logIter].Path; 
                    strHeader += "\r\n Capture Start Time : " + this.sysmon.LogViewStart.ToString("yyyy-MM-dd HH:mm:ss")
                        + "\r\n Capture Stop Time  : " + this.sysmon.LogViewStop.ToString("yyyy-MM-dd HH:mm:ss")
                        + "\r\n Capture Duration   : " + this.sysmon.LogViewStop.Subtract(this.sysmon.LogViewStart).ToString()
                        + (fShowServerCol ? "" : "\r\n Server Name        : " + this.cmbServer.Text);
                    strTemp = "Counter Name".PadRight(iCtrNmColWidth, ' ')
                        + " " + "Avg".PadLeft(iNumColWidth, ' ')
                        + " " + "Max".PadLeft(iNumColWidth, ' ')
                        + " " + "Min".PadLeft(iNumColWidth, ' ')
                        + (fShowServerCol ? " " + "Server".PadRight(iSvrColWidth, ' ') : "");
                    //+ " " + "Status";
                    strTemp += "\r\n" + "-".PadRight(iCtrNmColWidth, '-')
                        + _spacer + "-".PadLeft(iNumColWidth, '-')
                        + _spacer + "-".PadLeft(iNumColWidth, '-')
                        + _spacer + "-".PadLeft(iNumColWidth, '-')
                        + (fShowServerCol ? " " + "-".PadRight(iSvrColWidth, '-') : "");
                    //+ " " + "-".PadLeft(19, '-');

                    for (int i = 1; i <= sysmon.Counters.Count; i++)
                    {
                        if (sysmon.Counters[i].Visible)
                        {
                            sysmon.Counters[i].GetStatistics(out dblMax, out dblMin, out dblAvg, out iStatus);
                            strPath = sysmon.Counters[i].Path.Split(@"\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            strServer = @"\\" + strPath[0] + @"\";
                            strCtr = strPath[1];
                            for (int iSplitPath = 2; iSplitPath < strPath.Length; iSplitPath++)
                                strCtr += @"\" + strPath[iSplitPath];


                            strTemp += "\r\n" + strCtr.PadRight(iCtrNmColWidth).Substring(0, iCtrNmColWidth)
                                + " " + dblAvg.ToString("0.000").PadLeft(iNumColWidth)
                                + " " + dblMax.ToString("0.000").PadLeft(iNumColWidth)
                                + " " + dblMin.ToString("0.000").PadLeft(iNumColWidth)
                                + (fShowServerCol ? " " + strServer.PadRight(iSvrColWidth).Substring(0, iSvrColWidth) : "");
                            //+ " " + iStatus.ToString();
                        }
                    }
                    buildTable(strTemp);
                    formatTable();

                    Clipboard.Clear();

                    copyTable(strHeader);
                }
                catch (Exception exSysmon)
                {
                    showExBox(exSysmon, "Error saving data to clipboard");
                }
                this.btnC2C.Enabled = true;
                this.tspbarProgress.Value = 100;
                this.tslblDoingWhat.Text = "";
            }
        }

        private void buildTable(String tableContents)
        {
            String theText = tableContents;//grab the text from the textbox

            if (theText != null && theText.Length > 3)//3 because there must be at least 2 characters + delimiter
            {

                //For some reason, newline.split has empty rows between actual rows
                //Below will remove the empty rows
                Regex NEWLINE = new Regex(@"\r|\n");
                Regex SPACES = new Regex(@"\s{3,}|\t{1,}");
                String[] tempRows = NEWLINE.Split(theText);
                ArrayList rowsList = new ArrayList();

                //Split the text into rows
                foreach (String row in tempRows)
                {
                    row.Trim();
                    if (row != null && row.Length > 0)
                        rowsList.Add(row);
                }
                rowsList.TrimToSize();
                String[] rows = (String[])rowsList.ToArray(typeof(String));

                _arrayTable = new Cell[rows.Length][];

                int rowCount = 0;
                foreach (String row in rows)
                {
                    String[] rowCellContents;
                    rowCellContents = SPACES.Split(row);                   

                    Cell[] rowCells = new Cell[rowCellContents.Length];
                    int colCount = 0;
                    foreach (String cellContents in rowCellContents)
                    {
                        //Just in case there's white-space, get rid of it.
                        cellContents.Trim();
                        rowCells[colCount] = new Cell(cellContents);
                        colCount++;
                    }
                    _arrayTable[rowCount] = rowCells;
                    rowCount++;
                }


                //foreach (Cell[] row in _arrayTable)
                //{
                //    foreach (Cell content in row)
                //    {
                //        Console.Write(content.contentType + "   ");
                //    }
                //    Console.Write('\n');
                //}

            }
        }


        private void formatTable()
        {
            if (_arrayTable != null)
            {

                bool header = true;

                ArrayList tempTable = new ArrayList();
                for (int colIndex = 0; colIndex < _arrayTable[0].Length; colIndex++)
                {
                    Column curTempCol = new Column();

                    int minWidth = 0;
                    int maxWidth = 0;
                    int curWidth = 0;
                    int type = Cell.TYPE_INT;
                    for (int rowIndex = 0; rowIndex < _arrayTable.Length; rowIndex++)
                    {
                        Cell curCell = _arrayTable[rowIndex][colIndex];
                        curWidth = curCell.width;

                        if (rowIndex == 0)
                            minWidth = maxWidth = curWidth;
                        else
                        {
                            if (curWidth > maxWidth)
                                maxWidth = curWidth;
                            else if (curWidth < minWidth)
                                minWidth = curWidth;

                            if (curCell.contentType == Cell.TYPE_STRING)//Assume that if there is a string in the column (header excluded) that it's a string column
                                type = curCell.contentType;
                        }

                        curTempCol.addCell(curCell);
                    }

                    curTempCol.width = maxWidth - minWidth;
                    curTempCol.type = type;
                    tempTable.Add(curTempCol);
                }

                //re-arrange table such that string cols are at the end, and cols are ordered by least-difference
                for (int colindex = 0; colindex < tempTable.Count; colindex++)
                {
                    Column curCol = (Column)tempTable[colindex];
                    if (curCol.type == Cell.TYPE_STRING && !curCol.rearranged)
                    {
                        int moveIndex = tempTable.Count - 1;
                        Column comparisonCol = (Column)tempTable[moveIndex];
                        while (curCol.width < comparisonCol.width && comparisonCol.type == Cell.TYPE_STRING && moveIndex > 0)
                        {
                            moveIndex--;
                            comparisonCol = (Column)tempTable[moveIndex];
                        }

                        curCol.rearranged = true;
                        tempTable.Insert(moveIndex + 1, curCol);
                        tempTable.RemoveAt(colindex);
                        colindex--;
                    }
                }

                for (int colindex = 0; colindex < tempTable.Count; colindex++)
                {
                    for (int rowIndex = 0; rowIndex < ((Column)tempTable[colindex]).cells.Count; rowIndex++)
                    {
                        _arrayTable[rowIndex][colindex] = (Cell)((Column)tempTable[colindex]).cells[rowIndex];
                    }
                }


                for (int colIndex = 0; colIndex < _arrayTable[0].Length; colIndex++)//By this point should have checked that all rows have same #columns using verifyTable()
                {
                    int targetWidth = 0;
                    int rowIndex = 0;

                    Cell curCell;

                    //Calculate target width
                    for (rowIndex = 0; rowIndex < _arrayTable.Length; rowIndex++)
                    {
                        curCell = _arrayTable[rowIndex][colIndex];

                        int targetWidthColIndex = colIndex;
                        int cumWidth = 0;
                        while (targetWidthColIndex >= 0)
                        {
                            _arrayTable[rowIndex][targetWidthColIndex].calculateWidth();
                            cumWidth += _arrayTable[rowIndex][targetWidthColIndex].width;
                            targetWidthColIndex--;
                        }
                        curCell.cumulativeWidth = cumWidth;
                        targetWidth = ((cumWidth > targetWidth) ? cumWidth : targetWidth);
                    }

                    //Adjust to meet the target
                    for (rowIndex = 0; rowIndex < _arrayTable.Length; rowIndex++)
                    {
                        curCell = _arrayTable[rowIndex][colIndex];
                        int curWidth = curCell.width;

                        if (rowIndex == 0 && header)//Align Headers same as next row
                        {
                            if (_arrayTable.Length > 1)
                            {
                                int typeOfNextCell = _arrayTable[1][colIndex].contentType;
                                curCell.contentType = typeOfNextCell;//Yes, technically incorrect, but this isn't used again and logic to format already there below
                            }
                        }
                        curCell.calculateWidth();
                        int previousWidth = curCell.width;
                        while (((curCell.cumulativeWidth + _widthOfSpace) < targetWidth) ||
                            ((curCell.cumulativeWidth + _widthOfSpace - targetWidth) < (targetWidth - curCell.cumulativeWidth)))
                        {
                            if (curCell.contentType == Cell.TYPE_STRING)//Align Strings Left
                                curCell.contents += " ";
                            else if (curCell.contentType == Cell.TYPE_INT || curCell.contentType == Cell.TYPE_FLOAT)//Align numbers right
                                curCell.contents = " " + curCell.contents;
                            else
                            {
                                //TODO: Try to align floats to decimal point
                            }

                            curCell.calculateWidth();
                            curCell.cumulativeWidth += (curCell.width - previousWidth);
                            previousWidth = curCell.width;
                        }


                        curCell.contents += _spacer;

                        _arrayTable[rowIndex][colIndex] = curCell;
                    }
                }
            }
        }

        private void copyTable(String header)
        {
            if (_arrayTable != null)
            {
                String tableString = "";
                for (int rowIndex = 0; rowIndex < _arrayTable.Length; rowIndex++)
                {
                    String rowString = "";
                    if (_arrayTable[rowIndex] != null)
                    {
                        for (int colIndex = 0; colIndex < _arrayTable[rowIndex].Length; colIndex++)
                        {
                            if (_arrayTable[rowIndex][colIndex] != null)
                            {
                                String curCellContents = _arrayTable[rowIndex][colIndex].contents;
                                if (curCellContents != null)
                                    rowString += curCellContents;
                            }
                        }
                        rowString += '\n';
                    }
                    tableString += rowString;
                }
                Clipboard.SetText(header + "\r\n\n" + tableString);
            }
        }
      
        private void tmrDurRefresh_Tick(object sender, EventArgs e)
        {
            CheckSysmonSource();
            String strLogView;
            try
            {
                strLogView = "Data Duration : "
                    + sysmon.LogViewStart.ToString("yyyy-MM-dd HH:mm:ss") 
                    + " To " 
                    + sysmon.LogViewStop.ToString("yyyy-MM-dd HH:mm:ss");
                if (tslblStatus.Text != strLogView)
                    tslblStatus.Text = strLogView;
            }
            catch (Exception)
            {
                // Ignore exception
                if (tslblStatus.Text == "")
                    tslblStatus.Text = "Data Duration : 0000-00-00 00:00:00 To 0000-00-00 00:00:00";
            }

            if ((!this.bgWrkr.IsBusy) && (this.tspbarProgress.Value == 100))
                this.tspbarProgress.Value = 0;
        }

        private void CheckSysmonSource()
        {
            var unfilteredFileNames = sysmon.LogFileName.Replace("\0", ",").Split(',');
            var filtered = new List<string>();
            foreach (var name in unfilteredFileNames)
            {
                if (!string.IsNullOrEmpty(name)) filtered.Add(name);
            }
            var fileNames = filtered.ToArray();
            if (fileNames.Length == 0 || ((tbPFile.Lines.Length <= 0 || fileNames[0] == tbPFile.Lines[0]) 
                && tbPFile.Lines.Length != 0)) return;
            var line = new List<string>();
            foreach (string files in tbPFile.Lines)
            {
                line.Add(files);
            }
            foreach (var fileName in fileNames)
            {
                line.Add(fileName);
            }
            tbPFile.Lines = line.ToArray();
            EnumMachines();
        }

        private void btnPFile_Click(object sender, EventArgs e)
        {
            tslblDoingWhat.Text = "Adding Perfmon File..";
            dlgOPFile.FileName = tbPFile.Text;
            dlgOPFile.ShowDialog();
            tbPFile.Text = "";
            tbPFile.Lines = dlgOPFile.FileNames;
            if (tbPFile.Lines.Length > 0)
            {
                EnumMachines();
            }
        }

        private void EnumMachines()
        {
            tslblDoingWhat.Text = "Enumerating computer names in file...";
            this.tspbarProgress.Value = 10;
            if (System.IO.File.Exists(tbPFile.Lines[0]))
            {
                this.bgWrkr.RunWorkerAsync("Enum_Machines");
            }
            else
                showExBox(new FileNotFoundException(),
                    "Performance counter file '" + tbPFile.Text + "' does not exist!!");

        }

        private void btnCtrAdd_Click(object sender, EventArgs e)
        {
            this.btnCtrAdd.Enabled = false;
            if (string.IsNullOrEmpty(tbPFile.Text))
            {
                MessageBox.Show(@"Please select a perfmon file before adding counters.",@"Missing Perfmon File", MessageBoxButtons.OK,MessageBoxIcon.Stop);
                this.btnCtrAdd.Enabled = true;
            }
            else
            {
                if (System.IO.File.Exists(tbPFile.Lines[0]))
                {
                    this.bgWrkr.RunWorkerAsync("Add_Counter");
                }
                else
                {
                    showExBox(new FileNotFoundException(),
                        "Performance counter file '" + tbPFile.Text + "' does not exist!!");
                    this.btnCtrAdd.Enabled = true;
                }

            }
        }

        private void btnC2C_Click(object sender, EventArgs e)
        {
            this.tspbarProgress.Value = 0;
            this.bgWrkr.RunWorkerAsync("C2C");
        }
        
        // Do heavy lifting in background thread. Can't allow UI freezing.
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument.ToString() == "Add_Counter")
                this.CtrAdd();
            if (e.Argument.ToString() == "Enum_Machines")
                this.fillMachineList();
            if (e.Argument.ToString() == "C2C")
                this.C2C();
            if (e.Argument.ToString() == "C4MSS")
                this.C4MSS();
        }

        private void btnClrCtrs_Click(object sender, EventArgs e)
        {
            this.sysmon.Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tspbarProgress.Value = 0;
            this.bgWrkr.RunWorkerAsync("C4MSS");
        }

        private void cmbCtrs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     }
}
