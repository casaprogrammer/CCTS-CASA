using Cane_Tracking.Classes;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Cane_Tracking
{
    public partial class frmMain : Form
    {

        CrossThreadingCheck ctcc = new CrossThreadingCheck();
        ConfigValues cnf = new ConfigValues();
        CaneDataUpdate cdu = new CaneDataUpdate();
        ConnectionsCheck cc = new ConnectionsCheck();
        TrackingList bnlist = new TrackingList();
        NirTimer nirTimer = new NirTimer();
        LoadingValues appLoading = new LoadingValues();
        AppLogging log = new AppLogging();
        ToolTip toolTip = new ToolTip();
        NirUDP nirUdp = new NirUDP();

        private static SerialPort serialPort;

        private Timer checkConfigsTimer;
        private Timer savingStateTimer;

        private static int seriesNo = 0;
        private static int savedCount = 0;
        private static double trashTotal = 0;

        private static bool decrementing = false;
        private static bool pause = false;
        private static bool forceScan = false;
        private static bool connected = false;
        private static bool restarting = false;

        private static string currentScannedSample;
        private static string logTextOutput;
        private static string batch;

        readonly char pad = '0'; //For adding zeroes to the left

        public frmMain()
        {
            InitializeComponent();
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

        private void rtMainBn1_TextChanged(object sender, EventArgs e)
        {
            if (rtMainBn1.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtMainBn1.Text + " is already in the main cane area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtMainBn2_TextChanged(object sender, EventArgs e)
        {
            if (rtMainBn2.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtMainBn2.Text + " is already in the main cane area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtMainBn3_TextChanged(object sender, EventArgs e)
        {
            if (rtMainBn3.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtMainBn3.Text + " is already in the main cane area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtMainBn4_TextChanged(object sender, EventArgs e)
        {
            if (rtMainBn4.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtMainBn4.Text + " is already in the main cane area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtKnivesBn1_TextChanged(object sender, EventArgs e)
        {
            if (rtKnivesBn1.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtKnivesBn1.Text + " is already in the cane knives area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtKnivesBn2_TextChanged(object sender, EventArgs e)
        {
            if (rtKnivesBn2.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtKnivesBn2.Text + " is already in the cane knives area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtShredBn1_TextChanged(object sender, EventArgs e)
        {
            if (rtShredBn1.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtShredBn1.Text + " is already in the shredder area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtShredBn2_TextChanged(object sender, EventArgs e)
        {
            if (rtShredBn2.Text != "")
            {
                logTextOutput = DateTime.Now.ToString() + " : Batch #" + rtShredBn2.Text + " is already in the shredder area. ";
                LogOutput(logTextOutput);

                log.LogEvent(logTextOutput);
            }
        }

        private void rtNirWashing_TextChanged(object sender, EventArgs e)
        {
            if (this.rtNirWashing.Text != "")
            {
                if (nirTimer.washingTimerList.Count > 0)
                {
                    for (int i = 0; i < nirTimer.washingTimerList.Count;)
                    {
                        nirTimer.washingTimerList[i].Stop();
                        nirTimer.washingTimerList.RemoveAt(i);
                    }

                    nirTimer.SetWashingTimer(rtNirScanning, rtNirCount, rtNirWashing, rtWashingCount);
                    logTextOutput = DateTime.Now.ToString() + " : Preparing Batch #" + rtNirWashing.Text + " for scanning";

                    LogOutput(logTextOutput);
                    log.LogEvent(logTextOutput);
                }
                else
                {
                    nirTimer.SetWashingTimer(rtNirScanning, rtNirCount, rtNirWashing, rtWashingCount);
                    logTextOutput = DateTime.Now.ToString() + " : Preparing Batch #" + rtNirWashing.Text + " for scanning";

                    LogOutput(logTextOutput);
                    log.LogEvent(logTextOutput);

                    if (rtNirScanning.Text != "")
                    {
                        for (int i = 0; i < nirTimer.nirTimerList.Count;)
                        {
                            nirTimer.nirTimerList[i].Stop();
                            nirTimer.nirTimerList.RemoveAt(i);
                        }

                        ctcc.ChangeText(rtNirScanning, "");
                        ctcc.ChangeText(rtNirCount, "");
                        ctcc.ChangeColorTextBox(rtNirCount, Color.CornflowerBlue);
                    }
                }
            }
            else
            {
                //Stop countings during Loading of state values
                if (nirTimer.washingTimerList.Count > 0)
                {
                    for (int i = 0; i < nirTimer.washingTimerList.Count;)
                    {
                        nirTimer.washingTimerList[i].Stop();
                        nirTimer.washingTimerList.RemoveAt(i);
                    }
                }
            }
        }

        private void rtNirScanning_TextChanged(object sender, EventArgs e)
        {
            if (rtNirScanning.Text != "")
            {
                nirTimer.SetNirTimer(rtNirScanning, rtNirCount);

                IncrementSeriesNo();

                currentScannedSample = rtNirScanning.Text + rtSeriesNo.Text.PadLeft(3, pad);

                nirUdp.SendMessage(currentScannedSample);

                logTextOutput = DateTime.Now.ToString() + " : Scanning Sample #" + currentScannedSample;

                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);

            }
            else
            {
                if (seriesNo > 0)
                {
                    logTextOutput = DateTime.Now.ToString() + " : Finished Scanning Sample #" + currentScannedSample; //currentScannedSeries;

                    nirUdp.EndMessage(currentScannedSample);

                    LogOutput(logTextOutput);
                    log.LogEvent(logTextOutput);
                }

                //Stop countings during Loading of state values
                if (nirTimer.nirTimerList.Count > 0)
                {
                    for (int i = 0; i < nirTimer.nirTimerList.Count;)
                    {
                        nirTimer.nirTimerList[i].Stop();
                        nirTimer.nirTimerList.RemoveAt(i);
                    }
                }
            }

        }

        private void rtForceScan_TextChanged(object sender, EventArgs e)
        {
            if (rtForceScan.Text == "")
            {
                if (forceScan)
                {
                    logTextOutput = DateTime.Now.ToString() + " : Finished Forced Scanning Sample #" + currentScannedSample; //currentScannedSeries;

                    nirUdp.EndMessage(currentScannedSample);

                    LogOutput(logTextOutput);
                    log.LogEvent(logTextOutput);

                    rtForceScan.Enabled = true;
                    btnForceScan.Enabled = true;
                    forceScan = false;
                }

                btnForceScan.Enabled = false;
            }
            else
            {
                btnForceScan.Enabled = true;
            }
        }



        /*+======================================= TRASH CHECKER PANEL RICHTEXT BOX =======================================+*/

        private void rtTrashBatchNum_TextChanged(object sender, EventArgs e)
        {
            btnSaveTrash.Enabled = false;
            btnCancelTrash.Enabled = false;

            trashTotal = 0;

            rtTotalTrash.Text = "0";
            rtLeaves.Text = "0";
            rtCaneTops.Text = "0";
            rtRoots.Text = "0";
            rtDeadStalks.Text = "0";
            rtMixedBurned.Text = "0";
            rtBurned.Text = "0";
            rtMud.Text = "0";

            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
            rtLeaves.SelectionAlignment = HorizontalAlignment.Right;
            rtCaneTops.SelectionAlignment = HorizontalAlignment.Right;
            rtRoots.SelectionAlignment = HorizontalAlignment.Right;
            rtDeadStalks.SelectionAlignment = HorizontalAlignment.Right;
            rtMixedBurned.SelectionAlignment = HorizontalAlignment.Right;
            rtBurned.SelectionAlignment = HorizontalAlignment.Right;
            rtMud.SelectionAlignment = HorizontalAlignment.Right;

            dgvTrash.DataSource = null;

        }

        private void rtTrashBatchNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtTrashBatchNum.Text, @"^\d+$"))
                {
                    cdu.GetCaneData(dgvTrash, dtpCurrentDate, rtTrashBatchNum, rtLeaves);
                }
                else
                {
                    MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rtTrashBatchNum.Text = "";
                }
            }
        }

        private void rtLeaves_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtLeaves.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    rtCaneTops.Focus();
                    rtCaneTops.SelectAll();
                }
                else
                {
                    rtLeaves.Text = "0";
                    rtLeaves.Focus();
                    rtLeaves.SelectionAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private void rtCaneTops_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtCaneTops.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    rtTotalTrash.Text = trashTotal.ToString();
                    rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;

                    rtRoots.Focus();
                    rtRoots.SelectAll();
                }
                else
                {
                    rtCaneTops.Text = "0";
                    rtCaneTops.Focus();
                    rtCaneTops.SelectionAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private void rtRoots_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtRoots.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    rtDeadStalks.Focus();
                    rtDeadStalks.SelectAll();
                }
                else
                {
                    rtRoots.Text = "0";
                    rtRoots.Focus();
                    rtRoots.SelectionAlignment = HorizontalAlignment.Right;
                }

            }
        }

        private void rtDeadStalks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtDeadStalks.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    rtMixedBurned.Focus();
                    rtMixedBurned.SelectAll();
                }
                else
                {
                    rtDeadStalks.Text = "0";
                    rtDeadStalks.Focus();
                    rtDeadStalks.SelectionAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private void rtMixedBurned_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtMixedBurned.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    rtBurned.Focus();
                    rtBurned.SelectAll();
                }
                else
                {
                    rtMixedBurned.Text = "0";
                    rtMixedBurned.Focus();
                    rtDeadStalks.SelectionAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private void rtBurned_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtBurned.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    rtMud.Focus();
                    rtMud.SelectAll();
                }
                else
                {
                    rtBurned.Text = "0";
                    rtBurned.Focus();
                    rtBurned.SelectionAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private void rtMud_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (Regex.IsMatch(rtMud.Text, @"^[0-9]*\.?[0-9]+$"))
                {
                    trashTotal = double.Parse(rtLeaves.Text) + double.Parse(rtCaneTops.Text) + double.Parse(rtRoots.Text) + double.Parse(rtDeadStalks.Text) +
                                 double.Parse(rtMixedBurned.Text) + double.Parse(rtBurned.Text) + double.Parse(rtMud.Text);


                    rtTotalTrash.Text = trashTotal.ToString();
                    rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;


                    rtTotalTrash.Focus();
                }
                else
                {
                    rtMud.Text = "0";
                    rtMud.Focus();
                    rtMud.SelectionAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private void rtTotalTrash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (dgvTrash.RowCount > 0)
                {
                    btnSaveTrash.Enabled = true;
                    btnCancelTrash.Enabled = true;

                    btnSaveTrash.Focus();
                }
            }
        }

        private void rtLeaves_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rtCaneTops_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rtRoots_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rtDeadStalks_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rtMixedBurned_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rtBurned_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void rtMud_TextChanged(object sender, EventArgs e)
        {
            trashTotal = 0;
            rtTotalTrash.Text = "0";
            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
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

        private void btnEventLogs_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnEventLogs, "Event Logs");
        }

        private void btnReset_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnReset, "Restart Application");
        }

        private void btnConnections_MouseHover(object sender, EventArgs e)
        {
            toolTip.SetToolTip(this.btnConnections, "Check Connections");
        }




        /*+============================================= BUTTON ACTIONS ==================================================+*/

        private void btnTipperOne_Click(object sender, EventArgs e)
        {

            if (rtTipperOneBn.Text == "")
            {
                MessageBox.Show("Tipper One Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtTipperOneBn.Text.PadLeft(3, pad);

                for (int i = 0; i < bnlist.lTbox.Count; i++)
                {
                    if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "TipOne")
                    {
                        bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                        bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Tipper One"));
                        bnlist.lTbox[i].Item1.Text = batch;
                        bnlist.lTbox[i].Item2.Text = "0";
                        bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                        bnlist.lTbox[i].Item2.ForeColor = Color.White;


                        logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Tipper One Area";
                        LogOutput(logTextOutput);
                        log.LogEvent(logTextOutput);

                        rtTipperOneBn.Text = "";
                        break;
                    }
                }
            }
        }

        private void btnTipperTwo_Click(object sender, EventArgs e)
        {

            if (rtTipperTwoBn.Text == "")
            {
                MessageBox.Show("Tipper Two Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtTipperTwoBn.Text.PadLeft(3, pad);
                for (int i = 0; i < bnlist.lTbox.Count; i++)
                {
                    if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "TipTwo")
                    {
                        bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                        bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Tipper Two"));
                        bnlist.lTbox[i].Item1.Text = batch;
                        bnlist.lTbox[i].Item2.Text = "0";
                        bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                        bnlist.lTbox[i].Item2.ForeColor = Color.White;


                        logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Tipper Two Area";
                        LogOutput(logTextOutput);
                        log.LogEvent(logTextOutput);

                        rtTipperTwoBn.Text = "";
                        break;
                    }
                }
            }

        }

        private void btnDumpTruck_Click(object sender, EventArgs e)
        {

            if (rtDumpTruckBn.Text == "")
            {
                MessageBox.Show("Dump Truck Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtDumpTruckBn.Text.PadLeft(3, pad);
                for (int i = 0; i < bnlist.lTbox.Count; i++)
                {
                    if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "DumpTruck")
                    {
                        bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                        bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Dump Truck"));
                        bnlist.lTbox[i].Item1.Text = batch;
                        bnlist.lTbox[i].Item2.Text = "0";
                        bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                        bnlist.lTbox[i].Item2.ForeColor = Color.White;


                        logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Dump Truck Area";
                        LogOutput(logTextOutput);
                        log.LogEvent(logTextOutput);

                        rtDumpTruckBn.Text = "";
                        break;
                    }
                }
            }

        }

        private void btnStockPile_Click(object sender, EventArgs e)
        {

            if (rtStockPileBn.Text == "")
            {
                MessageBox.Show("Stock Pile Batch Number Field is blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                batch = rtStockPileBn.Text.PadLeft(3, pad);
                for (int i = 0; i < bnlist.lTbox.Count; i++)
                {
                    if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "StockPile")
                    {
                        bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                        bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Stock Pile"));
                        bnlist.lTbox[i].Item1.Text = batch;
                        bnlist.lTbox[i].Item2.Text = "0";
                        bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                        bnlist.lTbox[i].Item2.ForeColor = Color.White;


                        logTextOutput = DateTime.Now.ToString() + " : Dumped Batch #" + batch + " to Stock Pile Area";
                        LogOutput(logTextOutput);
                        log.LogEvent(logTextOutput);

                        rtStockPileBn.Text = "";
                        break;
                    }
                }
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
            log.LogEvent(t);
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
                    log.LogEvent(t);

                    ctcc.ChangeText(bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item1, "");
                    ctcc.ChangeText(bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item2, "");
                    ctcc.ChangeColorTextBox(bnlist.dumpCanesHistory[bnlist.dumpCanesHistory.Count - 1].Item2, Color.CornflowerBlue);

                    bnlist.dumpCanesHistory.RemoveAt(bnlist.dumpCanesHistory.Count - 1);

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

                nirTimer.PauseNir(pause);
                t = DateTime.Now.ToString() + " : " + "Resuming countings";
                btnPause.BackColor = Color.White;
            }
            else
            {
                pause = true;

                nirTimer.PauseNir(pause);
                t = DateTime.Now.ToString() + " : " + "Countings Paused";
                btnPause.BackColor = Color.Red;

            }

            LogOutput(t);
            log.LogEvent(t);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to restart the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == DialogResult.Yes)
            {
                string t = DateTime.Now + " : " + "Restarted Application";
                log.LogEvent(t);
                restarting = true;
                Application.Restart();
            }
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            frmLoadData frm = new frmLoadData();
            frm.Show();
        }

        private void btnEventLogs_Click(object sender, EventArgs e)
        {
            frmEventLogs frm = new frmEventLogs();
            frm.Show();
        }

        private void btnForceScan_Click(object sender, EventArgs e)
        {
            if (rtForceScan.Text != "")
            {
                if (Regex.IsMatch(rtForceScan.Text, @"^\d+$"))
                {
                    rtForceScan.Text = rtForceScan.Text.PadLeft(3, pad);

                    nirTimer.SetForceScanTimer(rtForceScan, btnForceScan);

                    IncrementSeriesNo();

                    currentScannedSample = rtForceScan.Text + rtSeriesNo.Text.PadLeft(3, pad);

                    nirUdp.SendMessage(currentScannedSample);

                    logTextOutput = DateTime.Now.ToString() + " : Force Scanning Sample #" + currentScannedSample;

                    LogOutput(logTextOutput);
                    log.LogEvent(logTextOutput);

                    rtForceScan.Enabled = false;
                    btnForceScan.Enabled = false;
                    btnForceScan.Text = "0";
                    forceScan = true;
                }
                else
                {
                    MessageBox.Show("Enter numeric values only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    rtForceScan.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Nothing to scan", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveTrash_Click(object sender, EventArgs e)
        {
            if (dgvTrash.RowCount > 0)
            {
                cdu.UpdateCaneData(dgvTrash, rtTotalTrash, rtLeaves, rtCaneTops,
                                   rtRoots, rtDeadStalks, rtMixedBurned, rtBurned, rtMud, rtTrashBatchNum);
            }
        }

        private void btnCancelTrash_Click(object sender, EventArgs e)
        {
            dgvTrash.DataSource = null;

            rtTrashBatchNum.Text = "";
            rtTotalTrash.Text = "0";
            rtLeaves.Text = "0";
            rtCaneTops.Text = "0";
            rtRoots.Text = "0";
            rtDeadStalks.Text = "0";
            rtMixedBurned.Text = "0";
            rtBurned.Text = "0";
            rtMud.Text = "0";

            rtTotalTrash.SelectionAlignment = HorizontalAlignment.Right;
            rtLeaves.SelectionAlignment = HorizontalAlignment.Right;
            rtCaneTops.SelectionAlignment = HorizontalAlignment.Right;
            rtRoots.SelectionAlignment = HorizontalAlignment.Right;
            rtDeadStalks.SelectionAlignment = HorizontalAlignment.Right;
            rtMixedBurned.SelectionAlignment = HorizontalAlignment.Right;
            rtBurned.SelectionAlignment = HorizontalAlignment.Right;
            rtMud.SelectionAlignment = HorizontalAlignment.Right;
        }


        /*+============================================== SERIAL / DATABASE / UDP CONNECTION ===============================================+*/

        //Serial Connection (Transfer Com port to textfile)
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
                log.LogEvent(t);
            }
            catch (IOException ex)
            {
                string t = DateTime.Now.ToString() + " : " + ex.Message.ToString();
                LogOutput(t);
                log.LogEvent(t);
            }
        }

        //Serial Data Received
        private void CaneSensorDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            serialPort = (SerialPort)sender;
            string incomingData = serialPort.ReadLine().TrimEnd('\r');

            Sensor sc = new Sensor(incomingData, pause, decrementing, bnlist);

            sc.GetSensorsActivity();

        }

        //Database (WeighBridge, App's own Database)
        private void CheckDatabaseConnection()
        {
            if (cc.ConnectionExist())
            {

                while (!cc.DbConnectionEstablished())
                {
                    cc.CheckConnectionDatabase();
                }

                if (cc.DbConnectionEstablished())
                {
                    InitializeSerialConnections();
                    CheckUdpConnection();

                    logTextOutput = DateTime.Now + " : Successfully connected to Application's System Database";
                    LogOutput(logTextOutput);
                    log.LogEvent(logTextOutput);

                    SaveStateStart();
                    CheckConfigStart();
                }
            }
            else
            {
                MessageBox.Show("Please check manual for correct database connection string", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void CheckWBConnection()
        {

            if (cc.WeighBridgeConnectionExist())
            {
                logTextOutput = DateTime.Now + " : Successfully connected to WeighBridge Database";
                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);
            }
            else
            {
                logTextOutput = DateTime.Now + " : Connection to WeighBridge Database unsuccessful";
                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);

                rtTrashBatchNum.Enabled = false;
            }
        }

        //NIR UDP Connection
        private void CheckUdpConnection()
        {
            if (nirUdp.UdpConnected())
            {
                logTextOutput = DateTime.Now + " : UDP Connection Established";
                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);
            }
            else
            {
                logTextOutput = DateTime.Now + " : UDP Connection unsuccesful";
                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);
            }
        }



        /*+======================================== SERIES NO. INCREMENT===============================================+*/

        /*
         * Increment Series No. on NIR Scan
         */
        private void IncrementSeriesNo()
        {
            seriesNo = cnf.SampleCount;
            rtSeriesNo.Text = (seriesNo += 1).ToString().PadLeft(3, pad);

            cnf.ScannedSample(seriesNo.ToString());
        }


        /*+================================== APP STATE SAVING =====================================+*/

        private void SaveStateStart()
        {
            savingStateTimer = new Timer();
            savingStateTimer.Interval = 10000;
            savingStateTimer.Enabled = true;
            savingStateTimer.Tick += new EventHandler(SaveState_Tick);
        }

        private void SaveState_Tick(object sender, EventArgs e)
        {
            log.SavingState(bnlist, seriesNo.ToString().PadLeft(3, pad));

            if (savedCount == 5)
            {
                log.TruncateSavedStateLogs();
                log.SavingState(bnlist, seriesNo.ToString().PadLeft(3, pad));

                savedCount = 0;
            }

            if (log.SavedState())
            {
                logTextOutput = DateTime.Now.ToString() + " : State Saved";
                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);
                savedCount++;
            }
            else
            {
                logTextOutput = DateTime.Now.ToString() + " : Error saving";
                LogOutput(logTextOutput);
                log.LogEvent(logTextOutput);
            }
        }



        /*+===================================== CHECKS ON CONFIGURATION CHANGES ========================================+*/

        /*
         * To check changes in the configuration text files
         */

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
            appLoading.LoadSavedValues(bnlist);
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
            rtDumpBn7.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBn8.SelectionAlignment = HorizontalAlignment.Center;

            rtDumpBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx4.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx5.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx6.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx7.SelectionAlignment = HorizontalAlignment.Center;
            rtDumpBx8.SelectionAlignment = HorizontalAlignment.Center;

            rtStockBn1.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn2.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn3.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn4.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn5.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn6.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn7.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBn8.SelectionAlignment = HorizontalAlignment.Center;

            rtStockBx1.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx2.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx3.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx4.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx5.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx6.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx7.SelectionAlignment = HorizontalAlignment.Center;
            rtStockBx8.SelectionAlignment = HorizontalAlignment.Center;

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

        //Add textboxes for tracking on program start
        private void RegisterTextboxes()
        {
            //Tipper One
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn1, rtTipOneBx1, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn2, rtTipOneBx2, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn3, rtTipOneBx3, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn4, rtTipOneBx4, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn5, rtTipOneBx5, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn6, rtTipOneBx6, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn7, rtTipOneBx7, "TipOne"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipOneBn8, rtTipOneBx8, "TipOne"));

            //Tipper Two
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn1, rtTipTwoBx1, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn2, rtTipTwoBx2, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn3, rtTipTwoBx3, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn4, rtTipTwoBx4, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn5, rtTipTwoBx5, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn6, rtTipTwoBx6, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn7, rtTipTwoBx7, "TipTwo"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtTipTwoBn8, rtTipTwoBx8, "TipTwo"));

            //Dump Truck
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn1, rtDumpBx1, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn2, rtDumpBx2, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn3, rtDumpBx3, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn4, rtDumpBx4, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn5, rtDumpBx5, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn6, rtDumpBx6, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn7, rtDumpBx7, "DumpTruck"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtDumpBn8, rtDumpBx8, "DumpTruck"));

            //Stock Pile
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn1, rtStockBx1, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn2, rtStockBx2, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn3, rtStockBx3, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn4, rtStockBx4, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn5, rtStockBx5, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn6, rtStockBx6, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn7, rtStockBx7, "StockPile"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtStockBn8, rtStockBx8, "StockPile"));

            //Main Cane
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn1, rtMainBx1, "MainCane"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn2, rtMainBx2, "MainCane"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn3, rtMainBx3, "MainCane"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtMainBn4, rtMainBx4, "MainCane"));

            //Cane Knives
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtKnivesBn1, rtKnivesBx1, "CaneKnives"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtKnivesBn2, rtKnivesBx2, "CaneKnives"));

            //Shredded Cane
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtShredBn1, rtShredBx1, "Shredder"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtShredBn2, rtShredBx2, "Shredder"));

            //Foss NIR
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtNirWashing, rtWashingCount, "NirWashing"));
            bnlist.lTbox.Add(new Tuple<RichTextBox, RichTextBox, string>(rtNirScanning, rtNirCount, "NirScanning"));

            //Sensor Indicators
            bnlist.sensorIndicators.Add(new Tuple<RichTextBox, string>(rtSideCaneCheck, "Side Cane"));
            bnlist.sensorIndicators.Add(new Tuple<RichTextBox, string>(rtMainCaneCheck, "Main Cane"));
            bnlist.sensorIndicators.Add(new Tuple<RichTextBox, string>(rtCaneKnivesCheck, "Cane Knives"));
            bnlist.sensorIndicators.Add(new Tuple<RichTextBox, string>(rtShredderCheck, "Shredder"));

        }

        //Series default value
        private void DefaultValues()
        {
            rtSeriesNo.Text = cnf.SampleCount.ToString().PadLeft(3, pad);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected || !restarting)
            {
                DialogResult dialogResult = MessageBox.Show("Application is closing. Continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    log.LogEvent(DateTime.Now + " : " + "Application was closed");
                    appLoading.ResetLoadStatus();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Checking database connection, Click ok to continue", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //RichTextbox UI positions and values
            TextAlignment();
            DefaultValues();
            RegisterTextboxes();

            //Connections
            CheckDatabaseConnection();
            CheckWBConnection();
        }

    }
}
