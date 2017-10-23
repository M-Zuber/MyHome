using System;
using System.Windows.Forms;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    /// A form to allow the user to choose the type of item to open an add form for
    /// </summary>
    public partial class NewChoice : Form
    {
        /// <summary>
        /// The result of the users choice
        /// </summary>
        public int UserChoice { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Default Ctor
        /// </summary>
        public NewChoice()
        {
            InitializeComponent();
        }

        /// <summary>
        /// If the user press the okay button checks that a value is checked before setting
        /// the local property -if yes closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, EventArgs e)
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
            else if (rdbExpense.Checked)
            {
                UserChoice = 1;
                Close();
            }
            // Sets the local property with an indicator for income
            // and closes the form
            else if (rdbIncome.Checked)
            {
                UserChoice = 2;
                Close();
            }
        }

        /// <summary>
        /// If the user cancels sets the local property with the appropriate value
        /// and closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            UserChoice = 0;
            Close();
        }
    }
}
