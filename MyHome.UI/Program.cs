using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FrameWork;

namespace MyHome.UI
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

            Application.Run(new MenuMDIUI());

            // With the current configuration there is no login needed.
            // The code is left as it may change soon, and while we do have source control it will be easier to simply uncomment this
            // If the settings for connecting to the database are not set yet
            //if (!Globals.SettingFiles["DatabaseSettings"].AreSettingsSet(
            //                    new List<string>() { "Database Name", "User Id", "Password" }))
            //{
            //    // Intializes and runs an instance of the login form
            //    Login connecting = new Login();
            //    Application.Run(connecting);

            //    // If the user enters correct connection parameters
            //    if (connecting.ConnectionSuccess)
            //    {
            //        // Runs the main application
            //        Application.Run(new MenuMDIUI());
            //    }
            //}
            //// If the database settings where previously set
            //else
            //{
            //    // Getse all the settings
            //    Dictionary<string, string> allSettings = 
            //        Globals.SettingFiles["DatabaseSettings"].GetAllSettings();
                
            //    // Sets the local variables with the parameters saved in the database
            //    Globals.DataBaseName = allSettings["Database Name"];
            //    Globals.UserId = allSettings["User Id"];
            //    Globals.Password = allSettings["Password"];

            //    // Runs the main application
            //    Application.Run(new MenuMDIUI());
            //}

            Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was closed at: " + DateTime.Now);
        }
    }
}
