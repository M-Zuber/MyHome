using System;
using System.Globalization;
using System.Windows.Forms;
using MyHome.DataClasses;
using MyHome.DataRepository;
using MyHome.Persistence;
using MyHome.Services;
using MyHome.UI.Helpers;

namespace MyHome.UI
{
    /// <summary>
    ///     Shows data on a single income, allowing the user to edit it
    /// </summary>
    public partial class IncomeViewer : Form
    {
        /// <summary>
        ///     Sets the initial state and current state expense properties of the form
        /// </summary>
        /// <param name="income">The income the form was opened for</param>
        public IncomeViewer(Income income)
        {
            InitializeComponent();

            _currentIncome = income;
            _originalIncome = _currentIncome.Copy();

            _dataContext = new AccountingDataContext();
            _incomeService = new IncomeService(new IncomeRepository(_dataContext));
            _incomeCategoryService = new IncomeCategoryService(new IncomeCategoryRepository(_dataContext));
            _paymentMethodService = new PaymentMethodService(new PaymentMethodRepository(_dataContext));
        }

        /// <summary>
        ///     A copy of the income of the form, to keep track of changes
        /// </summary>
        private readonly Income _originalIncome;

        /// <summary>
        ///     The current state the income of the form is in
        /// </summary>
        private Income _currentIncome;

        private readonly AccountingDataContext _dataContext;

        private readonly IncomeService _incomeService;

        private readonly IncomeCategoryService _incomeCategoryService;

        private readonly PaymentMethodService _paymentMethodService;

        /// <summary>
        ///     Sets the data bindings of the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void IncomeViewer_Load(object sender, EventArgs e)
        {
            SetDataBindings();
        }

        /// <summary>
        ///     Saves the current state of the income -if it is different than the original state
        ///     -closes the form after the save
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_currentIncome.Equals(_originalIncome))
            {
                _incomeService.Save(_currentIncome);

                Close();
            }
        }

        /// <summary>
        ///     Updates the income with a new income category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentIncome.Category = (IncomeCategory) cmbCategory.SelectedItem;
        }

        /// <summary>
        ///     Updates the income with a new payment method
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void CmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentIncome.Method = (PaymentMethod) cmbPayment.SelectedItem;
        }

        /// <summary>
        ///     Updates the income with a new amount
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void TxtAmount_TextChanged(object sender, EventArgs e)
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
            else if (!txtAmount.Text.IsDecimal())
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
                _currentIncome.Amount = decimal.Parse(txtAmount.Text);
            }
        }

        /// <summary>
        ///     Updates the income with a new comment
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void TxtDetail_TextChanged(object sender, EventArgs e)
        {
            _currentIncome.Comments = txtDetail.Text;
        }

        /// <summary>
        ///     Updates the income with a new date
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DtPick_ValueChanged(object sender, EventArgs e)
        {
            _currentIncome.Date = dtPick.Value;
        }

        /// <summary>
        ///     Enables editing
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void BtnEdit_Click(object sender, EventArgs e)
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
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Enables the controls for editing and updates which buttons are visible
            ToggleEnableControls(txtAmount, txtDetail, cmbCategory,
                cmbPayment, dtPick, btnSave, btnEdit, btnCancel, btnDelete);
            ToggleVisibility(btnSave, btnCancel, btnEdit, btnDelete);

            // Makes sure that the expense of the binding has the original values
            _currentIncome = _originalIncome.Copy();

            // Resets the data bindings
            SetDataBindings();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var canDelete =
                MessageBox.Show("Are you sure you want to delete this income?\n" +
                                "Once done, it can not be undone!",
                    "Deleting...",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

            if (canDelete == DialogResult.OK)
            {
                _incomeService.Delete(_currentIncome.Id);
                Close();
            }
        }

        /// <summary>
        ///     Sets the data bindings of the form,
        ///     including the selected value of the combo boxes, and events
        /// </summary>
        private void SetDataBindings()
        {
            //Simple control bindings
            txtAmount.Text = _currentIncome.Amount.ToString(CultureInfo.InvariantCulture);
            txtDetail.Text = _currentIncome.Comments;
            dtPick.Value = _currentIncome.Date;

            //Expense category bindings
            cmbCategory.DataSource = _incomeCategoryService.GetAll();
            cmbCategory.DisplayMember = "NAME";
            cmbCategory.ValueMember = "ID";
            cmbCategory.SelectedIndex = cmbCategory.FindString(_currentIncome.Category.Name);

            //Payment Method bindings
            cmbPayment.DataSource = _paymentMethodService.GetAll();
            cmbPayment.DisplayMember = "NAME";
            cmbPayment.ValueMember = "ID";
            cmbPayment.SelectedIndex = cmbPayment.FindString(_currentIncome.Method.Name);

            //Event Bindings
            // This is to keep events firing until all the data bindings are fully set
            cmbCategory.SelectedIndexChanged += CmbCategory_SelectedIndexChanged;
            cmbPayment.SelectedIndexChanged += CmbPayment_SelectedIndexChanged;
            txtAmount.TextChanged += TxtAmount_TextChanged;
            txtDetail.TextChanged += TxtDetail_TextChanged;
            dtPick.ValueChanged += DtPick_ValueChanged;
        }

        /// <summary>
        ///     Toggles the enable property of the controls sent
        /// </summary>
        /// <param name="controls">A list of controls to enable/disable</param>
        private static void ToggleEnableControls(params Control[] controls)
        {
            foreach (var currControl in controls)
            {
                currControl.Enabled = !currControl.Enabled;
            }
        }

        /// <summary>
        ///     Toggles the visible property of the controls sent
        /// </summary>
        /// <param name="controls">A list of controls to show/hide</param>
        private static void ToggleVisibility(params Control[] controls)
        {
            foreach (var currControl in controls)
            {
                currControl.Visible = !currControl.Visible;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _dataContext?.Dispose();
        }
    }
}