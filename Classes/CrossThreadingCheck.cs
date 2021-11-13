using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class CrossThreadingCheck
    {

        private delegate void SetColorCallBack(RichTextBox rt, Color color);
        private delegate void SetTextCallBack(RichTextBox rt, string text);
        private delegate void SetTextButton(Button btn, string text);
        private delegate void GetTextCallBack(RichTextBox rt);
        private delegate void DataGridViewCallBack(DataGridView dgv, DataTable dt);
        private delegate void SetControlBehavior(RichTextBox rt);

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

        public string GetControlValue(Control control)
        {
            if (control.InvokeRequired)
            {
                return (string)control.Invoke(
                    new Func<String>(() => GetControlValue(control)));
            }
            else
            {
                string text = control.Text;
                return text;
            }
        }

        public string DataGridValues(DataGridView dgv, int column, string cell)
        {
            if (dgv.InvokeRequired)
            {
                return (string)dgv.Invoke(
                   new Func<String>(() => DataGridValues(dgv, column, cell)));
            }
            else
            {
                string value = dgv.Rows[dgv.SelectedCells[column].RowIndex].Cells[cell].Value.ToString();
                return value;
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

        public void SetRichTextboxFocus(RichTextBox rt)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetControlBehavior(SetRichTextboxFocus);
                rt.Invoke(d, new object[] { rt });
            }
            else
            {
                rt.Focus();
            }
        }

        public void SelectAllTextbox(RichTextBox rt)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetControlBehavior(SelectAllTextbox);
                rt.Invoke(d, new object[] { rt });
            }
            else
            {
                rt.SelectAll();
            }
        }

        public void RichTextboxEnable(RichTextBox rt, bool enable)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetControlBehavior(SelectAllTextbox);
                rt.Invoke(d, new object[] { rt });
            }
            else
            {
                rt.Enabled = enable;
            }
        }

        public void SelectionAlignmentRichTextbox(RichTextBox rt)
        {
            if (rt.InvokeRequired)
            {
                var d = new SetControlBehavior(SelectionAlignmentRichTextbox);
                rt.Invoke(d, new object[] { rt });
            }
            else
            {
                rt.SelectionAlignment = HorizontalAlignment.Right;
            }
        }

        public void DataGrid(DataGridView dgv, DataTable dt)
        {
            if (dgv.InvokeRequired)
            {
                var d = new DataGridViewCallBack(DataGrid);
                dgv.Invoke(d, new object[] { dgv, dt });
            }
            else
            {
                dgv.DataSource = dt;
                if (dt != null)
                {
                    dgv.Columns[0].Visible = false;
                }
            }
        }

        public string DateTimePickerVal(DateTimePicker dtp)
        {
            if (dtp.InvokeRequired)
            {
                return (string)dtp.Invoke(
                    new Func<String>(() => DateTimePickerVal(dtp)));
            }
            else
            {
                string text = dtp.Value.ToString("yyyy/MM/dd");
                return text;
            }
        }
    }
}
