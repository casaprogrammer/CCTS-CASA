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

        private void DefaultValues()
        {
            rtTipperOne.Text = File.ReadAllText(Path.GetFullPath("Configurations/tipperOneMaxCount.txt"));
            rtTipperTwo.Text = File.ReadAllText(Path.GetFullPath("Configurations/tipperTwoMaxCount.txt"));
            rtDumpAndPile.Text = File.ReadAllText(Path.GetFullPath("Configurations/dumpAndPileMaxCount.txt"));
            rtMainCane.Text = File.ReadAllText(Path.GetFullPath("Configurations/mainCaneMaxCount.txt"));
            rtKnivesAndShredder.Text = File.ReadAllText(Path.GetFullPath("Configurations/knivesAndShredderMaxCount.txt"));
            rtNir.Text = File.ReadAllText(Path.GetFullPath("Configurations/nirTimerCount.txt"));
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (
                rtTipperOne.Text != "" && rtTipperOne.Text != "0" &&
                rtTipperTwo.Text != "" && rtTipperTwo.Text != "0" &&
                rtDumpAndPile.Text != "" && rtDumpAndPile.Text != "0" && 
                rtMainCane.Text != "" && rtMainCane.Text != "0" &&
                rtKnivesAndShredder.Text != "" && rtKnivesAndShredder.Text != "0" &&
                rtNir.Text != "" && rtNir.Text != "0"
               )
            {
                File.WriteAllText(Path.GetFullPath("Configurations/tipperOneMaxCount.txt"), rtTipperOne.Text);
                File.WriteAllText(Path.GetFullPath("Configurations/tipperTwoMaxCount.txt"), rtTipperTwo.Text);
                File.WriteAllText(Path.GetFullPath("Configurations/dumpAndPileMaxCount.txt"), rtDumpAndPile.Text);
                File.WriteAllText(Path.GetFullPath("Configurations/mainCaneMaxCount.txt"), rtMainCane.Text);
                File.WriteAllText(Path.GetFullPath("Configurations/knivesAndShredderMaxCount.txt"), rtKnivesAndShredder.Text);
                File.WriteAllText(Path.GetFullPath("Configurations/nirTimerCount.txt"), rtNir.Text);
                MessageBox.Show("Changes Saved", "Saved");

                this.Close();
            }
            else
            {
                MessageBox.Show("Don't leave a field blank/0 count not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
