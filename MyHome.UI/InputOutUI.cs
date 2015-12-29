using System;
using System.Windows.Forms;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using MyHome.UI.Helpers;

namespace MyHome.UI
{
    /// <summary>
    ///     Enables the user to add new expense data
    ///     allows for continuous data entry
    /// </summary>
    public partial class InputOutUI : Form
    {
        private readonly AccountingDataContext _dataContext;
        private readonly ExpenseCategoryService _expenseCategoryService;
        private readonly ExpenseService _expenseService;
        private readonly PaymentMethodService _paymentMethodService;

        #region C'tor

        /// <summary>
        ///     Standard Default Ctor
        /// </summary>
        public InputOutUI()
        {
            InitializeComponent();

            _dataContext = new AccountingDataContext();
            _expenseService = new ExpenseService(new ExpenseRepository(_dataContext));
            _expenseCategoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(_dataContext));
            _paymentMethodService = new PaymentMethodService(new PaymentMethodRepository(_dataContext));
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        ///     Connects the combo boxes on the form with the data from the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void InputOutUI_Load(object sender, EventArgs e)
        {
            SetDataBindings();
        }

        /// <summary>
        ///     Saves the new expense into the cache -validates the amount
        ///     and gives an option to enter more data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // If the amount is blank
            if (txtAmount.Text == "")
            {
                MessageBox.Show("The amount can not be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            // Checks that the amount is in numbers
            else if (!txtAmount.Text.IsDecimal())
            {
                MessageBox.Show("The amount must be in numbers",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                txtAmount.Text = "";
            }
            // Otherwise saves the new expense
            else
            {
                var newExpense =
                    new Expense(decimal.Parse(txtAmount.Text), dtPick.Value,
                        Convert.ToInt32(cmbCategory.SelectedValue),
                        Convert.ToInt32(cmbPayment.SelectedValue),
                        txtDetail.Text);

                _expenseService.Create(newExpense);

                // Asks if more data is being entered
                DialogResult = MessageBox.Show("The entry was saved" +
                                               "\nDo you want to add another expense? ",
                    "Save successful",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (DialogResult != DialogResult.Yes)
                {
                    Close();
                }
                // If more data is being entered clears the user entered data for the new data
                else
                {
                    // Puts the focus back to the top of the form and resets the selected values
                    cmbCategory.Focus();
                    txtAmount.Text = "";
                    txtDetail.Text = "";
                }
            }
        }

        #endregion

        #region Other Methods

        private void SetDataBindings()
        {
            // Sets up the combo box of the income categories
            cmbCategory.DataSource =
                _expenseCategoryService.GetAll();
            cmbCategory.DisplayMember = "NAME";
            cmbCategory.ValueMember = "ID";

            // Sets up the combo box with the payment methods
            cmbPayment.DataSource =
                _paymentMethodService.GetAll();
            cmbPayment.DisplayMember = "NAME";
            cmbPayment.ValueMember = "ID";
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (_dataContext != null)
            {
                _dataContext.Dispose();
            }
        }

        #endregion
    }
}