using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class CrossThreadingCheck
    {

        private delegate void SetColorCallBack(RichTextBox rt, Color color);
        private delegate void SetTextCallBack(RichTextBox rt, string text);
        private delegate void SetTextButton(Button btn, string text);
        private delegate void GetTextCallBack(RichTextBox rt);

        public void ChangeColorTextBox(RichTextBox rt, Color color)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetColorCallBack(ChangeColorTextBox);
                rt.Invoke(d, new object[] { rt, color });
            }
            else
            {
                rt.BackColor = color;
            }
        }

        public void ChangeForeColorTextBox(RichTextBox rt, Color color)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetColorCallBack(ChangeForeColorTextBox);
                rt.Invoke(d, new object[] { rt, color });
            }
            else
            {
                rt.ForeColor = color;
            }
        }

        public void ChangeText(RichTextBox rt, string text)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetTextCallBack(ChangeText);
                rt.Invoke(d, new object[] { rt, text });
            }
            else
            {
                rt.Text = text;
            }
        }

        public string GetTextboxValue(Control control)
        {
            if (control.InvokeRequired)
            {
                return (string)control.Invoke(
                    new Func<String>(() => GetTextboxValue(control)));
            }
            else
            {
                string text = control.Text;
                return text;
            }
        }

        public void ChangeButtonText(Button btn, string text)
        {
            if (btn.InvokeRequired)
            {
                var d = new SetTextButton(ChangeButtonText);
                btn.Invoke(d, new object[] { btn, text });
            }
            else
            {
                btn.Text = text;
            }
        }
    }
}
