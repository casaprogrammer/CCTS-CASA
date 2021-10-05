using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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

        Classes.CountInterval cnt = new Classes.CountInterval();

        private void DefaultValues()
        { 
            rtTipperOne.Text = cnt.TipperOneMaxCount.ToString();
            rtTipperTwo.Text = cnt.TipperTwoMaxCount.ToString();
            rtDumpAndPile.Text = cnt.DumpAndPileMaxCount.ToString();
            rtMainCane.Text = cnt.MainCaneMaxCount.ToString();
            rtKnivesAndShredder.Text = cnt.KnivesAndShredderMaxCount.ToString();
            rtWashingTime.Text = cnt.WashingTime.ToString();
            rtNir.Text = cnt.NirTime.ToString();
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
                rtWashingTime.Text != "" && rtWashingTime.Text != "0"
               )
            {
                try
                {
                    cnt.ChangeTipperOneCount(rtTipperOne.Text);
                    cnt.ChangeTipperTwoCount(rtTipperTwo.Text);
                    cnt.ChangeDumpAndPileCount(rtDumpAndPile.Text);
                    cnt.ChangeMainCaneCount(rtMainCane.Text);
                    cnt.ChangeKnivesAndShredderCount(rtKnivesAndShredder.Text);
                    cnt.ChangeWashingTime(rtWashingTime.Text);
                    cnt.ChangeNirTime(rtNir.Text);

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
                rtTipperOne.Text = File.ReadAllText(Path.GetFullPath("Configurations/tipperOneMaxCount.txt"));
            }
        }

        private void rtTipperTwo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipperTwo.Text = File.ReadAllText(Path.GetFullPath("Configurations/tipperTwoMaxCount.txt"));
            }
        }

        private void rtDumpAndPile_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpAndPile.Text = File.ReadAllText(Path.GetFullPath("Configurations/dumpAndPileMaxCount.txt"));
            }
        }

        private void rtMainCane_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtMainCane.Text = File.ReadAllText(Path.GetFullPath("Configurations/mainCaneMaxCount.txt"));
            }
        }

        private void rtKnivesAndShredder_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtKnivesAndShredder.Text = File.ReadAllText(Path.GetFullPath("Configurations/knivesAndShredderMaxCount.txt"));
            }
        }

        private void rtNir_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtNir.Text = File.ReadAllText(Path.GetFullPath("Configurations/nirTimerCount.txt"));
            }
        }
    }
}
