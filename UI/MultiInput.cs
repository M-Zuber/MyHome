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
                // Creates a new expense and sets the properties with the data from the form
                ExpBL exbNewExp = ExpBL.CreateExpence();
                exbNewExp.Amount = this.txtAmount.Text;
                exbNewExp.Date = this.dtpStartDate.Value.Date;
                exbNewExp.Category =
                    Convert.ToInt32(this.cmbCategory.SelectedValue);
                exbNewExp.Method =
                    Convert.ToInt32(this.cmbPayment.SelectedValue);
                exbNewExp.Comment = this.txtDetail.Text;

                // Saves the new expense into the cache
                exbNewExp.Save();

                // Asks if more data is being entered
                DialogResult = MessageBox.Show("The entry was saved" +
                                               "\nDo you want to add another expense? ",
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
                        this.MultiMonthSave();
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
        //TODO when the user is saving on a monthly basis - if the days are different -either force them to be the same using code? or inform the user that it might not be exactly what they think and give option of it changing auotmatically, going on anyway, or cancel
        //todo      if the end month has less days its not a neccessary check, but maybe it should show in any case to avoid complicating the code
        private void MultiMonthSave()
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
                //TODO should i check if the day was changed'cuz a month was hit with less days, to move it back when possible -or continue saving with the new day??
                if ((dtCurrentSaveDate.Day < DateTime.DaysInMonth(dtCurrentSaveDate.Year, dtCurrentSaveDate.Month)) &&
                    (dtCurrentSaveDate.Day != dtpStartDate.Value.Day))
                {
                    
                }
            }
        }

        private void MultiYearSave()
        {
        }

        #endregion


        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            //toDo date time pic to leave event -make shure that the startdate is always earlier or the same than the end date
            //todo their should be a better event that will just stop the dropdown box from going past the appropiate date
            //todo      in which case i might have to stop the user from being able to enter the date using the keyboard
        }
    }
}
