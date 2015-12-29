using System;
using System.Linq;
using System.Windows.Forms;
using MyHome.DataClasses;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.UI
{

    /// <summary>
    /// Presents a list of category items from the given category group
    ///  allows the user the option of adding new items
    /// </summary>
    public partial class ViewCategoriesUI : Form
    {

        private readonly AccountingDataContext _context;
        private readonly CategoryService _categoryService;
        #region Properties

        /// <summary>
        /// Represents which category group the form is displaying for
        /// </summary>
        public CategoryType CategoryType { get; set; }

        /// <summary>
        /// A placeholder for the original value of a category the is being edited
        /// </summary>
        public string OriginalCategoryName { get; set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// C'tor that intializes the category group property
        /// </summary>
        /// <param name="categoryType">The category type</param>
        public ViewCategoriesUI(Services.CategoryType categoryType)
        {
            // Sets the property with the id given
            CategoryType = categoryType;

            // Auto generated code for the form
            InitializeComponent();

            _context = new AccountingDataContext();
            _categoryService = new CategoryService(_context);
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
            dgvCategoryNames.DataSource =
                _categoryService.CategoryHandlers[CategoryType].GetAll();

            // Connects the data grid with the names only and displays the category group
            // name as the title of the form
            dgvCategoryNames.Columns[0].Visible = false;

            Text = _categoryService.CategoryTypeNames[CategoryType];
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
            using (AddCategoryUI addNewCategory = new AddCategoryUI(CategoryType))
            {
                addNewCategory.ShowDialog();
            }

            // Refreshes the list so the new category is displayed
            dgvCategoryNames.DataSource = _categoryService.CategoryHandlers[CategoryType].GetAll();
        } 

        private void dgvCategoryNames_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_categoryService.CategoryHandlers[CategoryType].GetAll().FirstOrDefault(category => category.Name == dgvCategoryNames.CurrentCell.Value.ToString()) == null)
            {
                var editedItem = (DataClasses.Category)dgvCategoryNames.CurrentCell.OwningRow.DataBoundItem;
                _categoryService.CategoryHandlers[(Services.CategoryType)CategoryType].Create(editedItem.Name);
            }
            else
            {
                dgvCategoryNames.CurrentCell.Value = OriginalCategoryName;
            }
        }

        private void dgvCategoryNames_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            OriginalCategoryName = dgvCategoryNames.CurrentCell.Value.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (_context != null)
            {
                _context.Dispose();
            }
        }


        #endregion

    }
}
