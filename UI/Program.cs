using System;
using System.Windows.Forms;
using FrameWork;
using System.Collections.Generic;

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
            Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was started at: " + DateTime.Now);

            if (!Globals.SettingFiles["DatabaseSettings"].AreSettingsSet(
                                new List<string>() { "Database Name", "User Id", "Password" }))
            {
                Login connecting = new Login();
                Application.Run(connecting);
                if (connecting.ConnectionSuccess)
                {
                    Application.Run(new MenuMDIUI());
                }
            }
            else
            {
                Dictionary<string, string> allSettings = 
                    Globals.SettingFiles["DatabaseSettings"].GetAllSettings();
                Globals.DataBaseName = allSettings["Database Name"];
                Globals.UserId = allSettings["User Id"];
                Globals.Password = allSettings["Password"];
                Application.Run(new MenuMDIUI());
            }

            Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was closed at: " + DateTime.Now);
        }
    }
}
