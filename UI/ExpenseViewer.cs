using System;
using System.Windows.Forms;
using BusinessLogic;
using Data;
using DataAccess;
using LocalTypes;

namespace MyHome2013
{
    /// <summary>
    ///     Shows data on a single expense, allowing the user to edit it
    /// </summary>
    public partial class ExpenseViewer : Form
    {
        #region C'Tor

        /// <summary>
        ///     Sets the intial state and current state expense properties of the form
        /// </summary>
        /// <param name="expense">The expense the form was opened for</param>
        public ExpenseViewer(Expense expense)
        {
            currentExpense = expense;

            // Makes a shallow copy of the expense passed in
            originalExpense = currentExpense.Copy();
            InitializeComponent();

            _dataContext = new AccountingDataContext();
            _expenseService = new ExpenseService(new ExpenseRepository(_dataContext));
            _expenseCategoryService = new ExpenseCategoryService(new ExpenseCategoryRepository(_dataContext));
            _paymentMethodService = new PaymentMethodService(new PaymentMethodRepository(_dataContext));
        }

        #endregion

        #region Properties

        /// <summary>
        ///     A copy of the expense of the form, to keep track of changes
        /// </summary>
        private readonly Expense originalExpense;

        /// <summary>
        ///     The current state the expense of the form is in
        /// </summary>
        private Expense currentExpense;

        private readonly AccountingDataContext _dataContext;
        private readonly ExpenseCategoryService _expenseCategoryService;
        private readonly ExpenseService _expenseService;
        private readonly PaymentMethodService _paymentMethodService;

        #endregion

        #region Event Methods

        /// <summary>
        ///     Sets the data bindings of the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExpenseViewer_Load(object sender, EventArgs e)
        {
            SetDataBindings();
        }

        /// <summary>
        ///     Saves the current state of the expense -if it is different than the origional state
        ///     -closes the form after the save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!currentExpense.Equals(originalExpense))
            {
                _expenseService.Save(currentExpense);

                Close();
            }
        }

        /// <summary>
        ///     Updates the expense with a new expense category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentExpense.Category = (ExpenseCategory) cmbCategory.SelectedItem;
        }

        /// <summary>
        ///     Updates the expense with a new payment method
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentExpense.Method = (PaymentMethod) cmbPayment.SelectedItem;
        }

        /// <summary>
        ///     Updates the expense with a new amount
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                MessageBox.Show("The amount can not be blank",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            // Checks that the amount is in numbers
            else if (!HelperMethods.IsNumeric(txtAmount.Text))
            {
                MessageBox.Show("The amount must be in numbers",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                txtAmount.Text = "";
            }
            else
            {
                currentExpense.Amount = decimal.Parse(txtAmount.Text);
            }
        }

        /// <summary>
        ///     Updates the expense with a new comment
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void txtDetail_TextChanged(object sender, EventArgs e)
        {
            currentExpense.Comment = txtDetail.Text;
        }

        /// <summary>
        ///     Updates the expense with a new date
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            currentExpense.Date = dtPick.Value;
        }

        /// <summary>
        ///     Enables editing
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Enables the controls for editing and updates which buttons are visible
            ToggleEnableControls(txtAmount, txtDetail, cmbCategory,
                cmbPayment, dtPick, btnSave, btnEdit, btnCancel, btnDelete);
            ToggleVisibility(btnSave, btnCancel, btnEdit, btnDelete);
        }

        /// <summary>
        ///     Cancels the edit and leaves the form open
        ///     -clears any changes made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Enables the controls for editing and updates which buttons are visible
            ToggleEnableControls(txtAmount, txtDetail, cmbCategory,
                cmbPayment, dtPick, btnSave, btnEdit, btnCancel, btnDelete);
            ToggleVisibility(btnSave, btnCancel, btnEdit, btnDelete);

            // Makes sure that the expense of the binding has the origional values
            currentExpense = originalExpense.Copy();

            // Resets the data bindings
            SetDataBindings();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var canDelete =
                MessageBox.Show("Are you sure you want to delete this expense?\n" +
                                "Once done, it can not be undone!",
                    "Deleting...",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

            if (canDelete == DialogResult.OK)
            {
                _expenseService.Delete(currentExpense.Id);
                Close();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        ///     Sets the data bindings of the form,
        ///     including the selected value of the combo boxes, and events
        /// </summary>
        private void SetDataBindings()
        {
            //Simple control bindings
            txtAmount.Text = currentExpense.Amount.ToString();
            txtDetail.Text = currentExpense.Comment;
            dtPick.Value = currentExpense.Date;

            //Expense category bindings
            cmbCategory.DataSource = _expenseCategoryService.LoadAll();
            cmbCategory.DisplayMember = "NAME";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = cmbCategory.FindString(currentExpense.Category.Name);

            //Payment Method bindings
            cmbPayment.DataSource = _paymentMethodService.LoadAll();
            cmbPayment.DisplayMember = "NAME";
            cmbPayment.ValueMember = "ID";
            cmbPayment.SelectedIndex = cmbPayment.FindString(currentExpense.Method.Name);

            //Event Bindings
            // This is to keep events from firing until all the data bindings are fully set
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            cmbPayment.SelectedIndexChanged += cmbPayment_SelectedIndexChanged;
            txtAmount.TextChanged += txtAmount_TextChanged;
            txtDetail.TextChanged += txtDetail_TextChanged;
            dtPick.ValueChanged += dtPick_ValueChanged;
        }

        /// <summary>
        ///     Toggles the enable property of the controls sent
        /// </summary>
        /// <param name="controls">A list of controls to enable/disable</param>
        private void ToggleEnableControls(params Control[] controls)
        {
            foreach (var CurrControl in controls)
            {
                CurrControl.Enabled = !CurrControl.Enabled;
            }
        }

        /// <summary>
        ///     Toggles the visible property of the controls sent
        /// </summary>
        /// <param name="controls">A list of controls to show/hide</param>
        private void ToggleVisibility(params Control[] controls)
        {
            foreach (var CurrControl in controls)
            {
                CurrControl.Visible = !CurrControl.Visible;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _dataContext?.Dispose();
        }

        #endregion
    }
}