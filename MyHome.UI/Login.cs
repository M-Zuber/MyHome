using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyHome.Infrastructure;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    ///     Form for logging into the database
    ///     -as a prerequisite to starting the program for the first time
    /// </summary>
    public partial class Login : Form
    {
        // All the text-boxes in the form to allow for easy validation
        private readonly List<TextBox> _allTextBoxes;

        /// <summary>
        ///     Sets up the properties and data members of the form
        /// </summary>
        public Login()
        {
            InitializeComponent();
            ConnectionSuccess = false;
            _allTextBoxes = new List<TextBox> {txtDatabaseName, txtUserId, txtPassword};
        }

        /// <summary>
        ///     Indicator if the parameters for connection are correct
        /// </summary>
        public bool ConnectionSuccess { get; set; }

        /// <summary>
        ///     Validates the connection parameters
        ///     -If they pass, saves them into the appropriate settings file
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            // Resets the local variables for the connection parameters
            // towards the next connection attempt
            Globals.DataBaseName = "";
            Globals.UserId = "";
            Globals.Password = "";

            // Checks that all the text boxes are filled in
            if (_allTextBoxes.Count(currTextbox => currTextbox.Text == "") != 0)
            {
                MessageBox.Show("All fields must be filled in to try and connect", "Error connecting...",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (var currTextbox in _allTextBoxes)
                {
                    currTextbox.Text = "";
                }
            }
            else
            {
                // Sets the local variables for the database connection parameters
                Globals.DataBaseName = txtDatabaseName.Text;
                Globals.UserId = txtUserId.Text;
                Globals.Password = txtPassword.Text;


                // Informs the user of success
                MessageBox.Show("Welcome to MyHome", "Connection success",
                    MessageBoxButtons.OK, MessageBoxIcon.None);

                // Sets the flag property to indicate success
                ConnectionSuccess = true;

                // Creates a dictionary of the connection parameters in order to save it into the file
                var databaseSettings = new Dictionary<string, string>
                {
                    {"Database Name", Globals.DataBaseName},
                    {"User Id", Globals.UserId},
                    {"Password", Globals.Password}
                };

                // Saves the connection parameters into the appropriate file
                Globals.SettingFiles["DatabaseSettings"].SaveSettings(databaseSettings);
                Close();
            }
        }
    }
}