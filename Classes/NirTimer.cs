using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class NirTimer
    {
        CrossThreadingCheck ctcc = new CrossThreadingCheck();
        ConfigValues ci = new ConfigValues();

        private Timer washingTimer;
        private Timer nirTimer;
        private Timer forceScanTimer;

        public List<Timer> washingTimerList = new List<Timer>();
        public List<Timer> nirTimerList = new List<Timer>();

        private static int fossNirWashingTime;
        private static int fossNirTime;


        public void SetWashingTimer(RichTextBox rtNirScanning, RichTextBox rtNirCount, RichTextBox rtBn, RichTextBox rtCnt)
        {
            washingTimer = new Timer();
            washingTimer.Interval = 1000;
            washingTimer.Enabled = true;
            washingTimer.Tick += (object sender, EventArgs e) => WashingTimer_Tick(sender, e, rtNirScanning, rtNirCount, rtBn, rtCnt);
            washingTimerList.Add(washingTimer);
        }

        private void WashingTimer_Tick(object sender, EventArgs e, RichTextBox rtNirScanning, RichTextBox rtNirCount, RichTextBox rtBn, RichTextBox rtCnt)
        {
            washingTimer = (Timer)sender;

            fossNirWashingTime = ci.WashingTime;

            int count = int.Parse(rtCnt.Text);

            ctcc.ChangeText(rtCnt, (count += 1).ToString());

            if (count > fossNirWashingTime)
            {
                washingTimer.Stop();

                ctcc.ChangeText(rtNirScanning, rtBn.Text);
                ctcc.ChangeText(rtNirCount, "0");
                ctcc.ChangeColorTextBox(rtNirCount, Color.Maroon);
                ctcc.ChangeForeColorTextBox(rtNirCount, Color.White);

                ctcc.ChangeText(rtBn, "");
                ctcc.ChangeText(rtCnt, "");
                ctcc.ChangeColorTextBox(rtCnt, Color.CornflowerBlue);

                washingTimerList.Clear();
            }
        }

        public void SetNirTimer(RichTextBox rtBn, RichTextBox rtCnt)
        {
            nirTimer = new Timer();
            nirTimer.Interval = 1000;
            nirTimer.Enabled = true;
            nirTimer.Tick += (object sender, EventArgs e) => NirTimer_Tick(sender, e, rtBn, rtCnt);
            nirTimerList.Add(nirTimer);
        }

        private void NirTimer_Tick(object sender, EventArgs e, RichTextBox rtBn, RichTextBox rtCnt)
        {
            nirTimer = (Timer)sender;

            fossNirTime = ci.NirTime;

            int count = int.Parse(rtCnt.Text);

            ctcc.ChangeText(rtCnt, (count += 1).ToString());

            if (count > fossNirTime)
            {
                nirTimer.Stop();
                ctcc.ChangeText(rtBn, "");
                ctcc.ChangeText(rtCnt, "");
                ctcc.ChangeColorTextBox(rtCnt, Color.CornflowerBlue);

                nirTimerList.Clear();
            }
        }

        public void SetForceScanTimer(RichTextBox rtBn, Button btn)
        {
            forceScanTimer = new Timer();
            forceScanTimer.Interval = 1000;
            forceScanTimer.Enabled = true;
            forceScanTimer.Tick += (object sender, EventArgs e) => ForceScanTimer_Tick(sender, e, rtBn, btn);
        }

        private void ForceScanTimer_Tick(object sender, EventArgs e, RichTextBox rtBn, Button btn)
        {
            forceScanTimer = (Timer)sender;

            fossNirTime = ci.ForceScanTime;

            int count = int.Parse(btn.Text);

            ctcc.ChangeButtonText(btn, (count += 1).ToString());

            if (count > fossNirTime)
            {
                forceScanTimer.Stop();
                ctcc.ChangeText(rtBn, "");
                ctcc.ChangeButtonText(btn, "Force Scan");
            }
        }

        private void PauseWashingCount(bool pause)
        {
            if (washingTimerList.Count > 0)
            {
                for (int i = 0; i < washingTimerList.Count; i++)
                {
                    if (pause)
                    {
                        washingTimerList[i].Stop();
                    }
                    else
                    {
                        washingTimerList[i].Start();
                    }
                }
            }
        }

        private void PauseNirCount(bool pause)
        {
            if (nirTimerList.Count > 0)
            {
                for (int i = 0; i < nirTimerList.Count; i++)
                {
                    if (pause)
                    {
                        nirTimerList[i].Stop();
                    }
                    else
                    {
                        nirTimerList[i].Start();
                    }
                }

            }
        }

        public void PauseNir(bool pause)
        {
            PauseNirCount(pause);
            PauseWashingCount(pause);
        }

    }
}
