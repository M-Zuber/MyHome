using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using FrameWork;

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
        public Login()
        {
            InitializeComponent();
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
            if (allTextBoxes.Where(currTextbox => currTextbox.Text == "").Count() != 0)
            {
                MessageBox.Show("All fields must be filled in to try and connect", "Error connecting...",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (TextBox CurrTextbox in allTextBoxes)
                {
                    CurrTextbox.Text = "";
                }
            }
            else
            {
                // Sets the local variables for the database connection parameters
                Globals.DataBaseName = txtDatabaseName.Text;
                Globals.UserId = txtUserId.Text;
                Globals.Password = txtPassword.Text;

                // Tries to connect with the parameters entered into the form by the user
                // If the connection fails, informs the user and clears the form
                if (!TryConnect())
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

        #region Other Methods

        /// <summary>
        /// Tests the Db connection with the current parameters in the settings
        /// </summary>
        /// <returns>True if the database can be connected to, otherwise false</returns>
        private bool TryConnect()
        {
            return HelperMethods.TestConnection();
        }

        #endregion
    }
}
