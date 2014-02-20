using System;
using System.Data;
using System.Windows.Forms;
using BL;
using FrameWork;

//TODO refactor the code here
namespace MyHome2013
{
    /// <summary>
    /// Provides the data on income and expenses for the selected month
    /// </summary>
    public partial class DataViewUI : Form
    {
        #region Properties

        /// <summary>
        /// Holds a category list to bind to the combo box
        /// </summary>
        public DataTable CategoryList { get; set; }

        #endregion

        #region C'tor

        /// <summary>
        /// Standard default Ctor
        /// </summary>
        public DataViewUI()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Control Event Methods

        /// <summary>
        /// Connects the controls on the form with the data from the cache
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void DataViewUI_Load(object sender, EventArgs e)
        {
            // If there is any changes in the data asks the user if they want to save them
            if (Cache.SDB.HasChanges())
            {
                DialogResult = MessageBox.Show("Differences between the data in the program " +
                                               "and the saved data where detected\n" + 
                                               "To view the most update to-date information please save",
                                               "Loading...",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button1);

                // If the user is saving the changes
                if (DialogResult == DialogResult.Yes)
                {
                    GlobalBL.SaveFromCache();
                }
            }
            // Garentees that i am starting with clear views before reloading them
            Cache.SDB.viwin.Clear();
            Cache.SDB.viw.Clear();

            // Automatically forces the window to be open to its max size
            this.WindowState = FormWindowState.Maximized;

            // Reloads the view to garentee that there is access to the most recent data
            GlobalBL.LoadToCache("viw");
            GlobalBL.LoadToCache("viwin");

            // Updates the data in the expense and income chart views
            this.dgOut.DataSource =
                DataAccess.ExpenseEntity.LoadOfMonth(dtPick.Value);
            this.dgOut.Columns[this.dgOut.Columns.Count - 1].Visible = false;

            this.dgIn.DataSource =
                Cache.SDB.viwin.SearchByMonth(dtPick.Value);

            // Pulls up a table with the sup income, expenses, and within each expense category
            // and connects it with the combo box (and text box - to display the total)
            this.CategoryList = new MonthViewBL(this.dtPick.Value).CuttingAll();
            this.DataBindingSetup();
        }

        /// <summary>
        /// Resets the data in the form based on the users date choice
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void dtPick_ValueChanged(object sender, EventArgs e)
        {
            this.DataBinding();
        }

        private void DataBinding()
        {
            // Garentees that i am starting with clear views before reloading them
            Cache.SDB.viwin.Clear();
            Cache.SDB.viw.Clear();

            // Reloads the view to garentee that there is access to the most recent data
            GlobalBL.LoadToCache("viw");
            GlobalBL.LoadToCache("viwin");

            // Updates the data in the expense and income chart views
            this.dgOut.DataSource =
                DataAccess.ExpenseEntity.LoadOfMonth(dtPick.Value);
            this.dgIn.DataSource =
                Cache.SDB.viwin.SearchByMonth(this.dtPick.Value);

            // Refreshes the data table with the category list and refreshes the data bindings
            this.CategoryList = new MonthViewBL(this.dtPick.Value).CuttingAll();
            this.DataBindingSetup();
        } 

        #endregion

        #region Other Methods

        /// <summary>
        /// Sets up the data bindings to the combo box and the text box
        /// Refreshes also -by deleting and rebinding
        /// </summary>
        private void DataBindingSetup()
        {
            // Clears any old data bindings
            this.cmbCategory.DataSource = null;
            this.txtCategoryTotal.DataBindings.Clear();

            // Binds the data table with the list of categorys
            // and binds the text box to display the total for the given category
            this.cmbCategory.DataSource = this.CategoryList;
            this.cmbCategory.DisplayMember = "KEY";
            this.txtCategoryTotal.DataBindings.Add("Text", this.CategoryList, "VALUE");
        }

        #endregion

        private void dgOut_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            using (ExpenseViewer viewAndEditExpense = 
                new ExpenseViewer((DataAccess.ExpenseEntity)this.dgOut.CurrentCell.OwningRow.DataBoundItem))
            {
                viewAndEditExpense.ShowDialog();
            }

            this.DataBinding();
        }
    }
}
