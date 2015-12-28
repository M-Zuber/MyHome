using System;
using System.Windows.Forms;
using MyHome.Persistence;
using MyHome.Services;

namespace MyHome.UI
{
    /// <summary>
    /// Lets the user add a new category -the category group is dependant on where
    /// the form is opened from
    /// </summary>
    public partial class AddCategoryUI : Form
    {
        private readonly AccountingDataContext _context;
        private readonly CategoryService _categoryService;

        #region Properties

        /// <summary>
        /// Indicates what category group the new category is part of
        /// </summary>
        public CategoryType CategoryType { get; set; }
        
        #endregion

        #region C'tor

        /// <summary>
        /// Basic ctor that recieves an indicator of which category group
        ///  the category is being added to
        /// </summary>
        /// <param name="categoryType">Category group indicator</param>
        public AddCategoryUI(CategoryType categoryType)
        {
            InitializeComponent();
            CategoryType = categoryType;

            _context = new AccountingDataContext();
            _categoryService = new CategoryService(_context);
        }
        
        #endregion

        #region Control Event Methods

        /// <summary>
        /// Saves the category into the appropiate category group
        ///  Performs a validication to ensure that the category has a name
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // If the category name is blank shows the user an error message
            if (this.txtCategoryName.Text == "")
            {
                MessageBox.Show("Please fill in a name for the category",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                this.txtCategoryName.Focus();
            }
            else if (_categoryService.CategoryHandlers[this.CategoryType].Exists(txtCategoryName.Text))
            {
                MessageBox.Show("There can not be two  categories with the same name\n" +
                                "Please choose a new name",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                this.txtCategoryName.Text = "";
                this.txtCategoryName.Focus();
            }
            else
            {
                _categoryService.CategoryHandlers[this.CategoryType].Create(txtCategoryName.Text);

                Close();
            }
        } 

        /// <summary>
        /// When the form closes, asks if the user wants to add another category option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCategoryUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Checks if a category had been saved before asking if the user wants to add another one
            if (this.txtCategoryName.Text != "")
            {
                // Asks if more data is being entered
                DialogResult = MessageBox.Show("The entry was saved" +
                                               "\nDo you want to add another category option? ",
                                               "Save successful",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button1);

                // If yes cancels the forms close, and resets the text box
                if (DialogResult == DialogResult.Yes)
                {
                    e.Cancel = true;
                    this.txtCategoryName.Text = "";
                    
                    // Refocus the form on the text box
                    this.txtCategoryName.Focus();
                } 
            }
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
