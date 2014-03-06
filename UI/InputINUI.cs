using System;
using System.Windows.Forms;
using BL;
using Old_FrameWork;

namespace MyHome2013
{
    /// <summary>
    /// Enables the user to add new income data
    ///  allows for continuous data entry
    /// </summary>
    public partial class InputINUI : Form
    {
        #region C'tor

        /// <summary>
        /// Standard Default Ctor
        /// </summary>
        public InputINUI()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        /// Connects the combo boxes on the form with the data from the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void InputINUI_Load(object sender, EventArgs e)
        {
            // Sets up the combo box of the income categories
            this.cmbCategory.DataSource =
                Cache.SDB.t_incomes_category;
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";
            
            // Sets up the combo box with the payment methods
            this.cmbPayment.DataSource =
                Cache.SDB.t_payment_methods;
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
        }

        /// <summary>
        /// Saves the new income into the cache -validates the amount
        ///  and gives an option to enter more data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // If the amount is blank
            if (this.txtAmount.Text == "")
            {
                MessageBox.Show("The amount can not be blank",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            // Checks that the amount is in numbers
            else if (!GlobalBL.IsNumeric(this.txtAmount.Text))
            {
                MessageBox.Show("The amount must be in numbers",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                this.txtAmount.Text = "";
            }
            // Otherwise saves the new income
            else
            {
                // Creates a new income and sets the properties with the data from the form
                IncBL incNewInc = IncBL.CreateIncome();
                incNewInc.Amount = this.txtAmount.Text;
                incNewInc.Date = this.dtPick.Value.Date;
                incNewInc.Category =
                    Convert.ToInt32(this.cmbCategory.SelectedValue);
                incNewInc.Method =
                    Convert.ToInt32(this.cmbPayment.SelectedValue);
                incNewInc.Comment = this.txtDetail.Text;

                // Saves the new income into the cache
                incNewInc.Save();

                // Asks if more data is being entered
                DialogResult = MessageBox.Show("The entry was saved" +
                                               "\nDo you want to add another income? ",
                                               "Save successful",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button1);
                if (DialogResult != DialogResult.Yes)
                {
                    this.Close();
                }
                // If more data is being entered clears the user entered data for the new data
                else
                {
                    this.txtAmount.Text = "";
                    this.txtDetail.Text = "";

                    // Puts the focus back to the top of the form
                    this.cmbCategory.Focus();
                }
            }
        } 

        #endregion
    }
}
