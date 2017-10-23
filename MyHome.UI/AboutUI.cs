using System;
using System.Windows.Forms;

namespace MyHome.UI
{
    /// <inheritdoc />
    /// <summary>
    /// Gives info about the code team
    /// </summary>
    public partial class AboutUI : Form
    {
        /// <inheritdoc />
        /// <summary>
        /// Basic default ctor
        /// </summary>
        public AboutUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes the form on the click of the okay button
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event arg object</param>
        private void BtnOkay_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
