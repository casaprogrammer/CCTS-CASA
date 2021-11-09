using Cane_Tracking.Classes;
using System;
using System.Windows.Forms;

namespace Cane_Tracking
{
    public partial class frmEditConfigs : Form
    {
        public frmEditConfigs()
        {
            InitializeComponent();
            DefaultValues();
        }

        ConfigValues cnf = new ConfigValues();
        AppLogging log = new AppLogging();

        private void DefaultValues()
        {
            rtTipperOne.Text = cnf.TipperOneMaxCount.ToString();
            rtTipperTwo.Text = cnf.TipperTwoMaxCount.ToString();
            rtDumpAndPile.Text = cnf.DumpAndPileMaxCount.ToString();
            rtMainCane.Text = cnf.MainCaneMaxCount.ToString();
            rtKnivesAndShredder.Text = cnf.KnivesAndShredderMaxCount.ToString();
            rtWashingTime.Text = cnf.WashingTime.ToString();
            rtNir.Text = cnf.NirTime.ToString();
            rtNirNCS.Text = cnf.NirAddress;
            rtNcsPort.Text = cnf.NirPort.ToString();
            rtScanLimit.Text = cnf.ScanLimit.ToString();
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (
                rtTipperOne.Text != "" && rtTipperOne.Text != "0" &&
                rtTipperTwo.Text != "" && rtTipperTwo.Text != "0" &&
                rtDumpAndPile.Text != "" && rtDumpAndPile.Text != "0" &&
                rtMainCane.Text != "" && rtMainCane.Text != "0" &&
                rtKnivesAndShredder.Text != "" && rtKnivesAndShredder.Text != "0" &&
                rtNir.Text != "" && rtNir.Text != "0" &&
                rtWashingTime.Text != "" && rtWashingTime.Text != "0" &&
                rtNirNCS.Text != "" && rtNcsPort.Text != "" &&
                rtScanLimit.Text != ""
               )
            {
                try
                {
                    cnf.ChangeTipperOneCount(rtTipperOne.Text);
                    cnf.ChangeTipperTwoCount(rtTipperTwo.Text);
                    cnf.ChangeDumpAndPileCount(rtDumpAndPile.Text);
                    cnf.ChangeMainCaneCount(rtMainCane.Text);
                    cnf.ChangeKnivesAndShredderCount(rtKnivesAndShredder.Text);
                    cnf.ChangeWashingTime(rtWashingTime.Text);
                    cnf.ChangeNirTime(rtNir.Text);
                    cnf.ChangeNirAddress(rtNirNCS.Text);
                    cnf.ChangeNirPort(rtNcsPort.Text);
                    cnf.ScannedSample(rtScanLimit.Text);

                    string t = DateTime.Now + " : " + "Changes to configuration files were made";
                    log.LogEvent(t);

                    MessageBox.Show("Changes Saved", "Saved");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error", e.ToString());
                }


                /*this.Close();*/
            }
            else
            {
                MessageBox.Show("Don't leave a field blank/0 not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtTipperOne_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipperOne.Text = cnf.TipperOneMaxCount.ToString();
            }
        }

        private void rtTipperTwo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipperTwo.Text = cnf.TipperTwoMaxCount.ToString();
            }
        }

        private void rtDumpAndPile_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpAndPile.Text = cnf.DumpAndPileMaxCount.ToString();
            }
        }

        private void rtMainCane_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtMainCane.Text = cnf.MainCaneMaxCount.ToString();
            }
        }

        private void rtKnivesAndShredder_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtKnivesAndShredder.Text = cnf.KnivesAndShredderMaxCount.ToString();
            }
        }

        private void rtNir_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtNir.Text = cnf.NirTime.ToString();
            }
        }

        private void rtScanLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtScanLimit.Text = cnf.ScanLimit.ToString();
            }
        }
    }
}
