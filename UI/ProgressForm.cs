using System.Windows.Forms;
using BusinessLogic;

namespace MyHome2013
{
    /// <summary>
    /// A visual representaion of the progress of a backup operation
    /// </summary>
    public partial class ProgressForm : Form
    {
        #region Data Members
        
        // Instance of the backup class
        Backup dataBackup = Program.Container.GetInstance<Backup>();

        #endregion

        #region C'Tor
        
        /// <summary>
        /// Sets up the event handlers
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();

            // Signs up to the AllDataProgress event
            dataBackup.AllDataProgress += new AllDataProgressDelegate(
                () =>
                    { 
                        // After each table is saved into the backup file,
                        // moves the progress bar forward
                        pgbAllDataProgress.PerformStep();
                    });

            // Signs up to the TableProgress event
            dataBackup.TableProgress += new TableProgressDelegate(
                () =>
                    {
                        // After each row in the current table is saved into the backup file,
                        // moves the progress bar forward
                        pgbTableProgress.PerformStep();

                        // If the value of the progress bar has reached the maximum,
                        // mods it with the maximum to give the progress bar a scrolling effect
                        pgbTableProgress.Value %= pgbTableProgress.Maximum;
                    });
        }

        #endregion

        #region Other Methods

        /// <summary>
        /// Backups all the data in the database into the backup files
        /// </summary>
        public void BackupAllData()
        {
            // Shows the form
            this.Show();

            // Backups the data
            dataBackup.BackupData();

            // Closes the form
            this.Close();
        }

        #endregion
    }
}
