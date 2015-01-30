using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FrameWork;
using MySql.Data.MySqlClient;
using DataAccess;
using System.Data.Common;
using LocalTypes;
using BusinessLogic;
using LightInject;

namespace MyHome2013
{
    static class Program
    {
        static string ProviderName = "MySql.Data.MySqlClient";
        static string Server = "127.0.0.10";

        public static IServiceContainer Container = null;

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

            var dbprovider = ConnectionManager.GetDbProvider(ProviderName, new ConnectionOptions
            {
                Server = Server,
                Database = Globals.DataBaseName,
                Username = Globals.UserId,
                Password = Globals.Password
            });

            Program.Container = BuildContainer(dbprovider);

            // Runs the main application
            Application.Run(new MenuMDIUI());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Globals.LogFiles["ProgramActivityLog"].AddMessage("The program was closed at: " + DateTime.Now);
        }

        static bool TestConnection()
        {
            var provider = ConnectionManager.GetDbProvider(ProviderName, new ConnectionOptions
            {
                Server = Server,
                Database = Globals.DataBaseName,
                Username = Globals.UserId,
                Password = Globals.Password
            });

            return ConnectionManager.TestConnection(provider);
        }

        public static IServiceContainer BuildContainer(DbProviderFactory dbprovider)
        {
            var container = new ServiceContainer();

            // Register Data Layer
            container.Register<IRepository<PaymentMethod>>(context => new CachedPaymentMethodRepository(new PaymentMethodAccess(dbprovider)), new PerContainerLifetime());
            container.Register<IRepository<IncomeCategory>>(context => new CachedIncomeCategoryRepository(new IncomeCategoryAccess(dbprovider)), new PerContainerLifetime());
            container.Register<IRepository<ExpenseCategory>>(context => new CachedExpenseCategoryRepository(new ExpenseCategoryAccess(dbprovider)), new PerContainerLifetime());
            container.Register<ITransactionRepository<Income>>(context => new CachedIncomeRepository(new IncomeAccess(dbprovider)), new PerContainerLifetime());
            container.Register<ITransactionRepository<Expense>>(context => new CachedExpenseRepository(new ExpenseAccess(dbprovider)), new PerContainerLifetime());

            // Register Business Layer
            container.Register<IncomeHandler>(new PerContainerLifetime());
            container.Register<ExpenseHandler>(new PerContainerLifetime());

            container.Register<DateTime, MonthHandler>((context, value) => new MonthHandler(context.GetInstance<IncomeHandler>(), context.GetInstance<ExpenseHandler>(), value));
            container.Register<Backup>();

            return container;
        }
    }
}
