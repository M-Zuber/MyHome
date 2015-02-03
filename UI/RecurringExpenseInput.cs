using BusinessLogic;
using MyHome2013.Core.LocalTypes;
using System;
using System.Windows.Forms;

namespace MyHome2013
{
    /// <summary>
    /// Enables the user to add new expense data
    /// that recurrs over the given period, with the frequnecy given
    /// -allows for continuous data entry
    /// </summary>
    public partial class RecurringExpenseInput : Form
    {
        #region C'Tor

        /// <summary>
        /// Standard Default Ctor
        /// </summary>
        public RecurringExpenseInput()
        {
            InitializeComponent();
        }

        #endregion

        #region Control Event Methods

        /// <summary>
        /// Connects the combo boxes on the form with the data from the cache
        ///  and sets the date time pickers with data bindings to keep them 
        ///  from crossing over
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
        private void RecurringExpenseInput_Load(object sender, EventArgs e)
        {
            // Sets up the combo box of the income categories
            var ech = Program.Container.GetInstance<IRepository<ExpenseCategory>>();
            this.cmbCategory.DataSource = ech.LoadAll();
            this.cmbCategory.DisplayMember = "NAME";
            this.cmbCategory.ValueMember = "ID";

            // Sets up the combo box with the payment methods
            var r = Program.Container.GetInstance<IRepository<PaymentMethod>>();
            this.cmbPayment.DataSource = r.LoadAll();
            this.cmbPayment.DisplayMember = "NAME";
            this.cmbPayment.ValueMember = "ID";

            // Sets up the date time pickers with data bindings to keep the dates from
            // crossing over in the wrong direction
            this.dtpStartDate.DataBindings.Add("MaxDate", this.dtpEndDate, "Value");
            this.dtpEndDate.DataBindings.Add("MinDate", this.dtpStartDate, "Value");
        }

        /// <summary>
        /// Saves the new expenses into the cache according to the given frequency
        /// -validates the amount and gives an option to enter more data
        /// </summary>
        /// <param name="sender">Standard sender object</param>
        /// <param name="e">Standard event object</param>
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
            else if (!HelperMethods.IsNumeric(this.txtAmount.Text))
            {
                MessageBox.Show("The amount must be in numbers",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
                this.txtAmount.Text = "";
            }
            // Makes sure that a recurrence frequency is chosen
            else if (this.GetRecurrenceFrequency() == "none")
            {
                MessageBox.Show("This form is for entering expenses that recurr\n" +
                                "Please choose a recurrence frequency",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1);
            }
            // Otherwise saves the new expenses
            else
            {
                // Gets the recurrence frequency and saves the data into the cache
                // accordingly
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
        /// Gets the recurrence frequency and saves the appropiate amount of expenses into
        /// the data base
        /// </summary>
        private void MultiSave()
        {
            // Gets the recurrence frequency and calls the corresponding save function
            switch (this.GetRecurrenceFrequency().ToLower())
            {
                // The expense recurrs every day
                case ("day"):
                {
                    this.MultiDaySave();
                    break;
                }
                // The expense recurrs every month
                case ("month"):
                {
                    // Checks if the day in the month is within the valid range
                    if (this.dtpStartDate.Value.Day <= 28)
                    {
                        this.MultiMonthSave(); 
                    }
                    // If the day is not in the range, informs the user
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
                // The expense occurs every year
                case ("year"):
                {
                    this.MultiYearSave();
                    break;
                }
                // Default case
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
            // Goes over the radio buttons one at a time to see which one is checked
            foreach (RadioButton CurrButton in this.pnRecurrenceOptions.Controls)
            {
                // If the current button is checked
                if (CurrButton.Checked)
                {
                    // Returns the text of the button (which signals what recurrence frequency was chosen)
                    return (CurrButton.Text);
                }
            }

            // If no button was checked, returns an indication of that fact
            return ("none");
        }

        /// <summary>
        /// Saves multiple expenses into the cache -with a frequency of every day
        /// </summary>
        private void MultiDaySave()
        {
            // Gets the amount of days in the range of dates choosen
            int nDaysRange = this.CalcDaysInRange();

            // Sets a local variable that will hold the date of the individual expense being saved
            // the initial value is the start date
            DateTime dtCurrentSaveDate = this.dtpStartDate.Value.Date;

            // Loops for the amount of days in the range
            for (int nDayIndex = 0; nDayIndex < nDaysRange; nDayIndex++)
            {
                SaveNewExpense(dtCurrentSaveDate);

                // Ups the date for the next expense
               dtCurrentSaveDate = dtCurrentSaveDate.AddDays(1);
            }
        }

        private void SaveNewExpense(DateTime dtCurrentSaveDate)
        {
            var pmr = Program.Container.GetInstance<IRepository<PaymentMethod>>();
            var er = Program.Container.GetInstance<IRepository<ExpenseCategory>>();
            var eh = Program.Container.GetInstance<ExpenseHandler>();

            Expense newExpense =
                    new Expense(double.Parse(this.txtAmount.Text), dtCurrentSaveDate,
                                er.LoadById(Convert.ToInt32(this.cmbCategory.SelectedValue)),
                                pmr.LoadById(Convert.ToInt32(this.cmbPayment.SelectedValue)),
                                this.txtDetail.Text);

            eh.AddNewExpense(newExpense);
        }

        /// <summary>
        /// Calculates the amount of days in the range of two dates
        /// </summary>
        /// <returns>The amount of days in the range</returns>
        private int CalcDaysInRange()
        {
            // Creates a time span object with the difference in between the end date and the start date
            TimeSpan tsDaysInRange = this.dtpEndDate.Value.Date - this.dtpStartDate.Value.Date;

            // Returns the amount of days represented by the time span object
            // plus one so that if the dates are the same day, it will still save once
            return (tsDaysInRange.Days + 1);
        }

        /// <summary>
        /// Calculates the months in the range from the start date to the end date
        /// </summary>
        /// <returns>The number of months in the range</returns>
        private int CalcMonthsInRange()
        {
            // Calculates the months in the range, taking the year into account
            // plus one so that if the dates are the same day, it will still save once
            return (((this.dtpEndDate.Value.Year - this.dtpStartDate.Value.Year) * 12) +
                                    (this.dtpEndDate.Value.Month - this.dtpStartDate.Value.Month) + 1);
        }

        /// <summary>
        /// Saves multiple expenses into the cache -with a frequency of every month
        /// </summary>
        private void MultiMonthSave()
        {
            // Creates a dialog result, in case further input is needed from the user
            DialogResult rsltSaveExpenses = DialogResult.OK;

            // If the days in the start month and end month are different
            // informs the user, and gives them an option to go back and change it
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

            // If the days where not different or the user choose to continoue anyway
            if (rsltSaveExpenses == DialogResult.OK)
            {
                // Calculates the months in the range of dates choosen
                int nMonthsRange = this.CalcMonthsInRange();

                // Sets a local variable that will hold the date of the individual expense being saved
                // the initial value is the start date
                DateTime dtCurrentSaveDate = this.dtpStartDate.Value.Date;

                // Loops for the amount of months in the range
                for (int nMonthIndex = 0; nMonthIndex < nMonthsRange; nMonthIndex++)
                {
                    SaveNewExpense(dtCurrentSaveDate);

                    // Ups the date for the next expense
                    // If the new month has less days than it will automatically set the day 
                    // to the last possible day
                    dtCurrentSaveDate = dtCurrentSaveDate.AddMonths(1);
                } 
            }
        }

        /// <summary>
        /// Saves multiple expenses into the cache -with a frequency of every year
        /// </summary>
        private void MultiYearSave()
        {
            // Calculates the years in the range of dates choosen
            int nYearsInRange = (this.dtpEndDate.Value.Year - this.dtpStartDate.Value.Year) + 1;

            // Sets a local variable that will hold the date of the individual expense being saved
            // the initial value is the start date
            DateTime dtCurrentSaveDate = this.dtpStartDate.Value;

            // Loops for the amount of years in the range
            for (int nYearIndex = 0; nYearIndex < nYearsInRange; nYearIndex++)
            {
                SaveNewExpense(dtCurrentSaveDate);

                // Ups the date for the next expense
                dtCurrentSaveDate = dtCurrentSaveDate.AddYears(1);
            }
        }

        #endregion
    }
}
