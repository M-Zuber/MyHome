using BusinessLogic;
using MyHome2013.Core.LocalTypes;
using System;
using System.Windows.Forms;

namespace MyHome2013
{
    /// <summary>
    /// Shows data on a single income, allowing the user to edit it
    /// </summary>
    public partial class IncomeViewer : Form
    {
        #region Properties

        /// <summary>
        /// A copy of the income of the form, to keep track of changes
        /// </summary>
        private Income originalIncome;

        /// <summary>
        /// The current state the income of the form is in
        /// </summary>
        private Income currentIncome;

        #endregion

        #region C'Tor

        /// <summary>
        /// Sets the intial state and current state expense properties of the form
        /// </summary>
        /// <param name="income">The income the form was opened for</param>
        public IncomeViewer(Income income)
        {
            this.currentIncome = income;

            // Makes a shallow copy of the income passed in
            this.originalIncome = this.currentIncome.Copy();
            InitializeComponent();
        }
        
        #endregion

        #region Event Methods

        /// <summary>
        /// Sets the data bindings of the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void IncomeViewer_Load(object sender, EventArgs e)
        {
            SetDataBindings();
        }

        /// <summary>
        /// Saves the current state of the income -if it is different than the original state
        /// -closes the form after the save
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!currentIncome.Equals(originalIncome))
            {
                var ih = Program.Container.GetInstance<IncomeHandler>();
                ih.Save(this.currentIncome);

                this.Close();
            }
        }

        /// <summary>
        /// Updates the income with a new income category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentIncome.Category = (IncomeCategory)this.cmbCategory.SelectedItem;
        }

        /// <summary>
        /// Updates the income with a new payment method 
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentIncome.Method = (PaymentMethod)this.cmbPayment.SelectedItem;
        }

        /// <summary>
        /// Updates the income with a new amount
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
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
            else
            {
                this.currentIncome.Amount = double.Parse(this.txtAmount.Text);
            }
        }

        /// <summary>
        /// Updates the income with a new comment
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void txtDetail_TextChanged(object sender, EventArgs e)
        {
            this.currentIncome.Comment = this.txtDetail.Text;
        }

        /// <summary>
        /// Updates the income with a new date
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            this.currentIncome.Date = this.dtPick.Value;
        }

        /// <summary>
        /// Enables editing
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Enables the controls for editing and updates which buttons are visible
            this.ToggleEnableControls(this.txtAmount, this.txtDetail, this.cmbCategory,
                this.cmbPayment, this.dtPick, this.btnSave, this.btnEdit, this.btnCancel, this.btnDelete);
            this.ToggleVisibility(this.btnSave, this.btnCancel, this.btnEdit, this.btnDelete);
        }

        /// <summary>
        /// Cancels the edit and leaves the form open
        /// -clears any changes made
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Enables the controls for editing and updates which buttons are visible
            this.ToggleEnableControls(this.txtAmount, this.txtDetail, this.cmbCategory,
                this.cmbPayment, this.dtPick, this.btnSave, this.btnEdit, this.btnCancel, this.btnDelete);
            this.ToggleVisibility(this.btnSave, this.btnCancel, this.btnEdit, this.btnDelete);

            // Makes sure that the expense of the binding has the origional values
            this.currentIncome = this.originalIncome.Copy();

            // Resets the data bindings
            this.SetDataBindings();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult canDelete =
                MessageBox.Show("Are you sure you want to delete this income?\n" +
                                "Once done, it can not be undone!",
                                "Deleting...",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2);

            if (canDelete == DialogResult.OK)
            {
                var ih = Program.Container.GetInstance<IncomeHandler>();
                ih.Delete(this.currentIncome.ID);
                this.Close();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Sets the data bindings of the form,
        /// including the selected value of the combo boxes, and events
        /// </summary>
        private void SetDataBindings()
        {
            //Simple control bindings
            this.txtAmount.Text = currentIncome.Amount.ToString();
            this.txtDetail.Text = currentIncome.Comment;
            this.dtPick.Value = currentIncome.Date;

            //Expense category bindings
            var ich = Program.Container.GetInstance<IRepository<IncomeCategory>>();
            this.cmbCategory.DataSource = ich.LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";
            this.cmbCategory.SelectedIndex = this.cmbCategory.FindString(this.currentIncome.Category.Name);

            //Payment Method bindings
            var r = Program.Container.GetInstance<IRepository<PaymentMethod>>();
            this.cmbPayment.DataSource = r.LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
            this.cmbPayment.SelectedIndex = this.cmbPayment.FindString(this.currentIncome.Method.Name);

            //Event Bindings
            // This is to keep events firing until all the data bindings are fully set
            this.cmbCategory.SelectedIndexChanged += this.cmbCategory_SelectedIndexChanged;
            this.cmbPayment.SelectedIndexChanged += this.cmbPayment_SelectedIndexChanged;
            this.txtAmount.TextChanged += this.txtAmount_TextChanged;
            this.txtDetail.TextChanged += this.txtDetail_TextChanged;
            this.dtPick.ValueChanged += this.dtPick_ValueChanged;
        }

        /// <summary>
        /// Toggles the enable property of the controls sent
        /// </summary>
        /// <param name="controls">A list of controls to enable/disable</param>
        private void ToggleEnableControls(params Control[] controls)
        {
            foreach (Control CurrControl in controls)
            {
                CurrControl.Enabled = !CurrControl.Enabled;
            }
        }

        /// <summary>
        /// Toggles the visible property of the controls sent
        /// </summary>
        /// <param name="controls">A list of controls to show/hide</param>
        private void ToggleVisibility(params Control[] controls)
        {
            foreach (Control CurrControl in controls)
            {
                CurrControl.Visible = !CurrControl.Visible;
            }
        }

        #endregion
    }
}
