using FrameWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyHome2013
{
    /// <summary>
    /// Form for logging into the databse
    /// -as a prerequisite to starting the program for the first time
    /// </summary>
    public partial class Login : Form
    {
        #region Data Members

        // All the text-boxes in the form to allow for easy validation
        private List<TextBox> allTextBoxes;
        private Func<bool> testConnection;

        #endregion

        #region Properties

        /// <summary>
        /// Indicator if the parameters for connection are correct
        /// </summary>
        public bool ConnectionSuccess { get; set; }

        #endregion

        #region C'Tor

        /// <summary>
        /// Sets up the properties and data members of the form
        /// </summary>
        public Login(Func<bool> testConnection)
        {
            InitializeComponent();
            this.testConnection = testConnection;
            this.ConnectionSuccess = false;
            this.allTextBoxes = new List<TextBox> { txtDatabaseName, txtUserId, txtPassword };
        }
        
        #endregion

        #region Control Methods

        /// <summary>
        /// Validates the connection parameters
        ///  -If they pass, saves them into the appropiate settings file
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Resests the local variables for the connection parameters
            // towards the next connection attempt
            Globals.DataBaseName = "";
            Globals.UserId = "";
            Globals.Password = "";

            // Checks that all the text boxes are filled in
            if (string.IsNullOrWhiteSpace(txtDatabaseName.Text)
                || !File.Exists(txtDatabaseName.Text.Trim()))
            {
                MessageBox.Show("The database path must be filled in to try and connect", "Error connecting...",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Sets the local variables for the database connection parameters
                Globals.DataBaseName = txtDatabaseName.Text;
                Globals.UserId = txtUserId.Text;
                Globals.Password = txtPassword.Text;

                // Tries to connect with the parameters entered into the form by the user
                // If the connection fails, informs the user and clears the form
                if (!this.testConnection())
                {
                    MessageBox.Show("One or more fields contains incorrect information", "Error connecting...",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Clears all the text-boxes on the form
                    foreach (TextBox CurrTextbox in allTextBoxes)
                    {
                        CurrTextbox.Text = "";
                    }
                }
                // Saves the parameters into the settings file, and sets ConnectionSuccess to true
                // to notify the program to continue
                else
                {
                    // Informs the user of success
                    MessageBox.Show("Welcome to MyHome2013", "Connection success",
                                   MessageBoxButtons.OK, MessageBoxIcon.None);

                    // Sets the flag property to indicate success
                    this.ConnectionSuccess = true;

                    // Creates a dictionary of the connection parameters in order to save it into the file
                    Dictionary<string, string> databaseSettings = new Dictionary<string, string>() 
                    {
                        {"ProviderName", "System.Data.SQLite"},
                        {"Database Name", Globals.DataBaseName},
                        {"User Id", Globals.UserId},
                        {"Password", Globals.Password}
                    };

                    // Saves the connection parameters into the appropiate file
                    Globals.SettingFiles["DatabaseSettings"].SaveSettings(databaseSettings);
                    this.Close();
                }
            }
        }
        
        #endregion
    }
}
