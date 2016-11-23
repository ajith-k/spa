namespace spa
{
    partial class frmSPA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSPA));
            this.lblPFile = new System.Windows.Forms.Label();
            this.tbPFile = new System.Windows.Forms.TextBox();
            this.btnPFile = new System.Windows.Forms.Button();
            this.btnC2C = new System.Windows.Forms.Button();
            this.cmbCtrs = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.btnCtrAdd = new System.Windows.Forms.Button();
            this.dlgOPFile = new System.Windows.Forms.OpenFileDialog();
            this.tmrDurRefresh = new System.Windows.Forms.Timer(this.components);
            this.bgWrkr = new System.ComponentModel.BackgroundWorker();
            this.btnClrCtrs = new System.Windows.Forms.Button();
            this.cmbServer = new System.Windows.Forms.ComboBox();
            this.lblCtrGrp = new System.Windows.Forms.Label();
            this.tlpOuter = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlpRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.sysmon = new AxSystemMonitor.AxSystemMonitor();
            this.tlpRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspbarProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblDoingWhat = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpOuter.SuspendLayout();
            this.tlpRow1.SuspendLayout();
            this.tlpRow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sysmon)).BeginInit();
            this.tlpRow3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPFile
            // 
            this.lblPFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPFile.AutoSize = true;
            this.lblPFile.Location = new System.Drawing.Point(3, 3);
            this.lblPFile.Margin = new System.Windows.Forms.Padding(3);
            this.lblPFile.Name = "lblPFile";
            this.lblPFile.Size = new System.Drawing.Size(79, 24);
            this.lblPFile.TabIndex = 0;
            this.lblPFile.Text = "Perfmon Files";
            this.lblPFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbPFile
            // 
            this.tbPFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPFile.Location = new System.Drawing.Point(88, 3);
            this.tbPFile.Multiline = true;
            this.tbPFile.Name = "tbPFile";
            this.tbPFile.ReadOnly = true;
            this.tbPFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPFile.Size = new System.Drawing.Size(438, 24);
            this.tbPFile.TabIndex = 1;
            this.tbPFile.TabStop = false;
            // 
            // btnPFile
            // 
            this.btnPFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPFile.Location = new System.Drawing.Point(532, 3);
            this.btnPFile.Name = "btnPFile";
            this.btnPFile.Size = new System.Drawing.Size(30, 24);
            this.btnPFile.TabIndex = 2;
            this.btnPFile.Text = "...";
            this.btnPFile.UseVisualStyleBackColor = true;
            this.btnPFile.Click += new System.EventHandler(this.btnPFile_Click);
            // 
            // btnC2C
            // 
            this.btnC2C.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnC2C.Location = new System.Drawing.Point(448, 3);
            this.btnC2C.Name = "btnC2C";
            this.btnC2C.Size = new System.Drawing.Size(114, 24);
            this.btnC2C.TabIndex = 6;
            this.btnC2C.Text = "&Copy (Fixed Width)";
            this.btnC2C.UseVisualStyleBackColor = true;
            this.btnC2C.Click += new System.EventHandler(this.btnC2C_Click);
            // 
            // cmbCtrs
            // 
            this.cmbCtrs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCtrs.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCtrs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCtrs.FormattingEnabled = true;
            this.cmbCtrs.Items.AddRange(new object[] {
            "Default",
            "CPU",
            "Disk",
            "Network"});
            this.cmbCtrs.Location = new System.Drawing.Point(321, 3);
            this.cmbCtrs.Name = "cmbCtrs";
            this.cmbCtrs.Size = new System.Drawing.Size(152, 21);
            this.cmbCtrs.TabIndex = 4;
            this.cmbCtrs.Text = "Default";
            this.cmbCtrs.SelectedIndexChanged += new System.EventHandler(this.cmbCtrs_SelectedIndexChanged);
            // 
            // lblServer
            // 
            this.lblServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(3, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(79, 30);
            this.lblServer.TabIndex = 7;
            this.lblServer.Text = "Computer     ";
            this.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCtrAdd
            // 
            this.btnCtrAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCtrAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCtrAdd.Location = new System.Drawing.Point(479, 3);
            this.btnCtrAdd.Name = "btnCtrAdd";
            this.btnCtrAdd.Size = new System.Drawing.Size(83, 24);
            this.btnCtrAdd.TabIndex = 5;
            this.btnCtrAdd.Text = "&Add";
            this.btnCtrAdd.UseVisualStyleBackColor = true;
            this.btnCtrAdd.Click += new System.EventHandler(this.btnCtrAdd_Click);
            // 
            // dlgOPFile
            // 
            this.dlgOPFile.Filter = "BLG Files (*.BLG)|*.blg|CSV Files (*.CSV)|*.csv|All Files (*.*)|*.*";
            this.dlgOPFile.Multiselect = true;
            this.dlgOPFile.Title = "Open Counter Log File";
            // 
            // tmrDurRefresh
            // 
            this.tmrDurRefresh.Enabled = true;
            this.tmrDurRefresh.Interval = 500;
            this.tmrDurRefresh.Tick += new System.EventHandler(this.tmrDurRefresh_Tick);
            // 
            // bgWrkr
            // 
            this.bgWrkr.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // btnClrCtrs
            // 
            this.btnClrCtrs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClrCtrs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClrCtrs.Location = new System.Drawing.Point(3, 3);
            this.btnClrCtrs.Name = "btnClrCtrs";
            this.btnClrCtrs.Size = new System.Drawing.Size(80, 24);
            this.btnClrCtrs.TabIndex = 7;
            this.btnClrCtrs.Text = "C&lear";
            this.btnClrCtrs.UseVisualStyleBackColor = true;
            this.btnClrCtrs.Click += new System.EventHandler(this.btnClrCtrs_Click);
            // 
            // cmbServer
            // 
            this.cmbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbServer.FormattingEnabled = true;
            this.cmbServer.Items.AddRange(new object[] {
            "\\\\*\\"});
            this.cmbServer.Location = new System.Drawing.Point(88, 3);
            this.cmbServer.Name = "cmbServer";
            this.cmbServer.Size = new System.Drawing.Size(145, 21);
            this.cmbServer.TabIndex = 3;
            // 
            // lblCtrGrp
            // 
            this.lblCtrGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCtrGrp.AutoSize = true;
            this.lblCtrGrp.Location = new System.Drawing.Point(239, 0);
            this.lblCtrGrp.Name = "lblCtrGrp";
            this.lblCtrGrp.Size = new System.Drawing.Size(76, 30);
            this.lblCtrGrp.TabIndex = 13;
            this.lblCtrGrp.Text = "Counter group";
            this.lblCtrGrp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpOuter
            // 
            this.tlpOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpOuter.AutoSize = true;
            this.tlpOuter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpOuter.ColumnCount = 1;
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuter.Controls.Add(this.tlpRow1, 0, 0);
            this.tlpOuter.Controls.Add(this.tlpRow2, 0, 1);
            this.tlpOuter.Controls.Add(this.sysmon, 0, 3);
            this.tlpOuter.Controls.Add(this.tlpRow3, 0, 2);
            this.tlpOuter.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpOuter.Location = new System.Drawing.Point(3, 3);
            this.tlpOuter.Name = "tlpOuter";
            this.tlpOuter.RowCount = 4;
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuter.Size = new System.Drawing.Size(565, 344);
            this.tlpOuter.TabIndex = 14;
            // 
            // tlpRow1
            // 
            this.tlpRow1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRow1.AutoSize = true;
            this.tlpRow1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpRow1.ColumnCount = 3;
            this.tlpRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpRow1.Controls.Add(this.lblPFile, 0, 0);
            this.tlpRow1.Controls.Add(this.tbPFile, 1, 0);
            this.tlpRow1.Controls.Add(this.btnPFile, 2, 0);
            this.tlpRow1.Location = new System.Drawing.Point(0, 0);
            this.tlpRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow1.Name = "tlpRow1";
            this.tlpRow1.RowCount = 1;
            this.tlpRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow1.Size = new System.Drawing.Size(565, 30);
            this.tlpRow1.TabIndex = 0;
            // 
            // tlpRow2
            // 
            this.tlpRow2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRow2.AutoSize = true;
            this.tlpRow2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpRow2.ColumnCount = 5;
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tlpRow2.Controls.Add(this.btnCtrAdd, 4, 0);
            this.tlpRow2.Controls.Add(this.cmbCtrs, 3, 0);
            this.tlpRow2.Controls.Add(this.lblServer, 0, 0);
            this.tlpRow2.Controls.Add(this.lblCtrGrp, 2, 0);
            this.tlpRow2.Controls.Add(this.cmbServer, 1, 0);
            this.tlpRow2.Location = new System.Drawing.Point(0, 30);
            this.tlpRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow2.Name = "tlpRow2";
            this.tlpRow2.RowCount = 1;
            this.tlpRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow2.Size = new System.Drawing.Size(565, 30);
            this.tlpRow2.TabIndex = 1;
            // 
            // sysmon
            // 
            this.sysmon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sysmon.Enabled = true;
            this.sysmon.Location = new System.Drawing.Point(3, 93);
            this.sysmon.Name = "sysmon";
            this.sysmon.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("sysmon.OcxState")));
            this.sysmon.Size = new System.Drawing.Size(559, 248);
            this.sysmon.TabIndex = 5;
            // 
            // tlpRow3
            // 
            this.tlpRow3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpRow3.AutoSize = true;
            this.tlpRow3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpRow3.ColumnCount = 5;
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpRow3.Controls.Add(this.btnC2C, 4, 0);
            this.tlpRow3.Controls.Add(this.btnClrCtrs, 0, 0);
            this.tlpRow3.Controls.Add(this.button1, 3, 0);
            this.tlpRow3.Location = new System.Drawing.Point(0, 60);
            this.tlpRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tlpRow3.Name = "tlpRow3";
            this.tlpRow3.RowCount = 1;
            this.tlpRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRow3.Size = new System.Drawing.Size(565, 30);
            this.tlpRow3.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(328, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 24);
            this.button1.TabIndex = 8;
            this.button1.Text = "C&opy (Var W Text)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbarProgress,
            this.tslblStatus,
            this.tslblDoingWhat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(569, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tspbarProgress
            // 
            this.tspbarProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspbarProgress.AutoSize = false;
            this.tspbarProgress.Name = "tspbarProgress";
            this.tspbarProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // tslblStatus
            // 
            this.tslblStatus.AutoSize = false;
            this.tslblStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tslblStatus.Name = "tslblStatus";
            this.tslblStatus.Size = new System.Drawing.Size(325, 17);
            this.tslblStatus.Text = "Data Duration : 0000-00-00 00:00:00 To 0000-00-00 00:00:00";
            // 
            // tslblDoingWhat
            // 
            this.tslblDoingWhat.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tslblDoingWhat.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.tslblDoingWhat.Margin = new System.Windows.Forms.Padding(3);
            this.tslblDoingWhat.Name = "tslblDoingWhat";
            this.tslblDoingWhat.Size = new System.Drawing.Size(121, 16);
            this.tslblDoingWhat.Spring = true;
            this.tslblDoingWhat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmSPA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 372);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tlpOuter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(585, 410);
            this.Name = "frmSPA";
            this.Text = "SQL Perfmon Analyzer";
            this.tlpOuter.ResumeLayout(false);
            this.tlpOuter.PerformLayout();
            this.tlpRow1.ResumeLayout(false);
            this.tlpRow1.PerformLayout();
            this.tlpRow2.ResumeLayout(false);
            this.tlpRow2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sysmon)).EndInit();
            this.tlpRow3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPFile;
        private System.Windows.Forms.TextBox tbPFile;
        private System.Windows.Forms.Button btnPFile;
        private System.Windows.Forms.Button btnC2C;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button btnCtrAdd;
        private AxSystemMonitor.AxSystemMonitor sysmon;
        private System.Windows.Forms.OpenFileDialog dlgOPFile;
        private System.Windows.Forms.Timer tmrDurRefresh;
        private System.Windows.Forms.ComboBox cmbCtrs;
        private System.ComponentModel.BackgroundWorker bgWrkr;
        private System.Windows.Forms.Button btnClrCtrs;
        private System.Windows.Forms.ComboBox cmbServer;
        private System.Windows.Forms.Label lblCtrGrp;
        private System.Windows.Forms.TableLayoutPanel tlpOuter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar tspbarProgress;
        private System.Windows.Forms.ToolStripStatusLabel tslblStatus;
        private System.Windows.Forms.TableLayoutPanel tlpRow1;
        private System.Windows.Forms.TableLayoutPanel tlpRow2;
        private System.Windows.Forms.TableLayoutPanel tlpRow3;
        private System.Windows.Forms.ToolStripStatusLabel tslblDoingWhat;
        private System.Windows.Forms.Button button1;
    }
}

