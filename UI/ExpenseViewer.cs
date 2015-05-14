﻿using System;
using System.Windows.Forms;
using BusinessLogic;
using LocalTypes;

namespace MyHome2013
{
    /// <summary>
    /// Shows data on a single expense, allowing the user to edit it
    /// </summary>
    public partial class ExpenseViewer : Form
    {
        #region Properties

        /// <summary>
        /// A copy of the expense of the form, to keep track of changes
        /// </summary>
        private Expense originalExpense;

        /// <summary>
        /// The current state the expense of the form is in
        /// </summary>
        private Expense currentExpense;

        #endregion

        #region C'Tor

        /// <summary>
        /// Sets the intial state and current state expense properties of the form
        /// </summary>
        /// <param name="expense">The expense the form was opened for</param>
        public ExpenseViewer(Expense expense)
        {
            this.currentExpense = expense;

            // Makes a shallow copy of the expense passed in
            this.originalExpense = this.currentExpense.Copy();
            InitializeComponent();
        }
        
        #endregion

        #region Event Methods

        /// <summary>
        /// Sets the data bindings of the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ExpenseViewer_Load(object sender, EventArgs e)
        {
            SetDataBindings();
        }

        /// <summary>
        /// Saves the current state of the expense -if it is different than the origional state
        /// -closes the form after the save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
       
            if (!currentExpense.Equals(originalExpense))
            {
                ExpenseHandler.Save(this.currentExpense);

                this.Close();
            }
        }

        /// <summary>
        /// Updates the expense with a new expense category
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentExpense.Category = (ExpenseCategory)this.cmbCategory.SelectedItem;
        }

        /// <summary>
        /// Updates the expense with a new payment method 
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentExpense.Method = (PaymentMethod)this.cmbPayment.SelectedItem;
        }

        /// <summary>
        /// Updates the expense with a new amount
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
                this.currentExpense.Amount = double.Parse(this.txtAmount.Text);
            }
        }

        /// <summary>
        /// Updates the expense with a new comment
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void txtDetail_TextChanged(object sender, EventArgs e)
        {
            this.currentExpense.Comment = this.txtDetail.Text;
        }

        /// <summary>
        /// Updates the expense with a new date
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            this.currentExpense.Date = this.dtPick.Value;
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
            this.currentExpense = this.originalExpense.Copy();

            // Resets the data bindings
            this.SetDataBindings();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult canDelete = 
                MessageBox.Show("Are you sure you want to delete this expense?\n" +
                                "Once done, it can not be undone!",
                                "Deleting...",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2);

            if (canDelete == DialogResult.OK)
            {
                ExpenseHandler.Delete(this.currentExpense.ID);
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
            this.txtAmount.Text = currentExpense.Amount.ToString();
            this.txtDetail.Text = currentExpense.Comment;
            this.dtPick.Value = currentExpense.Date;

            //Expense category bindings
            this.cmbCategory.DataSource = (new ExpenseCategoryHandler()).LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";
            this.cmbCategory.SelectedIndex = this.cmbCategory.FindString(this.currentExpense.Category.Name);

            //Payment Method bindings
            this.cmbPayment.DataSource = (new PaymentMethodHandler()).LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
            this.cmbPayment.SelectedIndex = this.cmbPayment.FindString(this.currentExpense.Method.Name);

            //Event Bindings
            // This is to keep events from firing until all the data bindings are fully set
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
