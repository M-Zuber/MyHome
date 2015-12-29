using System;
using System.Windows.Forms;

namespace MyHome.UI
{
    /// <summary>
    /// Gives info about the code team
    /// </summary>
    public partial class AboutUI : Form
    {
        #region C'tor

        /// <summary>
        /// Basic default ctor
        /// </summary>
        public AboutUI()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Control Event Methods

        /// <summary>
        /// Closes the form on the click of the okay button
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void btnOkay_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
