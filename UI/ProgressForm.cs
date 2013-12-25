using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BL;
using FrameWork;

namespace MyHome2013
{
    public partial class ProgressForm : Form
    {
        #region Data Members

        Backup dataBackup = new Backup();

        #endregion

        #region C'Tor
        
        public ProgressForm()
        {
            InitializeComponent();

            dataBackup.AllDataProgress += new AllDataProgressDelegate(
                () =>
                    { 
                        pgbAllDataProgress.PerformStep();
                        pgbAllDataProgress.Value %= pgbAllDataProgress.Maximum;
                    });
            dataBackup.TableProgress += new TableProgressDelegate(
                () =>
                    {
                        pgbTableProgress.PerformStep();
                        pgbTableProgress.Value %= pgbTableProgress.Maximum;
                    });
        }

        #endregion

        #region Other Methods

        public void BackupAllData()
        {
            this.Show();
            dataBackup.BackupData();
            this.Close();
        }

        #endregion
    }
}
