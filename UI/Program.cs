using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FrameWork;
using MySql.Data.MySqlClient;
using DataAccess;

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
            Application.ApplicationExit += Application_ApplicationExit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was started at: " + DateTime.Now);

            // If the settings for connecting to the database are not set yet
            if (!Globals.SettingFiles["DatabaseSettings"].AreSettingsSet(new[] { "Database Name", "User Id", "Password" }))
            {
                // Intializes and runs an instance of the login form
                Login connecting = new Login(TestConnection);
                Application.Run(connecting);

                // If the user does not enter correct connection parameters, exit the application
                if (!connecting.ConnectionSuccess)
                {
                    Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was closed because of invalid connection information at: " + DateTime.Now);
                    return;
                }
            }
            // If the database settings where previously set
            else
            {
                // Getse all the settings
                var allSettings = Globals.SettingFiles["DatabaseSettings"].GetAllSettings();

                // Sets the local variables with the parameters saved in the database
                Globals.DataBaseName = allSettings["Database Name"];
                Globals.UserId = allSettings["User Id"];
                Globals.Password = allSettings["Password"];
            }

            ConnectionManager.ProviderFactory = BuildProvider(Globals.DataBaseName, Globals.UserId, Globals.Password);

            // Runs the main application
            Application.Run(new MenuMDIUI());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was closed at: " + DateTime.Now);
        }

        static DbProviderFactoryProxy BuildProvider(string db, string username, string password)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.10",
                Database = db,
                UserID = username,
                Password = password
            };

            return new DbProviderFactoryProxy("MySql.Data.MySqlClient", builder.ToString());
        }

        static bool TestConnection()
        {
            return ConnectionManager.TestConnection(BuildProvider(Globals.DataBaseName, Globals.UserId, Globals.Password));
        }
    }
}
