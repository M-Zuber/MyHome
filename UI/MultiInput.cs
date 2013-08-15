using System;
using System.Windows.Forms;
using BL;
using FrameWork;

namespace MyHome2013
{
    public partial class MultiInput : Form
    {
        #region C'Tor
        
        public MultiInput()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Event Methods
        
        private void MultiInput_Load(object sender, EventArgs e)
        {
            // Sets up the combo box of the income categories
            this.cmbCategory.DataSource =
                Cache.SDB.t_expenses_category;
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";

            // Sets up the combo box with the payment methods
            this.cmbPayment.DataSource =
                Cache.SDB.t_payment_methods;
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // If the amount is blank
            if (this.txtAmount.Text == "")
            {
                MessageBox.Show("The amount can not be blank",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            // Checks that the amount is in numbers
            else if (!GlobalBL.IsNumeric(this.txtAmount.Text))
            {
                MessageBox.Show("The amount must be in numbers",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                this.txtAmount.Text = "";
            }
            else if (this.GetRecurrenceFrequency() == "none")
            {
                MessageBox.Show("This form is for entering expenses that recurr\n" +
                                "Please choose a recurrence frequency",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            // Otherwise saves the new expense
            else
            {
                this.MultiSave();

                // Asks if more data is being entered
                DialogResult = MessageBox.Show("The entries where saved" +
                                               "\nDo you want to add more expenses? ",
                                               "Save successful",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question,
                                               MessageBoxDefaultButton.Button1);
                if (DialogResult != DialogResult.Yes)
                {
                    this.Close();
                }
                // If more data is being entered clears the user entered data for the new data
                else
                {
                    this.txtAmount.Text = "";
                    this.txtDetail.Text = "";

                    // Puts the focus back to the top of the form
                    this.cmbCategory.Focus();
                }
            }
        }

        #endregion

        #region Other Methods
        
        /// <summary>
        /// Gets the recurrence frequency and saves the appropiate amount of expences into
        /// the data base
        /// </summary>
        private void MultiSave()
        {
            switch (this.GetRecurrenceFrequency().ToLower())
            {
                case ("day"):
                {
                    this.MultiDaySave();
                    break;
                }
                case ("month"):
                {
                    if (this.dtpStartDate.Value.Day <= 28)
                    {
                        this.MultiMonthSave(); 
                    }
                    else
                    {
                        MessageBox.Show("Due to the fact that not all months have this many days," +
                                        " expenses can not be saved using this day of the month.\n" +
                                        "Please change the day of the month" +
                                        " and try again",
                                        "Invalid day of month",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning,
                                        MessageBoxDefaultButton.Button1);
                    }
                    break;
                }
                case ("year"):
                {
                    this.MultiYearSave();
                    break;
                }
                default:
                {
                    break;
                }
            }

        }

        /// <summary>
        /// Gets the Recurrence frequency from the form as a string
        /// </summary>
        /// <returns>The string representation of the recurrence frequency</returns>
        private string GetRecurrenceFrequency()
        {
            foreach (RadioButton CurrButton in this.pnRecurrenceOptions.Controls)
            {
                if (CurrButton.Checked)
                {
                    return (CurrButton.Text);
                }
            }

            return ("none");
        }

        private void MultiDaySave()
        {
            int nDaysRange = this.CalcDaysInRange();
            DateTime dtCurrentSaveDate = this.dtpStartDate.Value.Date;

            for (int nDayIndex = 0; nDayIndex < nDaysRange; nDayIndex++)
            {
                // Creates a new expence and sets the fields accordingly
                ExpBL exbNewExp = ExpBL.CreateExpence();
                exbNewExp.Amount = this.txtAmount.Text;
                exbNewExp.Date = dtCurrentSaveDate;
                exbNewExp.Category =
                    Convert.ToInt32(this.cmbCategory.SelectedValue);
                exbNewExp.Method =
                    Convert.ToInt32(this.cmbPayment.SelectedValue);
                exbNewExp.Comment = this.txtDetail.Text;

                // Saves the new expense into the cache
                exbNewExp.Save();

                // Ups the date for the next expence
               dtCurrentSaveDate = dtCurrentSaveDate.AddDays(1);
            }
        }

        private int CalcDaysInRange()
        {
            TimeSpan tsDaysInRange = this.dtpEndDate.Value.Date - this.dtpStartDate.Value.Date;

            return (tsDaysInRange.Days + 1);
        }

        private int CalcMonthsInRange()
        {
            return (((this.dtpEndDate.Value.Year - this.dtpStartDate.Value.Year) * 12) +
                                    (this.dtpEndDate.Value.Month - this.dtpStartDate.Value.Month) + 1);
        }
        
        private void MultiMonthSave()
        {
            DialogResult rsltSaveExpenses = DialogResult.OK;

            if (this.dtpStartDate.Value.Day != this.dtpEndDate.Value.Day)
            {
               rsltSaveExpenses = MessageBox.Show("The day in the month for the end of the range is different" +
                                " than the day in the start month.\n" +
                                "If you choose to continue, the day in the end month will be ignored",
                                "Mismatched dates",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
            }

            if (rsltSaveExpenses == DialogResult.OK)
            {
                int nMonthsRange = this.CalcMonthsInRange();
                DateTime dtCurrentSaveDate = this.dtpStartDate.Value.Date;

                for (int nMonthIndex = 0; nMonthIndex < nMonthsRange; nMonthIndex++)
                {
                    // Creates a new expence and sets the fields accordingly
                    ExpBL exbNewExp = ExpBL.CreateExpence();
                    exbNewExp.Amount = this.txtAmount.Text;
                    exbNewExp.Date = dtCurrentSaveDate;
                    exbNewExp.Category =
                        Convert.ToInt32(this.cmbCategory.SelectedValue);
                    exbNewExp.Method =
                        Convert.ToInt32(this.cmbPayment.SelectedValue);
                    exbNewExp.Comment = this.txtDetail.Text;

                    // Saves the new expense into the cache
                    exbNewExp.Save();

                    // Ups the date for the next expence
                    // If the new month has less days than it will automatically set the day 
                    // to the last possible day
                    dtCurrentSaveDate = dtCurrentSaveDate.AddMonths(1);
                } 
            }
        }

        private void MultiYearSave()
        {
            int nYearsInRange = (this.dtpEndDate.Value.Year - this.dtpStartDate.Value.Year) + 1;

            DateTime dtCurrentSaveDate = this.dtpStartDate.Value;

            for (int nYearIndex = 0; nYearIndex < nYearsInRange; nYearIndex++)
            {
                 // Creates a new expence and sets the fields accordingly
                ExpBL exbNewExp = ExpBL.CreateExpence();
                exbNewExp.Amount = this.txtAmount.Text;
                exbNewExp.Date = dtCurrentSaveDate;
                exbNewExp.Category =
                    Convert.ToInt32(this.cmbCategory.SelectedValue);
                exbNewExp.Method =
                    Convert.ToInt32(this.cmbPayment.SelectedValue);
                exbNewExp.Comment = this.txtDetail.Text;

                // Saves the new expense into the cache
                exbNewExp.Save();

                dtCurrentSaveDate = 
                    new DateTime((dtCurrentSaveDate.Year + 1), dtCurrentSaveDate.Month, dtCurrentSaveDate.Day);
            }
        }

        #endregion


        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpStartDate.Value.Date > this.dtpEndDate.Value.Date)
            {
                MessageBox.Show("The start date can not be later than the end date",
                                "Mismatched dates",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                this.dtpStartDate.Value = this.dtpEndDate.Value;
            }
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpStartDate.Value.Date > this.dtpEndDate.Value.Date)
            {
                MessageBox.Show("The end date can not be earlier than the start date",
                                "Mismatched dates",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                this.dtpEndDate.Value = this.dtpStartDate.Value;
            }
        }

        private void dtpEndDate_DropDown(object sender, EventArgs e)
        {
            //Todo this doesnt fully work yet
            this.dtpEndDate.MinDate = this.dtpStartDate.Value;
        }
    }
}
