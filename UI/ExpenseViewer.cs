using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataAccess;

namespace MyHome2013
{
    /*
     * check what happens when the value of the controls are changed
     *      does it change the value in currentExpense??
     *  if not
     *      on each value change/ on press of save
     *          update values
     *          save using a method (yet to be written) of ExpenseEntity
     * 
     * should probably add an edit button which will enable the 
     *  controls and the save button
     */
    public partial class ExpenseViewer : Form
    {
        private ExpenseEntity originalExpense;
        private ExpenseEntity currentExpense;

        public ExpenseViewer(ExpenseEntity expense)
        {
            currentExpense = expense;
            originalExpense = currentExpense.Copy();
            InitializeComponent();
        }

        private void ExpenseViewer_Load(object sender, EventArgs e)
        {
            SetDataBindings();
        }

        private void SetDataBindings()
        {
            //Data Bindings
            this.txtAmount.Text = currentExpense.Amount.ToString();
            this.txtDetail.Text = currentExpense.Comment;
            this.dtPick.Value = currentExpense.Date;
            
            this.cmbCategory.DataSource = ExpenseCategoryEntity.LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";
            this.cmbCategory.SelectedValue = ExpenseCategoryEntity.LoadAll().First(ec => ec.ID == currentExpense.Category.ID).ID;
            

            this.cmbPayment.DataSource = PaymentMethodEntity.LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
            this.cmbPayment.SelectedValue = PaymentMethodEntity.LoadAll().First(pm => pm.ID == currentExpense.Method.ID).ID;

            //Event Bindings
            this.cmbCategory.SelectedIndexChanged += this.cmbCategory_SelectedIndexChanged;
            this.cmbPayment.SelectedIndexChanged += this.cmbPayment_SelectedIndexChanged;
            this.txtAmount.TextChanged += this.txtAmount_TextChanged;
            this.txtDetail.TextChanged += this.txtDetail_TextChanged;
            this.dtPick.ValueChanged += this.dtPick_ValueChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!currentExpense.Equals(originalExpense))
            {
                bool result = this.currentExpense.Save();
                MessageBox.Show(result.ToString());
            }
            else
            {
                MessageBox.Show("no diff");
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentExpense.Category = (ExpenseCategoryEntity)this.cmbCategory.SelectedItem;
        }

        private void cmbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.currentExpense.Method = (PaymentMethodEntity)this.cmbPayment.SelectedItem;
        }

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
            else if (!BL.GlobalBL.IsNumeric(this.txtAmount.Text))
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

        private void txtDetail_TextChanged(object sender, EventArgs e)
        {
            this.currentExpense.Comment = this.txtDetail.Text;
        }

        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            this.currentExpense.Date = this.dtPick.Value;
        }
    }
}
