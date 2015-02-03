using BusinessLogic;
using MyHome2013.Core.LocalTypes;
using System;
using System.Windows.Forms;

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
            else if (!HelperMethods.IsNumeric(this.txtAmount.Text))
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
                var r = Program.Container.GetInstance<IRepository<PaymentMethod>>();
                var ih = Program.Container.GetInstance<IncomeHandler>();
                var ich = Program.Container.GetInstance<IRepository<IncomeCategory>>();

                Income newIncome = 
                    new Income(decimal.Parse(this.txtAmount.Text), this.dtPick.Value,
                                ich.LoadById(Convert.ToInt32(this.cmbCategory.SelectedValue)),
                                r.LoadById(Convert.ToInt32(this.cmbPayment.SelectedValue)),
                                this.txtDetail.Text);

                ih.AddNewIncome(newIncome);

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
                    this.cmbCategory.Focus();
                    this.txtAmount.Text = "";
                    this.txtDetail.Text = "";
                }
            }
        } 

        #endregion

        #region Other Methods

        private void SetDataBindings()
        {
            // Sets up the combo box of the income categories
            var ich = Program.Container.GetInstance<IRepository<IncomeCategory>>();
            this.cmbCategory.DataSource = ich.LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";

            // Sets up the combo box with the payment methods
            var r = Program.Container.GetInstance<IRepository<PaymentMethod>>();
            this.cmbPayment.DataSource = r.LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
        }

        #endregion
    }
}
