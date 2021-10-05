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
        CountInterval ci = new CountInterval();

        private Timer washingTimer;
        private Timer nirTimer;

        public List<Timer> washingTimerList = new List<Timer>();
        public List<Timer> nirTimerList = new List<Timer>();

        private static int fossNirWashingTime;
        private static int fossNirTime;

        public void SetWashingTimer(ref int count, RichTextBox rtNirScanning, RichTextBox rtNirCount, RichTextBox rtBn, RichTextBox rtCnt)
        {
            int c = count;
            washingTimer = new Timer();
            washingTimer.Interval = 1000;
            washingTimer.Enabled = true;
            washingTimer.Tick += (object sender, EventArgs e) => WashingTimer_Tick(sender, e, ref c, rtNirScanning, rtNirCount, rtBn, rtCnt);
            washingTimerList.Add(washingTimer);
        }

        private void WashingTimer_Tick(object sender, EventArgs e, ref int count, RichTextBox rtNirScanning, RichTextBox rtNirCount, RichTextBox rtBn, RichTextBox rtCnt)
        {
            washingTimer = (Timer)sender;

            fossNirWashingTime = ci.WashingTime;

            ctcc.ChangeText(rtCnt, (count += 1).ToString());

            if (count > fossNirWashingTime)
            {
                ctcc.ChangeText(rtNirScanning, rtBn.Text);
                ctcc.ChangeText(rtNirCount, "0");
                ctcc.ChangeColorTextBox(rtNirCount, Color.Maroon);
                ctcc.ChangeForeColorTextBox(rtNirCount, Color.White);

                washingTimer.Stop();
                ctcc.ChangeText(rtBn, "");
                ctcc.ChangeText(rtCnt, "");
                ctcc.ChangeColorTextBox(rtCnt, Color.CornflowerBlue);
            }
        }

        public void SetNirTimer(ref int count, RichTextBox rtBn, RichTextBox rtCnt)
        {
            int c = count;
            nirTimer = new Timer();
            nirTimer.Interval = 1000;
            nirTimer.Enabled = true;
            nirTimer.Tick += (object sender, EventArgs e) => NirTimer_Tick(sender, e, ref c, rtBn, rtCnt);
            nirTimerList.Add(nirTimer);
        }

        private void NirTimer_Tick(object sender, EventArgs e, ref int count, RichTextBox rtBn, RichTextBox rtCnt)
        {
            nirTimer = (Timer)sender;

            fossNirTime = ci.NirTime;

            ctcc.ChangeText(rtCnt, (count += 1).ToString());

            if (count > fossNirTime)
            {
                nirTimer.Stop();
                ctcc.ChangeText(rtBn, "");
                ctcc.ChangeText(rtCnt, "");
                ctcc.ChangeColorTextBox(rtCnt, Color.CornflowerBlue);
            }
        }

        public void PauseNirCount(bool pause)
        {
            int i;
            int y;

            if (washingTimerList.Count > 0 || nirTimerList.Count > 0)
            {
                if (pause)
                {
                    for (i = 0; i < washingTimerList.Count; i++)
                    {
                        washingTimerList[i].Stop();
                    }

                    for (y = 0; y < nirTimerList.Count; y++)
                    {
                        nirTimerList[y].Stop();
                    }
                }
                else
                {
                    for (i = 0; i < washingTimerList.Count; i++)
                    {
                        washingTimerList[i].Start();
                    }

                    for (y = 0; y < nirTimerList.Count; y++)
                    {
                        nirTimerList[y].Start();
                    }
                }
            }

        }
    }
}
