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
    }
}
