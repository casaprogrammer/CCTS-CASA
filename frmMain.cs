using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking
{
    public partial class frmMain : Form
    {

        Classes.Sensor sc = new Classes.Sensor();
        Classes.BatchNumberList bnlist = new Classes.BatchNumberList();
        Classes.CrossThreadingCheck ctcc = new Classes.CrossThreadingCheck();
        Classes.NirTimer nirTimer = new Classes.NirTimer();
        
        ToolTip toolTip = new ToolTip();
        SqlConnection con = new SqlConnection(File.ReadAllText(Path.GetFullPath("Configurations/DbConnection.txt")));

        private static SerialPort serialPort;

        private Timer savingStateTimer;
        private Timer checkConfigsTimer;

        private static int seriesNo = 0;
        private static int stateBatchNum;

        private static bool decrementing = false;
        private static bool pause = false;

        private static string logTextOutput;
        private static string batch;


        public frmMain()
        {
            InitializeComponent();
            TextAlignment();
            InitializeSerialConnections();
            DefaultValues();
            //ConnectToDB();
            CheckConfigStart();
        }



        /*+============================================= RICHTEXTBOX FORMATS ==================================================+*/

        private void rtTipperOneBn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipperOneBn.Text = "";
            }
        }

        private void rtTipperTwoBn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipperTwoBn.Text = "";
            }
        }

        private void rtDumpTruckBn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpTruckBn.Text = "";
            }
        }

        private void rtStockPileBn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockPileBn.Text = "";
            }
        }

        private void rtTipOneBn1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn1.Text = "";
            }
        }

        private void rtTipOneBn2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn2.Text = "";
            }
        }

        private void rtTipOneBn3_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn3.Text = "";
            }
        }

        private void rtTipOneBn4_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn4.Text = "";
            }
        }

        private void rtTipOneBn5_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn5.Text = "";
            }
        }

        private void rtTipOneBn6_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn6.Text = "";
            }
        }

        private void rtTipOneBn7_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn7.Text = "";
            }
        }

        private void rtTipOneBn8_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipOneBn8.Text = "";
            }
        }

        private void rtTipTwoBn1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn1.Text = "";
            }
        }

        private void rtTipTwoBn2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn2.Text = "";
            }
        }

        private void rtTipTwoBn3_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn3.Text = "";
            }
        }

        private void rtTipTwoBn4_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn4.Text = "";
            }
        }

        private void rtTipTwoBn5_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn5.Text = "";
            }
        }

        private void rtTipTwoBn6_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn6.Text = "";
            }
        }

        private void rtTipTwoBn7_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn7.Text = "";
            }
        }

        private void rtTipTwoBn8_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtTipTwoBn8.Text = "";
            }
        }

        private void rtDumpBn1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn1.Text = "";
            }
        }

        private void rtDumpBn2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn2.Text = "";
            }
        }

        private void rtDumpBn3_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn3.Text = "";
            }
        }

        private void rtDumpBn4_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn4.Text = "";
            }
        }

        private void rtDumpBn5_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn5.Text = "";
            }
        }

        private void rtDumpBn6_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn6.Text = "";
            }
        }

        private void rtDumpBn7_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn7.Text = "";
            }
        }

        private void rtDumpBn8_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtDumpBn8.Text = "";
            }
        }

        private void rtStockBn1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn1.Text = "";
            }
        }

        private void rtStockBn2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn2.Text = "";
            }
        }

        private void rtStockBn3_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn3.Text = "";
            }
        }

        private void rtStockBn4_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn4.Text = "";
            }
        }

        private void rtStockBn5_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn5.Text = "";
            }
        }

        private void rtStockBn6_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn6.Text = "";
            }
        }

        private void rtStockBn7_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn7.Text = "";
            }
        }

        private void rtStockBn8_KeyDown(object sender, KeyEventArgs e)
        {
            if (!char.IsDigit((char)e.KeyValue) && !char.IsControl((char)e.KeyValue))
            {
                MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                rtStockBn8.Text = "";
            }
        }

        private void rtTipOneBn1_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn1.Text != "" && rtTipOneBx1.Text != "")
            {
                if (rtTipOneBn1.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn1.Text + " @ count " + rtTipOneBx1.Text;
                    batch = this.rtTipOneBn1.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn2_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn2.Text != "" && rtTipOneBx2.Text != "")
            {
                if (rtTipOneBn2.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn2.Text + " @ count " + rtTipOneBx2.Text;
                    batch = this.rtTipOneBn2.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn3_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn3.Text != "" && rtTipOneBx3.Text != "")
            {
                if (rtTipOneBn3.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn3.Text + " @ count " + rtTipOneBx3.Text;
                    batch = this.rtTipOneBn3.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn4_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn4.Text != "" && rtTipOneBx4.Text != "")
            {
                if (rtTipOneBn4.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn4.Text + " @ count " + rtTipOneBx4.Text;
                    batch = this.rtTipOneBn4.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn5_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn5.Text != "" && rtTipOneBx5.Text != "")
            {
                if (rtTipOneBn5.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn5.Text + " @ count " + rtTipOneBx5.Text;
                    batch = this.rtTipOneBn5.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn6_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn6.Text != "" && rtTipOneBx6.Text != "")
            {
                if (rtTipOneBn6.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn6.Text + " @ count " + rtTipOneBx6.Text;
                    batch = this.rtTipOneBn6.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn7_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn7.Text != "" && rtTipOneBx7.Text != "")
            {
                if (rtTipOneBn7.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn7.Text + " @ count " + rtTipOneBx7.Text;
                    batch = this.rtTipOneBn7.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipOneBn8_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipOneBn8.Text != "" && rtTipOneBx8.Text != "")
            {
                if (rtTipOneBn8.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 1) Changed Batch #" + batch + " to " + this.rtTipOneBn8.Text + " @ count " + rtTipOneBx8.Text;
                    batch = this.rtTipOneBn8.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn1_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn1.Text != "" && rtTipTwoBx1.Text != "")
            {
                if (rtTipTwoBn1.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn1.Text + " @ count " + rtTipTwoBx1.Text;
                    batch = this.rtTipTwoBn1.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn2_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn2.Text != "" && rtTipTwoBx2.Text != "")
            {
                if (rtTipTwoBn2.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn2.Text + " @ count " + rtTipTwoBx2.Text;
                    batch = this.rtTipTwoBn2.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn3_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn3.Text != "" && rtTipTwoBx3.Text != "")
            {
                if (rtTipTwoBn3.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn3.Text + " @ count " + rtTipTwoBx3.Text;
                    batch = this.rtTipTwoBn3.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn4_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn4.Text != "" && rtTipTwoBx4.Text != "")
            {
                if (rtTipTwoBn4.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn4.Text + " @ count " + rtTipTwoBx4.Text;
                    batch = this.rtTipTwoBn4.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn5_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn5.Text != "" && rtTipTwoBx5.Text != "")
            {
                if (rtTipTwoBn5.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn5.Text + " @ count " + rtTipTwoBx5.Text;
                    batch = this.rtTipTwoBn5.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn6_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn6.Text != "" && rtTipTwoBx6.Text != "")
            {
                if (rtTipTwoBn6.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn6.Text + " @ count " + rtTipTwoBx6.Text;
                    batch = this.rtTipTwoBn6.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn7_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn7.Text != "" && rtTipTwoBx7.Text != "")
            {
                if (rtTipTwoBn7.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn7.Text + " @ count " + rtTipTwoBx7.Text;
                    batch = this.rtTipTwoBn7.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtTipTwoBn8_TextChanged(object sender, EventArgs e)
        {
            if (this.rtTipTwoBn8.Text != "" && rtTipTwoBx8.Text != "")
            {
                if (rtTipTwoBn8.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Tipper 2) Changed Batch #" + batch + " to " + this.rtTipTwoBn8.Text + " @ count " + rtTipTwoBx8.Text;
                    batch = this.rtTipTwoBn8.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtDumpBn1_TextChanged(object sender, EventArgs e)
        {
            if (this.rtDumpBn1.Text != "" && rtDumpBx1.Text != "")
            {
                if (rtDumpBn1.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Dump Truck) Changed Batch #" + batch + " to " + this.rtDumpBn1.Text + " @ count " + rtDumpBx1.Text;
                    batch = this.rtDumpBn1.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtDumpBn2_TextChanged(object sender, EventArgs e)
        {
            if (this.rtDumpBn2.Text != "" && rtDumpBx2.Text != "")
            {
                if (rtDumpBn2.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Dump Truck) Changed Batch #" + batch + " to " + this.rtDumpBn2.Text + " @ count " + rtDumpBx2.Text;
                    batch = this.rtDumpBn2.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtDumpBn3_TextChanged(object sender, EventArgs e)
        {
            if (this.rtDumpBn3.Text != "" && rtDumpBx3.Text != "")
            {
                if (rtDumpBn3.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Dump Truck) Changed Batch #" + batch + " to " + this.rtDumpBn3.Text + " @ count " + rtDumpBx3.Text;
                    batch = this.rtDumpBn3.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtDumpBn4_TextChanged(object sender, EventArgs e)
        {
            if (this.rtDumpBn4.Text != "" && rtDumpBx4.Text != "")
            {
                if (rtDumpBn4.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Dump Truck) Changed Batch #" + batch + " to " + this.rtDumpBn4.Text + " @ count " + rtDumpBx4.Text;
                    batch = this.rtDumpBn4.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtDumpBn5_TextChanged(object sender, EventArgs e)
        {
            if (this.rtDumpBn5.Text != "" && rtDumpBx5.Text != "")
            {
                if (rtDumpBn5.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Dump Truck) Changed Batch #" + batch + " to " + this.rtDumpBn5.Text + " @ count " + rtDumpBx5.Text;
                    batch = this.rtDumpBn5.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtDumpBn6_TextChanged(object sender, EventArgs e)
        {
            if (this.rtDumpBn6.Text != "" && rtDumpBx6.Text != "")
            {
                if (rtDumpBn6.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Dump Truck) Changed Batch #" + batch + " to " + this.rtDumpBn6.Text + " @ count " + rtDumpBx6.Text;
                    batch = this.rtDumpBn6.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtStockBn1_TextChanged(object sender, EventArgs e)
        {
            if (this.rtStockBn1.Text != "" && rtStockBx1.Text != "")
            {
                if (rtStockBn1.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Stock Pile) Changed Batch #" + batch + " to " + this.rtStockBn1.Text + " @ count " + rtStockBx1.Text;
                    batch = this.rtStockBn1.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtStockBn2_TextChanged(object sender, EventArgs e)
        {
            if (this.rtStockBn2.Text != "" && rtStockBx2.Text != "")
            {
                if (rtStockBn2.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Stock Pile) Changed Batch #" + batch + " to " + this.rtStockBn2.Text + " @ count " + rtStockBx2.Text;
                    batch = this.rtStockBn2.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtStockBn3_TextChanged(object sender, EventArgs e)
        {
            if (this.rtStockBn3.Text != "" && rtStockBx3.Text != "")
            {
                if (rtStockBn3.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Stock Pile) Changed Batch #" + batch + " to " + this.rtStockBn3.Text + " @ count " + rtStockBx3.Text;
                    batch = this.rtStockBn3.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtStockBn4_TextChanged(object sender, EventArgs e)
        {
            if (this.rtStockBn4.Text != "" && rtStockBx4.Text != "")
            {
                if (rtStockBn4.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Stock Pile) Changed Batch #" + batch + " to " + this.rtStockBn4.Text + " @ count " + rtStockBx4.Text;
                    batch = this.rtStockBn4.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtStockBn5_TextChanged(object sender, EventArgs e)
        {
            if (this.rtStockBn5.Text != "" && rtStockBx5.Text != "")
            {
                if (rtStockBn5.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Stock Pile) Changed Batch #" + batch + " to " + this.rtStockBn5.Text + " @ count " + rtStockBx5.Text;
                    batch = this.rtStockBn5.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtStockBn6_TextChanged(object sender, EventArgs e)
        {
            if (this.rtStockBn6.Text != "" && rtStockBx6.Text != "")
            {
                if (rtStockBn6.Text != batch)
                {
                    logTextOutput = DateTime.Now.ToString() + " : (Stock Pile) Changed Batch #" + batch + " to " + this.rtStockBn6.Text + " @ count " + rtStockBx6.Text;
                    batch = this.rtStockBn6.Text;
                    LogOutput(logTextOutput);
                }
            }
        }

        private void rtNirWashing_TextChanged(object sender, EventArgs e)
        {
            int c = 0;
            if (this.rtNirWashing.Text != "")
            {
                if (nirTimer.washingTimerList.Count > 0)
                {
                    for (int i = 0; i < nirTimer.washingTimerList.Count;)
                    {
                        nirTimer.washingTimerList[i].Stop();
                        nirTimer.washingTimerList.RemoveAt(i);
                    }
                    nirTimer.SetWashingTimer(ref c, rtNirScanning, rtNirCount, rtNirWashing, rtWashingCount);
                }
                else
                {
                    nirTimer.SetWashingTimer(ref c, rtNirScanning, rtNirCount, rtNirWashing, rtWashingCount);
                }
            }
        }

        private void rtNirScanning_TextChanged(object sender, EventArgs e)
        {
            int c = 0;
            if (this.rtNirScanning.Text != "")
            {
                if (nirTimer.nirTimerList.Count > 0)
                {
                    for (int i = 0; i < nirTimer.nirTimerList.Count;)
                    {
                        nirTimer.nirTimerList[i].Stop();
                        nirTimer.nirTimerList.RemoveAt(i);
                    }
                    nirTimer.SetNirTimer(ref c, rtNirScanning, rtNirCount);
                }
                else
                {
                    nirTimer.SetNirTimer(ref c, rtNirScanning, rtNirCount);
                }
            }
        }




        /*+=============================================== MOUSE HOVERS ====================================================+*/

        private void btnEditConfigs_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnEditConfigs, "Edit Application Configuration");
        }

        private void btnUndoEntry_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnUndoEntry, "Undo Last Entry");
        }

        private void btnPause_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnPause, "Pause Application");
        }

        private void btnDecrement_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnDecrement, "Decrement Side Cane Count");
        }

        private void btnLoadData_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnLoadData, "Load Saved Data");
        }

        private void btnReset_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnReset, "Restart Application");
        }




        /*+============================================ BUTTON OPERATIONS ==================================================+*/

        private void btnTipperOne_Click(object sender, EventArgs e)
        {
            string type = "Tipper One";

            //TipperOneValue();

            if (rtTipperOneBn.Text == "")
            {
                MessageBox.Show("Tipper One Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtTipperOneBn.Text;

                if (rtTipOneBn1.Text == "" && rtTipOneBx1.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn1, rtTipOneBx1));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn1, rtTipOneBx1, type));
                    rtTipOneBn1.Text = batch;
                    rtTipOneBx1.Text = "0";
                    rtTipOneBx1.BackColor = Color.Maroon;
                    rtTipOneBx1.ForeColor = Color.White;
                }

                else if (rtTipOneBn2.Text == "" && rtTipOneBx2.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn2, rtTipOneBx2));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn2, rtTipOneBx2, type));
                    rtTipOneBn2.Text = batch;
                    rtTipOneBx2.Text = "0";
                    rtTipOneBx2.BackColor = Color.Maroon;
                    rtTipOneBx2.ForeColor = Color.White;
                }

                else if (rtTipOneBn3.Text == "" && rtTipOneBx3.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn3, rtTipOneBx3));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn3, rtTipOneBx3, type));
                    rtTipOneBn3.Text = batch;
                    rtTipOneBx3.Text = "0";
                    rtTipOneBx3.BackColor = Color.Maroon;
                    rtTipOneBx3.ForeColor = Color.White;
                }

                else if (rtTipOneBn4.Text == "" && rtTipOneBx4.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn4, rtTipOneBx4));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn4, rtTipOneBx4, type));
                    rtTipOneBn4.Text = batch;
                    rtTipOneBx4.Text = "0";
                    rtTipOneBx4.BackColor = Color.Maroon;
                    rtTipOneBx4.ForeColor = Color.White;
                }

                else if (rtTipOneBn5.Text == "" && rtTipOneBx5.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn5, rtTipOneBx5));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn5, rtTipOneBx5, type));
                    rtTipOneBn5.Text = batch;
                    rtTipOneBx5.Text = "0";
                    rtTipOneBx5.BackColor = Color.Maroon;
                    rtTipOneBx5.ForeColor = Color.White;
                }

                else if (rtTipOneBn6.Text == "" && rtTipOneBx6.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn6, rtTipOneBx6));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn6, rtTipOneBx6, type));
                    rtTipOneBn6.Text = batch;
                    rtTipOneBx6.Text = "0";
                    rtTipOneBx6.BackColor = Color.Maroon;
                    rtTipOneBx6.ForeColor = Color.White;
                }

                else if (rtTipOneBn7.Text == "" && rtTipOneBx7.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn7, rtTipOneBx7));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn7, rtTipOneBx7, type));
                    rtTipOneBn7.Text = batch;
                    rtTipOneBx7.Text = "0";
                    rtTipOneBx7.BackColor = Color.Maroon;
                    rtTipOneBx7.ForeColor = Color.White;
                }

                else if (rtTipOneBn8.Text == "" && rtTipOneBx8.Text == "")
                {
                    bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(rtTipOneBn8, rtTipOneBx8));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn8, rtTipOneBx8, type));
                    rtTipOneBn8.Text = batch;
                    rtTipOneBx8.Text = "0";
                    rtTipOneBx8.BackColor = Color.Maroon;
                    rtTipOneBx8.ForeColor = Color.White;
                }

                logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Tipper One Area";
                LogOutput(logTextOutput);

                rtTipperOneBn.Text = "";

                IncrementSeriesNo();
            }
        }

        private void btnTipperTwo_Click(object sender, EventArgs e)
        {
            string type = "Tipper Two";

            //TipperTwoValue();

            if (rtTipperTwoBn.Text == "")
            {
                MessageBox.Show("Tipper Two Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtTipperTwoBn.Text;

                if (rtTipTwoBn1.Text == "" && rtTipTwoBx1.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn1, rtTipTwoBx1));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn1, rtTipTwoBx1, type));
                    rtTipTwoBn1.Text = batch;
                    rtTipTwoBx1.Text = "0";
                    rtTipTwoBx1.BackColor = Color.Maroon;
                    rtTipTwoBx1.ForeColor = Color.White;
                }

                else if (rtTipTwoBn2.Text == "" && rtTipTwoBx2.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn2, rtTipTwoBx2));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn2, rtTipTwoBx2, type));
                    rtTipTwoBn2.Text = batch;
                    rtTipTwoBx2.Text = "0";
                    rtTipTwoBx2.BackColor = Color.Maroon;
                    rtTipTwoBx2.ForeColor = Color.White;
                }

                else if (rtTipTwoBn3.Text == "" && rtTipTwoBx3.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn3, rtTipTwoBx3));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn3, rtTipTwoBx3, type));
                    rtTipTwoBn3.Text = batch;
                    rtTipTwoBx3.Text = "0";
                    rtTipTwoBx3.BackColor = Color.Maroon;
                    rtTipTwoBx3.ForeColor = Color.White;
                }

                else if (rtTipTwoBn4.Text == "" && rtTipTwoBx4.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn4, rtTipTwoBx4));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn4, rtTipTwoBx4, type));
                    rtTipTwoBn4.Text = batch;
                    rtTipTwoBx4.Text = "0";
                    rtTipTwoBx4.BackColor = Color.Maroon;
                    rtTipTwoBx4.ForeColor = Color.White;
                }

                else if (rtTipTwoBn5.Text == "" && rtTipTwoBx5.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn5, rtTipTwoBx5));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn5, rtTipTwoBx5, type));
                    rtTipTwoBn5.Text = batch;
                    rtTipTwoBx5.Text = "0";
                    rtTipTwoBx5.BackColor = Color.Maroon;
                    rtTipTwoBx5.ForeColor = Color.White;
                }

                else if (rtTipTwoBn6.Text == "" && rtTipTwoBx6.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn6, rtTipTwoBx6));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn6, rtTipTwoBx6, type));
                    rtTipTwoBn6.Text = batch;
                    rtTipTwoBx6.Text = "0";
                    rtTipTwoBx6.BackColor = Color.Maroon;
                    rtTipTwoBx6.ForeColor = Color.White;
                }

                else if (rtTipTwoBn7.Text == "" && rtTipTwoBx7.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn7, rtTipTwoBx7));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn7, rtTipTwoBx7, type));
                    rtTipTwoBn7.Text = batch;
                    rtTipTwoBx7.Text = "0";
                    rtTipTwoBx7.BackColor = Color.Maroon;
                    rtTipTwoBx7.ForeColor = Color.White;
                }

                else if (rtTipTwoBn8.Text == "" && rtTipTwoBx8.Text == "")
                {
                    bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(rtTipTwoBn8, rtTipTwoBx8));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn8, rtTipTwoBx8, type));
                    rtTipTwoBn8.Text = batch;
                    rtTipTwoBx8.Text = "0";
                    rtTipTwoBx8.BackColor = Color.Maroon;
                    rtTipTwoBx8.ForeColor = Color.White;
                }

                logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Tipper Two Area";
                LogOutput(logTextOutput);

                rtTipperTwoBn.Text = "";

                IncrementSeriesNo();
            }
        }

        private void btnDumpTruck_Click(object sender, EventArgs e)
        {
            string type = "Dump Truck";

            //DumpAndStockPileValue();

            if (rtDumpTruckBn.Text == "")
            {
                MessageBox.Show("Dump Truck Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtDumpTruckBn.Text;

                if (rtDumpBn1.Text == "" && rtDumpBx1.Text == "")
                {
                    bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn1, rtDumpBx1));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn1, rtDumpBx1, type));
                    rtDumpBn1.Text = batch;
                    rtDumpBx1.Text = "0";
                    rtDumpBx1.BackColor = Color.Maroon;
                    rtDumpBx1.ForeColor = Color.White;
                }

                else if (rtDumpBn2.Text == "" && rtDumpBx2.Text == "")
                {
                    bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn2, rtDumpBx2));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn2, rtDumpBx2, type));
                    rtDumpBn2.Text = batch;
                    rtDumpBx2.Text = "0";
                    rtDumpBx2.BackColor = Color.Maroon;
                    rtDumpBx2.ForeColor = Color.White;
                }

                else if (rtDumpBn3.Text == "" && rtDumpBx3.Text == "")
                {
                    bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn3, rtDumpBx3));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn3, rtDumpBx3, type));
                    rtDumpBn3.Text = batch;
                    rtDumpBx3.Text = "0";
                    rtDumpBx3.BackColor = Color.Maroon;
                    rtDumpBx3.ForeColor = Color.White;
                }

                else if (rtDumpBn4.Text == "" && rtDumpBx4.Text == "")
                {
                    bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn4, rtDumpBx4));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn4, rtDumpBx4, type));
                    rtDumpBn4.Text = batch;
                    rtDumpBx4.Text = "0";
                    rtDumpBx4.BackColor = Color.Maroon;
                    rtDumpBx4.ForeColor = Color.White;
                }

                else if (rtDumpBn5.Text == "" && rtDumpBx5.Text == "")
                {
                    bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn5, rtDumpBx5));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn5, rtDumpBx5, type));
                    rtDumpBn5.Text = batch;
                    rtDumpBx5.Text = "0";
                    rtDumpBx5.BackColor = Color.Maroon;
                    rtDumpBx5.ForeColor = Color.White;
                }

                else if (rtDumpBn6.Text == "" && rtDumpBx6.Text == "")
                {
                    bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn6, rtDumpBx6));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn6, rtDumpBx6, type));
                    rtDumpBn6.Text = batch;
                    rtDumpBx6.Text = "0";
                    rtDumpBx6.BackColor = Color.Maroon;
                    rtDumpBx6.ForeColor = Color.White;
                }

                /*else if (rtDumpBn7.Text == "" && rtDumpBx7.Text == "")
                {
                    dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn7, rtDumpBx7));
                    rtDumpBn7.Text = batch;
                    rtDumpBx7.Text = "0";
                    rtDumpBx7.BackColor = Color.Maroon;
                    rtDumpBx7.ForeColor = Color.White;
                }

                else if (rtDumpBn8.Text == "" && rtDumpBx8.Text == "")
                {
                    dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(rtDumpBn8, rtDumpBx8));
                    rtDumpBn8.Text = batch;
                    rtDumpBx8.Text = "0";
                    rtDumpBx8.BackColor = Color.Maroon;
                    rtDumpBx8.ForeColor = Color.White;
                }*/

                logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Dump Truck Area";
                LogOutput(logTextOutput);

                rtDumpTruckBn.Text = "";

                IncrementSeriesNo();
            }
        }

        private void btnStockPile_Click(object sender, EventArgs e)
        {
            string type = "Stock Pile";

            //DumpAndStockPileValue();

            if (rtStockPileBn.Text == "")
            {
                MessageBox.Show("Stock Pile Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtStockPileBn.Text;

                if (rtStockBn1.Text == "" && rtStockBx1.Text == "")
                {
                    bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn1, rtStockBx1));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn1, rtStockBx1, type));
                    rtStockBn1.Text = batch;
                    rtStockBx1.Text = "0";
                    rtStockBx1.BackColor = Color.Maroon;
                    rtStockBx1.ForeColor = Color.White;
                }

                else if (rtStockBn2.Text == "" && rtStockBx2.Text == "")
                {
                    bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn2, rtStockBx2));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn2, rtStockBx2, type));
                    rtStockBn2.Text = batch;
                    rtStockBx2.Text = "0";
                    rtStockBx2.BackColor = Color.Maroon;
                    rtStockBx2.ForeColor = Color.White;
                }

                else if (rtStockBn3.Text == "" && rtStockBx3.Text == "")
                {
                    bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn3, rtStockBx3));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn3, rtStockBx3, type));
                    rtStockBn3.Text = batch;
                    rtStockBx3.Text = "0";
                    rtStockBx3.BackColor = Color.Maroon;
                    rtStockBx3.ForeColor = Color.White;
                }

                else if (rtStockBn4.Text == "" && rtStockBx4.Text == "")
                {
                    bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn4, rtStockBx4));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn4, rtStockBx4, type));
                    rtStockBn4.Text = batch;
                    rtStockBx4.Text = "0";
                    rtStockBx4.BackColor = Color.Maroon;
                    rtStockBx4.ForeColor = Color.White;
                }

                else if (rtStockBn5.Text == "" && rtStockBx5.Text == "")
                {
                    bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn5, rtStockBx5));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn5, rtStockBx5, type));
                    rtStockBn5.Text = batch;
                    rtStockBx5.Text = "0";
                    rtStockBx5.BackColor = Color.Maroon;
                    rtStockBx5.ForeColor = Color.White;
                }

                else if (rtStockBn6.Text == "" && rtStockBx6.Text == "")
                {
                    bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn6, rtStockBx6));
                    bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn6, rtStockBx6, type));
                    rtStockBn6.Text = batch;
                    rtStockBx6.Text = "0";
                    rtStockBx6.BackColor = Color.Maroon;
                    rtStockBx6.ForeColor = Color.White;
                }

                /*else if (rtStockBn7.Text == "" && rtStockBx7.Text == "")
                {
                    stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn7, rtStockBx7));
                    rtStockBn7.Text = batch;
                    rtStockBx7.Text = "0";
                    rtStockBx7.BackColor = Color.Maroon;
                    rtStockBx7.ForeColor = Color.White;
                }

                else if (rtStockBn8.Text == "" && rtStockBx8.Text == "")
                {
                    stockPile.Add(new Tuple<RichTextBox, RichTextBox>(rtStockBn8, rtStockBx8));
                    rtStockBn8.Text = batch;
                    rtStockBx8.Text = "0";
                    rtStockBx8.BackColor = Color.Maroon;
                    rtStockBx8.ForeColor = Color.White;
                }*/

                logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Stock Pile Area";
                LogOutput(logTextOutput);

                rtStockPileBn.Text = "";

                IncrementSeriesNo();
            }
        }

        private void btnEditConfigs_Click(object sender, EventArgs e)
        {
            frmEditConfigs frm = new frmEditConfigs();
            frm.Show();
        }

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            string t;
            if (decrementing)
            {
                decrementing = false;
                t = DateTime.Now.ToString() + " : " + "Incrementing";
                btnDecrement.BackColor = Color.White;
            }
            else
            {
                decrementing = true;
                t = DateTime.Now.ToString() + " : " + "Decrementing";
                btnDecrement.BackColor = Color.Red;
            }

            LogOutput(t);
        }

        private void btnUndoEntry_Click(object sender, EventArgs e)
        {
            int i;

            if (bnlist.dumpCanesHistory.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Are you sure you want to undo last entry?", "Confirmation", MessageBoxButtons.YesNo);

                if (dialog == DialogResult.Yes)
                {
                    if (bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item3.Equals("Tipper One"))
                    {
                        for (i = 0; i < bnlist.tipperOne.Count; i++)
                        {
                            if (bnlist.tipperOne[i].Item1 == bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1)
                            {
                                bnlist.tipperOne.RemoveAt(i);
                            }
                        }
                    }
                    else if (bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item3.Equals("Tipper Two"))
                    {
                        for (i = 0; i < bnlist.tipperTwo.Count; i++)
                        {
                            if (bnlist.tipperTwo[i].Item1 == bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1)
                            {
                                bnlist.tipperTwo.RemoveAt(i);
                            }
                        }
                    }
                    else if (bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item3.Equals("Dump Truck"))
                    {
                        for (i = 0; i < bnlist.dumpTruck.Count; i++)
                        {
                            if (bnlist.dumpTruck[i].Item1 == bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1)
                            {
                                bnlist.dumpTruck.RemoveAt(i);
                            }
                        }
                    }
                    else if (bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item3.Equals("Stock Pile"))
                    {
                        for (i = 0; i < bnlist.stockPile.Count; i++)
                        {
                            if (bnlist.stockPile[i].Item1 == bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1)
                            {
                                bnlist.stockPile.RemoveAt(i);
                            }
                        }
                    }


                    string t = DateTime.Now.ToString() + " : " + "Removed Batch #" + bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1.Text
                                                       + " at " + bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item3;
                    LogOutput(t);

                    ctcc.ChangeText(bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1, "");
                    ctcc.ChangeText(bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item2, "");
                    ctcc.ChangeColorTextBox(bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item2, Color.CornflowerBlue);

                    bnlist.dumpCanesHistory.RemoveAt(bnlist.dumpCanesHistory.Count - 1);

                    DecrementSeriesNo();
                }
            }
            else
            {
                MessageBox.Show("Nothing to Undo", "Notification");
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            string t;
            if (pause)
            {
                pause = false;
                t = DateTime.Now.ToString() + " : " + "Resuming countings";
                btnPause.BackColor = Color.White;
            }
            else
            {
                pause = true;
                t = DateTime.Now.ToString() + " : " + "Pause countings";
                btnPause.BackColor = Color.Red;
            }

            LogOutput(t);
            nirTimer.PauseNirCount(pause);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to restart the application?", "Confirmation", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            frmLoadData frm = new frmLoadData();
            frm.Show();
        }




        /*+======================================== SENSOR/INCREMENT-DECREMENT METHODS ===============================================+*/

        //Serial Connection
        private void InitializeSerialConnections()
        {
            try
            {
                serialPort = new SerialPort("COM3", 9600);

                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                serialPort.DataReceived += new SerialDataReceivedEventHandler(CaneSensorDataReceivedHandler);

                string t = DateTime.Now.ToString() + " : " + "Ports Activated";
                LogOutput(t);
            }
            catch (Exception)
            {
                string t = DateTime.Now.ToString() + " : " + "Port Connections does not exist or is disconnected";
                //MessageBox.Show(t);
                LogOutput(t);
            }
        }

        //Serial Data Received
        private void CaneSensorDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            serialPort = (SerialPort)sender;
            string incomingData = serialPort.ReadLine().TrimEnd('\r');

            sc.SensorIndicator(incomingData, rtSideCaneCheck, rtMainCaneCheck, rtCaneKnivesCheck, rtShredderCheck);

            sc.TipperOne(incomingData, pause, decrementing, bnlist);

            sc.TipperTwo(incomingData, pause, decrementing, bnlist);

            sc.DumpTruck(incomingData, pause, decrementing, bnlist);

            sc.StockPile(incomingData, pause, decrementing, bnlist);

            sc.MainCane(incomingData, pause, bnlist,
                        rtMainBn1, rtMainBn2, rtMainBn3, rtMainBn4, rtMainBx1, rtMainBx2, rtMainBx3, rtMainBx4);

            sc.CaneKnives(incomingData, pause, bnlist,
                          rtKnivesBn1, rtKnivesBn2, rtKnivesBx1, rtKnivesBx2);

            sc.ShreddedCane(incomingData, pause, bnlist,
                            rtShredBn1, rtShredBn2, rtShredBx1, rtShredBx2, rtNirWashing, rtWashingCount);
        }


        /*
         * Increment Series No. during Side Cane Dumpings
         * Tipper One click
         * Tipper Two click
         * Dump Truck click
         * Stock PIle click
         * 
         * 
         * Decrement Series No. during Undo Inputs
         * Undo Entry click
         */
        private void IncrementSeriesNo()
        {
            rtSeriesNo.Text = (seriesNo += 1).ToString();
        }

        private void DecrementSeriesNo()
        {
            rtSeriesNo.Text = (seriesNo -= 1).ToString();
        }




        /*+================================== APP STATE DATABASE SAVING AND CONNECTION =====================================+*/

        private void ConnectToDB()
        {
            try
            {
                con.Open();
                LogOutput(DateTime.Now.ToString() + " : Successfuly Connected to Application's Database");
                con.Close();

                SaveStateStart();
            }
            catch (Exception e)
            {
                LogOutput(e.ToString());
            }
        }

        private void ListBox()
        {
            List<Tuple<RichTextBox, RichTextBox, string>> lTbox = new List<Tuple<RichTextBox, RichTextBox, string>>();

            //Tipper One
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn1, rtTipOneBx1, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn2, rtTipOneBx2, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn3, rtTipOneBx3, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn4, rtTipOneBx4, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn5, rtTipOneBx5, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn6, rtTipOneBx6, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn7, rtTipOneBx7, "TipOne"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn8, rtTipOneBx8, "TipOne"));

            //Tipper Two
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn1, rtTipTwoBx1, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn2, rtTipTwoBx2, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn3, rtTipTwoBx3, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn4, rtTipTwoBx4, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn5, rtTipTwoBx5, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn6, rtTipTwoBx6, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn7, rtTipTwoBx7, "TipTwo"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn8, rtTipTwoBx8, "TipTwo"));

            //Dump Truck
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn1, rtDumpBx1, "DumpTruck"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn2, rtDumpBx2, "DumpTruck"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn3, rtDumpBx3, "DumpTruck"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn4, rtDumpBx4, "DumpTruck"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn5, rtDumpBx5, "DumpTruck"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn6, rtDumpBx6, "DumpTruck"));

            //Stock Pile
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn1, rtStockBx1, "StockPile"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn2, rtStockBx2, "StockPile"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn3, rtStockBx3, "StockPile"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn4, rtStockBx4, "StockPile"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn5, rtStockBx5, "StockPile"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn6, rtStockBx6, "StockPile"));

            //Main Cane
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn1, rtMainBx1, "MainCane"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn2, rtMainBx2, "MainCane"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn3, rtMainBx3, "MainCane"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn4, rtMainBx4, "MainCane"));

            //Cane Knives
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtKnivesBn1, rtKnivesBx1, "CaneKnives"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtKnivesBn2, rtKnivesBx2, "CaneKnives"));

            //Shredded Cane
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtShredBn1, rtShredBx1, "Shredder"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtShredBn2, rtShredBx2, "Shredder"));

            //Foss NIR
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtNirWashing, rtWashingCount, "Nir"));
            lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtNirScanning, rtNirCount, "Nir"));

            try
            {
                foreach (var i in lTbox)
                {
                    if (i.Item1.Text != "")
                    {
                        SaveState(i.Item1.Rtf, i.Item2.Rtf, int.Parse(i.Item1.Text), int.Parse(i.Item2.Text), i.Item3);
                    }
                }
            }
            catch (Exception e)
            {
                LogOutput(e.ToString());
            }
            finally
            {
                LogOutput(DateTime.Now.ToString() + " : State save");

                stateBatchNum++;
            }

        }

        private void SaveState(string batchNum, string numCount, int num, int count, string areaName)
        {
            int bNumber = 0;
            int bCount = 0;

            if (batchNum != "" && numCount != "")
            {
                bNumber = num;
                bCount = count;
            }

            string saveState = @"INSERT INTO app_state_record
                                 (batchNumBox, batchNum, countBox, currentCount, areaName, stateBatch, batchDate)
                               VALUES
                                 (@bNumBox, @bNum, @cntBox, @cCount, @aName, @sBatch, @bDate)";

            SqlCommand cmd = new SqlCommand(saveState, con);

            cmd.Parameters.AddWithValue("@bNumBox", batchNum);
            cmd.Parameters.AddWithValue("@bNum", bNumber);
            cmd.Parameters.AddWithValue("@cntBox", numCount);
            cmd.Parameters.AddWithValue("@cCount", bCount);
            cmd.Parameters.AddWithValue("@aName", areaName);
            cmd.Parameters.AddWithValue("@sBatch", stateBatchNum);
            cmd.Parameters.AddWithValue("@bDate", DateTime.Now);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogOutput(e.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void SaveStateStart()
        {
            savingStateTimer = new Timer();
            savingStateTimer.Interval = 10000;
            savingStateTimer.Enabled = true;
            savingStateTimer.Tick += new EventHandler(SaveState_Tick);
        }

        private void SaveState_Tick(object sender, EventArgs e)
        {
            ListBox();
        }




        /*+===================================== CHECKS ON CONFIGURATION CHANGES ========================================+*/

        private void CheckConfigStart()
        {
            checkConfigsTimer = new Timer();
            checkConfigsTimer.Interval = 1;
            checkConfigsTimer.Enabled = true;
            checkConfigsTimer.Tick += new EventHandler(CheckConfig_Tick);
        }

        private void CheckConfig_Tick(object sender, EventArgs e)
        {
            DefaultValues();
        }




        /*+======================================== OTHER METHODS ==========================================+*/

        //For logging output at the bottom richtextbox
        private void LogOutput(string output)
        {
            if (!string.IsNullOrEmpty(rtEvents.Text))
            {
                rtEvents.AppendText("\r\n" + output);
            }
            else
            {
                rtEvents.AppendText(output);
            }
            rtEvents.ScrollToCaret();
        }

        //Just for centering text on richtexbox
        private void TextAlignment()
        {
            rtTipperOneBn.SelectionAlignment = HorizontalAlignment.Center;
            rtTipperTwoBn.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpTruckBn.SelectionAlignment = HorizontalAlignment.Center;
            rtStockPileBn.SelectionAlignment = HorizontalAlignment.Center;

            rtTipOneBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn4.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn5.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn6.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn7.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBn8.SelectionAlignment = HorizontalAlignment.Center;

            rtTipOneBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx4.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx5.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx6.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx7.SelectionAlignment = HorizontalAlignment.Center;
            rtTipOneBx8.SelectionAlignment = HorizontalAlignment.Center;

            rtTipTwoBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn4.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn5.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn6.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn7.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBn8.SelectionAlignment = HorizontalAlignment.Center;

            rtTipTwoBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx4.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx5.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx6.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx7.SelectionAlignment = HorizontalAlignment.Center;
            rtTipTwoBx8.SelectionAlignment = HorizontalAlignment.Center;

            rtDumpBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn4.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn5.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn6.SelectionAlignment = HorizontalAlignment.Center;
            /*rtDumpBn7.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn8.SelectionAlignment = HorizontalAlignment.Center;*/

            rtDumpBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx4.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx5.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx6.SelectionAlignment = HorizontalAlignment.Center;
            /*rtDumpBx7.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx8.SelectionAlignment = HorizontalAlignment.Center;*/

            rtStockBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn4.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn5.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn6.SelectionAlignment = HorizontalAlignment.Center;
           /* rtStockBn7.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn8.SelectionAlignment = HorizontalAlignment.Center;*/

            rtStockBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx4.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx5.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx6.SelectionAlignment = HorizontalAlignment.Center;
            /*rtStockBx7.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx8.SelectionAlignment = HorizontalAlignment.Center;*/

            rtMainBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBn4.SelectionAlignment = HorizontalAlignment.Center;

            rtMainBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBx4.SelectionAlignment = HorizontalAlignment.Center;

            rtMainBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBn4.SelectionAlignment = HorizontalAlignment.Center;

            rtMainBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtMainBx4.SelectionAlignment = HorizontalAlignment.Center;

            rtKnivesBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtKnivesBn2.SelectionAlignment = HorizontalAlignment.Center;
            /*rtKnivesBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtKnivesBn4.SelectionAlignment = HorizontalAlignment.Center;*/

            rtKnivesBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtKnivesBx2.SelectionAlignment = HorizontalAlignment.Center;
            /*rtKnivesBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtKnivesBx4.SelectionAlignment = HorizontalAlignment.Center;*/

            rtShredBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtShredBn2.SelectionAlignment = HorizontalAlignment.Center;
           /* rtShredBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtShredBn4.SelectionAlignment = HorizontalAlignment.Center;*/

            rtShredBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtShredBx2.SelectionAlignment = HorizontalAlignment.Center;
            /*rtShredBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtShredBx4.SelectionAlignment = HorizontalAlignment.Center;*/

            rtNirWashing.SelectionAlignment = HorizontalAlignment.Center;
            rtNirScanning.SelectionAlignment = HorizontalAlignment.Center;
            /*rtFossBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtFossBn4.SelectionAlignment = HorizontalAlignment.Center;*/

            rtWashingCount.SelectionAlignment = HorizontalAlignment.Center;
            rtNirCount.SelectionAlignment = HorizontalAlignment.Center;
            /*rtFossBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtFossBx4.SelectionAlignment = HorizontalAlignment.Center;*/

            rtForceScan.SelectionAlignment = HorizontalAlignment.Center;
            rtSeriesNo.SelectionAlignment = HorizontalAlignment.Center;

            rtTrashBatchNum.SelectionAlignment = HorizontalAlignment.Center;
            rtLeaves.SelectionAlignment = HorizontalAlignment.Right;
            rtCaneTops.SelectionAlignment = HorizontalAlignment.Right;
            rtRoots.SelectionAlignment = HorizontalAlignment.Right;
            rtDeadStalks.SelectionAlignment = HorizontalAlignment.Right;
            rtMixedBurned.SelectionAlignment = HorizontalAlignment.Right;
            rtBurned.SelectionAlignment = HorizontalAlignment.Right;
            rtMud.SelectionAlignment = HorizontalAlignment.Right;
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        //Counting default values
        private void DefaultValues()
        {
            Classes.CountInterval cnt = new Classes.CountInterval();
            lblT1.Text = cnt.TipperOneMaxCount.ToString();
            lblT2.Text = cnt.TipperTwoMaxCount.ToString();
            lblDs.Text = cnt.DumpAndPileMaxCount.ToString();
            lblMc.Text = cnt.MainCaneMaxCount.ToString();
            lblCk.Text = cnt.KnivesAndShredderMaxCount.ToString();
            lblNi.Text = cnt.NirTime.ToString();

            rtSeriesNo.Text = (seriesNo).ToString();
        }

    }
}
