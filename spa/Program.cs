using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace spa
{
    static class Program
    {

        internal static readonly Font FONT = new Font("Segoe UI", 9.0F);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Environment.GetCommandLineArgs().Length == 1)
                Application.Run(new frmSPA());
            else
                Application.Run(new frmSPA(Environment.GetCommandLineArgs()));
        }
    }
}
