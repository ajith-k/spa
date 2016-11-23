# spa

SQL Perfmon Analyzer

Born out of a need to summarize performance counter values while analyzing SQL performance issues, this tool is a wrapper on the windows performance counter analysis control, namely sysmon.ocx.

That said, this tool can be useful to anyone who has to analyze any performance counters be they built-in windows counters or any application counters.

Once built you will need the following 4 files to allow the application to work.

  AxInterop.SystemMonitor.dll
  Interop.SystemMonitor.dll
  spa.exe
  spa.xml


The spa.xml file is a human readable (and editable) list of counter groups that can be added as a bunch. 

Comments and feedback welcome!