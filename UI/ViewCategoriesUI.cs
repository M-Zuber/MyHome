using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic;
using LocalTypes;

namespace MyHome2013
{
    /// <summary>
    /// Presents a list of category items from the given category group
    ///  allows the user the option of adding new items
    /// </summary>
    public partial class ViewCategoriesUI : Form
    {
        #region Properties

        /// <summary>
        /// Represents which category group the form is displaying for
        /// </summary>
        public int CategoryType { get; set; }

        /// <summary>
        /// A placeholder for the original value of a category the is being edited
        /// </summary>
        public string OriginalCategoryName { get; set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// C'tor that intializes the category group property
        /// </summary>
        /// <param name="nCategoryId">The category group id</param>
        public ViewCategoriesUI(int nCategoryId)
        {
            // Sets the property with the id given
            this.CategoryType = nCategoryId;

            // Auto generated code for the form
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
            // Loads the table that corrosponds to the wanted categry group
            this.dgvCategoryNames.DataSource =
                GlobalHandler.CategoryHandlers[this.CategoryType].LoadAll();
            
            // Connects the data grid with the names only and displays the category group
            // name as the title of the form
            this.dgvCategoryNames.Columns[0].Visible = false;

            this.Text = GlobalHandler.CategoryTypeNames[this.CategoryType];
        }

        /// <summary>
        /// Opens the form for adding new categories as a dialog
        /// -passes the propery with the the category group id
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Opens a dialog of the form for adding new categories
            using (AddCategoryUI addNewCategory = new AddCategoryUI(this.CategoryType))
            {
                addNewCategory.ShowDialog();
            }

            // Refreshes the list so the new category is displayed
            this.dgvCategoryNames.DataSource = GlobalHandler.CategoryHandlers[this.CategoryType].LoadAll();
        } 

        private void dgvCategoryNames_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GlobalHandler.CategoryHandlers[this.CategoryType].LoadAll().FirstOrDefault(category => category.Name == this.dgvCategoryNames.CurrentCell.Value.ToString()) == null)
            {
                GlobalHandler.CategoryHandlers[this.CategoryType].Save((BaseCategory)this.dgvCategoryNames.CurrentCell.OwningRow.DataBoundItem);
            }
            else
            {
                this.dgvCategoryNames.CurrentCell.Value = this.OriginalCategoryName;
            }
        }

        private void dgvCategoryNames_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.OriginalCategoryName = this.dgvCategoryNames.CurrentCell.Value.ToString();
        }

        #endregion

    }
}
