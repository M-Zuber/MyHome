using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BL;
using FrameWork;

namespace MyHome2013
{
    public partial class Login : Form
    {
        #region Properties

        List<TextBox> allTextBoxes;

        public bool ConnectionSuccess { get; set; }

        #endregion

        #region C'Tor

        public Login()
        {
            InitializeComponent();
            ConnectionSuccess = false;
            allTextBoxes = new List<TextBox> { txtDatabaseName, txtUserId, txtPassword };
        }
        
        #endregion

        #region Control Methods

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Globals.DataBaseName = "";
            Globals.UserId = "";
            Globals.Password = "";
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
                Globals.DataBaseName = txtDatabaseName.Text;
                Globals.UserId = txtUserId.Text;
                Globals.Password = txtPassword.Text;

                if (!TryConnect())
                {
                    MessageBox.Show("One or more fields contains incorrect information", "Error connecting...",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (TextBox CurrTextbox in allTextBoxes)
                    {
                        CurrTextbox.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Welcome to MyHome2013", "Connection success",
                                   MessageBoxButtons.OK, MessageBoxIcon.None);
                    ConnectionSuccess = true;
                    Dictionary<string, string> databaseSettings = new Dictionary<string, string>() 
                    {
                        {"Database Name", Globals.DataBaseName},
                        {"User Id", Globals.UserId},
                        {"Password", Globals.Password}
                    };
                    Globals.SettingFiles["DatabaseSettings"].SaveSettings(databaseSettings);
                    this.Close();
                }
            }
        }
        
        #endregion

        #region Other Methods

        private bool TryConnect()
        {
            return HelperMethods.TestConnection();
        }

        #endregion
    }
}
