using System;
using System.Windows.Forms;

namespace MyHome.UI
{
    /// <summary>
    /// A form to allow the user to choose the type of item to open an add form for
    /// </summary>
    public partial class NewChoice : Form
    {
        #region Properties

        /// <summary>
        /// The result of the users choice
        /// </summary>
        public int UserChoice { get; private set; } 
        
        #endregion

        #region C'Tor

        /// <summary>
        /// Default Ctor
        /// </summary>
        public NewChoice()
        {
            InitializeComponent();
        }
 
        #endregion

        #region Control Event Methods

        /// <summary>
        /// If the user press the okay button checks that a value is checked before setting
        /// the local property -if yes closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            // If neither option is checked
            if (!rdbExpense.Checked && !rdbIncome.Checked)
            {
                MessageBox.Show("Please choose one of the options",
                                "Opening...",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1);
            }
            // Sets the local property with an indicator for expense
            // and closes the form
            else if (this.rdbExpense.Checked)
            {
                this.UserChoice = 1;
                this.Close();
            }
            // Sets the local property with an indicator for income
            // and closes the form
            else if (this.rdbIncome.Checked)
            {
                this.UserChoice = 2;
                this.Close();
            }
        }

        /// <summary>
        /// If the user cancels sets the local property with the appropiate value
        /// and closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.UserChoice = 0;
            this.Close();
        }

        #endregion
    }
}
