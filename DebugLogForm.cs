using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Microsan
{
   
    public partial class DebugLogForm : Form
    {
        [Flags]
        public enum FormStyleFlags
        {
            None = 0,
            NoClose = 0x200,
            DropShadow = 0x20000
        }

        private int createParams = 0;
        public DebugLogForm()
        {
            InitializeComponent();
        }
        public DebugLogForm(FormStyleFlags createParams)
        {
            InitializeComponent();
            this.createParams = (int)createParams;
        }

        // 🧱 This disables the Close (X) button
        protected override CreateParams CreateParams
        {
            get
            {
                //const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= createParams;
                return cp;
            }
        }

        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxt.Clear();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxt.Copy();
        }

        private void selectAllAndCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxt.SelectAll();
            rtxt.Copy();
        }

        public static void ShowOnceWithMessage(string msg)
        {
            DebugLogForm debugLogForm = new DebugLogForm();
            
            debugLogForm.rtxt.AppendText(Environment.NewLine + msg + Environment.NewLine);
            debugLogForm.ShowDialog();
        }
        public static void ShowOnceWithMessageAndAckButton(string msg, string ackButtonText)
        {
            using (DebugLogForm debugLogForm = new DebugLogForm(FormStyleFlags.NoClose))
            {
                // Adjust text
                debugLogForm.rtxt.AppendText(Environment.NewLine + msg + Environment.NewLine);
                debugLogForm.rtxt.ReadOnly = true;
                debugLogForm.rtxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

                // Create button
                Button btnAck = new Button();
                btnAck.Text = ackButtonText;
                btnAck.AutoSize = true;
                btnAck.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                // Add to form
                debugLogForm.Controls.Add(btnAck);

                // Layout properly (use ClientSize to avoid title bar offsets)
                const int margin = 10;
                btnAck.Top = debugLogForm.ClientSize.Height - btnAck.Height - margin;
                btnAck.Left = debugLogForm.ClientSize.Width - btnAck.Width - margin;

                // Adjust RichTextBox height if needed
                debugLogForm.rtxt.Height = btnAck.Top - margin;

                // Close dialog when button clicked
                btnAck.Click += (s, e) => debugLogForm.Close();

                // Make sure resizing keeps layout correct
                debugLogForm.Resize += (s, e) =>
                {
                    btnAck.Top = debugLogForm.ClientSize.Height - btnAck.Height - margin;
                    btnAck.Left = debugLogForm.ClientSize.Width - btnAck.Width - margin;
                    debugLogForm.rtxt.Height = btnAck.Top - margin;
                };
                // Show modal dialog
                debugLogForm.ShowDialog();
            }
        }

    }

    public static class Debug
    {
        public static DebugLogForm logForm;
        public static void AddLine(string text)
        {
            if (logForm == null)
            {
                logForm = new DebugLogForm();
                logForm.Show();
            }
            logForm.rtxt.AppendText(text + "\n");
        }
    }
}
