/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-15
 * Time: 09:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsan
{
    /// <summary>
    /// Description of TCPClientSettingForm.
    /// </summary>
    public partial class TCPClientSettingForm : Form
    {
        public Action<bool> Connect;
        
        public TCPClientSettingForm(Action<bool> ConnectCtrlHandler)
        {
            InitializeComponent();
            
            this.Connect = ConnectCtrlHandler;
            
            if (Connect == null)
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = false;
            }
        }
        
        public void Show(string ip, string port, string startId, string stopId)
        {
            txtHostIP.Text = ip;
            txtHostPort.Text = port;
            txtMessageStartId.Text = startId;
            txtMessageStopId.Text = stopId;
        }
        
        public void SetConnectedState(bool connected)
        {
            btnConnect.Enabled = !connected;
            btnDisconnect.Enabled = connected;
            
            grpBoxIpPort.Enabled = !connected;
            grpBoxMessageStartStop.Enabled = !connected;
        }
        
        private void txtMessageStartStopIds_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "12345678")
            {
                txt.Text = "";
                txt.ForeColor = Color.Black;
            }
            txt = null;
        }
        
        private void txtMessageStartStopIds_Leave(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "")
            {
                txt.Text = "12345678";
                txt.ForeColor = Color.Gray;
            }
            txt = null;
        }
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (Connect != null)
                Connect(true);
        }
        
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (Connect != null)
                Connect(false);
        }
        void TCPClientSettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
        }

        private void txtHostIP_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Fix for GetCharIndexFromPosition() not reaching end of text.
        /// </summary>
        private int GetSafeCharIndex(TextBox tb, Point pt)
        {
            int idx = tb.GetCharIndexFromPosition(pt);

            // If click is past the right edge of text, move to end
            int textWidth = TextRenderer.MeasureText(tb.Text, tb.Font).Width;
            if (pt.X > textWidth)
                idx = tb.TextLength;

            return idx;
        }

        private void tb_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Tag = GetSafeCharIndex(tb, e.Location);
        }

        private void tb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            TextBox tb = (TextBox)sender;
            if (tb.Tag is int == false) return;

            int downIndex = (int)tb.Tag;
            int currIndex = GetSafeCharIndex(tb, e.Location);
            tb.SelectionStart = Math.Min(downIndex, currIndex);
            tb.SelectionLength = Math.Abs(downIndex - currIndex);
        }

        private void tb_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Tag is int == false) return;
            int downIndex = (int)tb.Tag;
            int currIndex = GetSafeCharIndex(tb, e.Location);
            if (downIndex == currIndex)
            {
                tb.SelectionStart = currIndex;
                tb.SelectionLength = 0;
            }
        }
    }
}
