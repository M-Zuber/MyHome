using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using LocalTypes;

namespace BusinessLogic
{
    #region Delegate Region

    /// <summary>
    /// Delegate for progress in a specific table
    /// </summary>
    public delegate void TableProgressDelegate();

    /// <summary>
    /// Delegate for progress in the data-set as a whole
    /// </summary>
    public delegate void AllDataProgressDelegate();
    
    #endregion

    /// <summary>
    /// Saves the data currently in the database into text files
    /// </summary>
    public class Backup
    {
        #region Event Members

        // Event for in-table progress
        public event TableProgressDelegate TableProgress;

        // Event for overall progress in the data-set
        public event AllDataProgressDelegate AllDataProgress;
        
        #endregion

        #region Data Members

        // Instance of the folder for the backup files
        private DirectoryInfo backupFolder = new DirectoryInfo("./Backup Files");

        // List of instances of the backup files - one per table
        private List<FileInfo> backupFiles = new List<FileInfo>()
        {
            new FileInfo("./Backup Files/ExpenseCategories.xml"),
            new FileInfo("./Backup Files/IncomeCategories.xml"),
            new FileInfo("./Backup Files/PaymentMethods.xml"),
            new FileInfo("./Backup Files/Expenses.xml"),
            new FileInfo("./Backup Files/Incomes.xml")
        };
        
        #endregion

        IncomeHandler incomehandler;
        ExpenseHandler expensehandler;
        ExpenseCategoryHandler expensecategoryhandler;
        IncomeCategoryHandler incomecategoryhandler;
        PaymentMethodHandler paymentmethodhandler;

        #region C'Tor

        /// <summary>
        /// Verifys that the folder and files exist before a backup is performed
        /// </summary>
        public Backup(IncomeHandler incomehandler, ExpenseHandler expensehandler, ExpenseCategoryHandler expensecategoryhandler, IncomeCategoryHandler incomecategoryhandler, PaymentMethodHandler paymentmethodhandler)
        {
            this.incomehandler = incomehandler;
            this.expensehandler = expensehandler;
            this.expensecategoryhandler = expensecategoryhandler;
            this.incomecategoryhandler = incomecategoryhandler;
            this.paymentmethodhandler = paymentmethodhandler;

            // If the folder doesnt exist yet -creates it
            if (!this.backupFolder.Exists)
            {
                backupFolder.Create();
            }

            // Goes over every file in the list to make sure it exists
            foreach (FileInfo CurrbackupFile in this.backupFiles)
            {
                if (!CurrbackupFile.Exists)
                {
                    // If the file doesnt exist, creates it.
                    // No actual value is written in, this is just the simplest way to create
                    // the file without locking up the resource
                    using (StreamWriter stwrAppend = CurrbackupFile.AppendText()){}
                }
            }
        }
        
        #endregion

        #region Event Methods

        /// <summary>
        /// Only raises the AllDataProgress event if someone is registered
        /// </summary>
        private void OnTableComplete()
        {
            if (this.AllDataProgress != null)
            {
                this.AllDataProgress();
            }
        }

        /// <summary>
        /// Only raises the TableProgress event if someone is registered
        /// </summary>
        private void OnLineWritten()
        {
            if (this.TableProgress != null)
            {
                this.TableProgress();
            }
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Saves the all the data into text files
        /// </summary>
        public void BackupData()
        {
            // Goes over every file in the list and writes into it the data of the corrsponding
            // table
            foreach (FileInfo CurrbackupFile in this.backupFiles)
            {
                this.BackupTable(CurrbackupFile);
            }
        }

        /// <summary>
        /// Saves the data of the requested table into the corrsponding file
        /// </summary>
        /// <param name="file">The name of the table to be saved</param>
        private void BackupTable(FileInfo file)
        {
            switch (file.Name.Split('.')[0])
            {
                case("ExpenseCategories"):
                {
                    SaveExpenseCategories(file.FullName);
                    break;
                }
                case("IncomeCategories"):
                {
                    SaveIncomeCategories(file.FullName);
                    break;
                }
                case("PaymentMethods"):
                {
                    SavePaymentMethods(file.FullName);
                    break;
                }
                case("Expenses"):
                {
                    SaveExpenses(file.FullName);
                    break;
                }
                case("Incomes"):
                {
                    SaveIncomes(file.FullName);
                    break;
                }
                default:
                {
                    break;
                }
            }
        }

        private void SaveIncomes(string saveFile)
        {
            XElement data = new XElement("Incomes");

            foreach (Income curIncome in incomehandler.LoadAll())
            {
                data.Add(new XElement("Income",
                            new XElement("ID", curIncome.ID),
                             new XElement("Category", curIncome.Category.Name),
                             new XElement("Methos", curIncome.Method.Name),
                             new XElement("Amount", curIncome.Amount),
                             new XElement("Date", curIncome.Date.ToString("dd-MM-yyyy")),
                             new XElement("Comments", curIncome.Comment ?? string.Empty)
                        ));

                this.OnLineWritten();
            }

            this.OnTableComplete();
            data.Save(saveFile);
        }

        private void SaveExpenses(string saveFile)
        {
            XElement data = new XElement("Expenses");

            foreach (Expense curExpense in expensehandler.LoadAll())
            {
                data.Add(new XElement("Expense",
                            new XElement("ID", curExpense.ID),
                             new XElement("Category", curExpense.Category.Name),
                             new XElement("Methos", curExpense.Method.Name),
                             new XElement("Amount", curExpense.Amount),
                             new XElement("Date", curExpense.Date.ToString("dd-MM-yyyy")),
                             new XElement("Comments", curExpense.Comment ?? string.Empty)
                        ));

                this.OnLineWritten();
            }

            this.OnTableComplete();
            data.Save(saveFile);
        }

        private void SavePaymentMethods(string saveFile)
        {
            XElement data = new XElement("PaymemtMethods");

            foreach (PaymentMethod curPaymentMethod in paymentmethodhandler.LoadAll())
            {
                data.Add(new XElement("PaymentMethod",
                            new XElement("ID", curPaymentMethod.Id),
                            new XElement("Name", curPaymentMethod.Name)
                        ));

                this.OnLineWritten();
            }

            this.OnTableComplete();
            data.Save(saveFile);
        }

        private void SaveIncomeCategories(string saveFile)
        {
            XElement data = new XElement("IncomeCategories");

            foreach (IncomeCategory curIncomeCategory in incomecategoryhandler.LoadAll())
            {
                data.Add(new XElement("IncomeCategory",
                            new XElement("ID", curIncomeCategory.Id),
                            new XElement("Name", curIncomeCategory.Name)
                         ));

                this.OnLineWritten();
            }

            this.OnTableComplete();
            data.Save(saveFile);
        }

        private void SaveExpenseCategories(string saveFile)
        {
            XElement data = new XElement("ExpenseCategories");

            foreach (ExpenseCategory curExpenseCategory in expensecategoryhandler.LoadAll())
            {
                data.Add(new XElement("ExpenseCategory",
                            new XElement("ID", curExpenseCategory.Id),
                            new XElement("Name", curExpenseCategory.Name)
                        ));
                
                this.OnLineWritten();
            }

            this.OnTableComplete();
            data.Save(saveFile);
        }
        
        #endregion
    }
}
