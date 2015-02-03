using System;
using System.Linq;
using System.Windows.Forms;
using MyHome2013.Core.LocalTypes;
using System.ComponentModel;
using System.Collections.Generic;

namespace MyHome2013
{
    /// <summary>
    /// Presents a list of category items from the given category group
    ///  allows the user the option of adding new items
    /// </summary>
    public partial class ViewCategoriesUI : Form
    {
        static Tuple<string, ListSortDirection>[] baseSorting = new[]
        {
            Tuple.Create("Id", ListSortDirection.Ascending),
            Tuple.Create("Name", ListSortDirection.Ascending)
        };

        /// <summary>
        /// A placeholder for the original value of a category the is being edited
        /// </summary>
        object CellPreEditValue = null;
        SortableBindingList<ExpenseCategory> edata = new SortableBindingList<ExpenseCategory>(baseSorting);
        SortableBindingList<IncomeCategory> idata = new SortableBindingList<IncomeCategory>(baseSorting);
        SortableBindingList<PaymentMethod> pmdata = new SortableBindingList<PaymentMethod>(baseSorting);

        #region C'tor

        /// <summary>
        /// C'tor that intializes the category group property
        /// </summary>
        /// <param name="nCategoryId">The category group id</param>
        public ViewCategoriesUI()
        {
            edata.AllowNew = true;
            idata.AllowNew = true;
            pmdata.AllowNew = true;

            InitializeComponent();
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        /// Connects the data viewer on the form with the table of options in the category group
        /// currently associated with the form
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void ViewCategoriesUI_Load(object sender, EventArgs e)
        {
            this.dgvExpenseCategoryNames.DataSource = edata;
            this.dgvExpenseCategoryNames.Columns[0].Visible = false;

            this.dgvIncomeCategoryNames.DataSource = idata;
            this.dgvIncomeCategoryNames.Columns[0].Visible = false;

            this.dgvPaymentMethodNames.DataSource = pmdata;
            this.dgvPaymentMethodNames.Columns[0].Visible = false;

            loadExpenseCategoryData();
            loadIncomeCategoryData();
            loadPaymentMethodData();
        }

        private void CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.CellPreEditValue = (sender as DataGridView).CurrentCell.Value;
        }

        private void dgvExpenseCategoryNames_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool result = CellEndEdit<ExpenseCategory>(edata, sender, e);
            if (result) loadExpenseCategoryData();
        }

        private void dgvIncomeCategoryNames_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool result = CellEndEdit<IncomeCategory>(idata, sender, e);
            if (result) loadIncomeCategoryData();
        }

        private void dgvPaymentMethodNames_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool result = CellEndEdit<PaymentMethod>(pmdata, sender, e);
            if (result) loadPaymentMethodData();
        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadExpenseCategoryData()
        {
            var ech = Program.Container.GetInstance<IRepository<ExpenseCategory>>();
            edata.Load(ech.LoadAll());
        }

        private void loadIncomeCategoryData()
        {
            var ich = Program.Container.GetInstance<IRepository<IncomeCategory>>();
            idata.Load(ich.LoadAll());
        }

        private void loadPaymentMethodData()
        {
            var pmh = Program.Container.GetInstance<IRepository<PaymentMethod>>();
            pmdata.Load(pmh.LoadAll());
        }

        private bool CellEndEdit<T>(IEnumerable<T> data, object sender, DataGridViewCellEventArgs e)
            where T : BaseCategory, new()
        {
            var grid = sender as DataGridView;
            var item = grid.CurrentRow.DataBoundItem as T;
            var newValue = grid.CurrentCell.Value as string;

            grid.CurrentCell.Value = CellPreEditValue;

            // The value was not changed
            if (newValue == CellPreEditValue as string)
                return false;

            // Filter the source data, to remove the current item, for the duplicate check
            var d = data;
            if (item != null) d = d.Where(x => x.Id != item.Id);

            // The new value is either empty or duplicates an existing value
            if (string.IsNullOrWhiteSpace(newValue) || d.Any(x => x.Name == newValue.Trim()))
            {
                if (item != null)
                    grid.CurrentCell.Value = CellPreEditValue;
                else
                    grid.Rows.Remove(grid.CurrentRow);

                return false;
            }

            // Trim existing values before saving to prevent extra whitespace
            if (item != null) item.Name = item.Name.Trim();

            var h = Program.Container.GetInstance<IRepository<T>>();
            h.Save(item ?? new T { Name = newValue.Trim() });

            return true;
        }


    }
}
