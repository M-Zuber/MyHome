using BusinessLogic;
using MyHome2013.Core.LocalTypes;
using System;
using System.Windows.Forms;

namespace MyHome2013
{
    /// <summary>
    /// Enables the user to add new expense data
    ///  allows for continuous data entry
    /// </summary>
    public partial class InputOutUI : Form
    {
        #region C'tor

        /// <summary>
        /// Standard Default Ctor
        /// </summary>
        public InputOutUI()
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
        private void InputOutUI_Load(object sender, EventArgs e)
        {
            this.SetDataBindings();
        } 

        /// <summary>
        /// Saves the new expense into the cache -validates the amount
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
            // Otherwise saves the new expense
            else
            {
                var pmr = Program.Container.GetInstance<IRepository<PaymentMethod>>();
                var ecr = Program.Container.GetInstance<IRepository<ExpenseCategory>>();
                var er = Program.Container.GetInstance<ExpenseHandler>();

                Expense newExpense =
                    new Expense(decimal.Parse(this.txtAmount.Text), this.dtPick.Value,
                                ecr.LoadById(Convert.ToInt32(this.cmbCategory.SelectedValue)),
                                pmr.LoadById(Convert.ToInt32(this.cmbPayment.SelectedValue)),
                                this.txtDetail.Text);

                er.AddNewExpense(newExpense);

                // Asks if more data is being entered
                DialogResult = MessageBox.Show("The entry was saved" +
                                               "\nDo you want to add another expense? ",
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
            var pmh = Program.Container.GetInstance<IRepository<PaymentMethod>>();
            var ech = Program.Container.GetInstance<IRepository<ExpenseCategory>>();

            // Sets up the combo box of the income categories
            this.cmbCategory.DataSource = ech.LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";

            // Sets up the combo box with the payment methods
            this.cmbPayment.DataSource = pmh.LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
        }

        #endregion
    }
}
