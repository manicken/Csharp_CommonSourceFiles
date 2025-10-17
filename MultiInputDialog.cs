using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Microsan
{
    /// <summary>
    /// 
    /// </summary>
    public static class MultiInputDialog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static Dictionary<string, string> Show(string title, Dictionary<string, string> fields)
        {
            Form form = new Form()
            {
                Text = title,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                ClientSize = new Size(340, 70 + 60 * fields.Count),
                MaximizeBox = false,
                MinimizeBox = false
            };

            var textboxes = new Dictionary<string, TextBox>();
            int top = 10;

            foreach (var kv in fields)
            {
                Label lbl = new Label()
                {
                    AutoSize = true,
                    Text = kv.Key + ":",
                    Left = 10,
                    Top = top
                };

                TextBox txt = new TextBox()
                {
                    Left = 10,
                    Width = form.Width - 20,
                    Text = kv.Value ?? ""
                };
                

                form.Controls.Add(lbl);
                form.Controls.Add(txt);
                textboxes[kv.Key] = txt;

                txt.Top = lbl.Bottom;
                top = txt.Bottom + 10;
            }

            Button btnCancel = new Button()
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Left = form.Width - 90,
                Width = 80,
                Top = top + 10
            };

            Button btnOk = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Left = btnCancel.Left - 90,
                Width = 80,
                Top = top + 10
            };

            form.Height = btnCancel.Bottom + 40;

            form.Controls.AddRange(new Control[] { btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            if (form.ShowDialog() == DialogResult.OK)
            {
                var result = new Dictionary<string, string>();
                foreach (var kv in textboxes)
                    result[kv.Key] = kv.Value.Text;
                return result;
            }

            return null;
        }
        private static void ExampleUse()
        {
            string name = "";
            string note = "";
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", name },
                { "Note", note }
            };
            var result = MultiInputDialog.Show("Edit Tab Name", projectMeta);
            if (result != null)
            {
                name = result["Name"];
                note = result["Note"];
            }
        }
    }

    


}
