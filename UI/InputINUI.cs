using System;
using System.Windows.Forms;
using BusinessLogic;
using LocalTypes;

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
            this.SetDataBindings();
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
            else if (!BusinessLogic.HelperMethods.IsNumeric(this.txtAmount.Text))
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
                Income newIncome =
                    new Income(double.Parse(this.txtAmount.Text), this.dtPick.Value,
                                IncomeCategoryHandler.LoadById(Convert.ToInt32(this.cmbCategory.SelectedValue)),
                                PaymentMethodHandler.LoadById(Convert.ToInt32(this.cmbPayment.SelectedValue)),
                                this.txtDetail.Text);

                //Ask confirmation from the user in case it is a duplicate income
                bool saveIncome = true;
                if (IncomeHandler.IsDuplicate(newIncome))
                {
                    if (MessageBox.Show("The income already exists. Do you wish to save anyway?", "Confirmation",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                        saveIncome = false;
                }
                if (saveIncome)
                {
                    IncomeHandler.AddNewIncome(newIncome);

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
                        // Puts the focus back to the top of the form and resets the selected values
                        ClearIncomeFields();
                    }
                }
                else
                    ClearIncomeFields();
            }
        }
        #endregion

        #region Other Methods

        private void SetDataBindings()
        {
            // Sets up the combo box of the income categories
            this.cmbCategory.DataSource =
                (new IncomeCategoryHandler()).LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";

            // Sets up the combo box with the payment methods
            this.cmbPayment.DataSource =
                (new PaymentMethodHandler()).LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
        }

        private void ClearIncomeFields()
        {
              this.cmbCategory.Focus();
              this.txtAmount.Text = "";
              this.txtDetail.Text = "";
        }
        #endregion
    }
}
