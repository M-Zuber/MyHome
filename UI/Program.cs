using System;
using System.Windows.Forms;
using FrameWork;

namespace MyHome2013
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Globals.LogFiles["ActivityLog"].AddMessage("The program was started at: " + DateTime.Now);
            Application.Run(new MenuMDIUI());
            Globals.LogFiles["ActivityLog"].AddMessage("The program was closed at: " + DateTime.Now);
        }
    }
}
