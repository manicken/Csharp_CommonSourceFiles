/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-15
 * Time: 09:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Microsan
{
    partial class SerialPortSettingForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpBoxMessageStartStop;
        public System.Windows.Forms.TextBox txtMessageStopId;
        public System.Windows.Forms.TextBox txtMessageStartId;
        private System.Windows.Forms.Button btnDisconnect;
        public System.Windows.Forms.TextBox txtHostPort;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtHostIP;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox grpBoxIpPort;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBoxMessageStartStop = new System.Windows.Forms.GroupBox();
            this.txtMessageStopId = new System.Windows.Forms.TextBox();
            this.txtMessageStartId = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtHostPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtHostIP = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpBoxIpPort = new System.Windows.Forms.GroupBox();
            this.grpBoxMessageStartStop.SuspendLayout();
            this.grpBoxIpPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxMessageStartStop
            // 
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStopId);
            this.grpBoxMessageStartStop.Controls.Add(this.txtMessageStartId);
            this.grpBoxMessageStartStop.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxMessageStartStop.Location = new System.Drawing.Point(1, 60);
            this.grpBoxMessageStartStop.Name = "grpBoxMessageStartStop";
            this.grpBoxMessageStartStop.Size = new System.Drawing.Size(289, 65);
            this.grpBoxMessageStartStop.TabIndex = 6;
            this.grpBoxMessageStartStop.TabStop = false;
            this.grpBoxMessageStartStop.Text = "Message Start/Stop IDs";
            // 
            // txtMessageStopId
            // 
            this.txtMessageStopId.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageStopId.Location = new System.Drawing.Point(149, 22);
            this.txtMessageStopId.MaxLength = 8;
            this.txtMessageStopId.Name = "txtMessageStopId";
            this.txtMessageStopId.Size = new System.Drawing.Size(130, 31);
            this.txtMessageStopId.TabIndex = 4;
            this.txtMessageStopId.Text = "12345678";
            this.txtMessageStopId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMessageStopId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMessageStartStopIds_MouseClick);
            this.txtMessageStopId.Leave += new System.EventHandler(this.txtMessageStartStopIds_Leave);
            this.txtMessageStopId.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_MouseDown);
            this.txtMessageStopId.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tb_MouseMove);
            this.txtMessageStopId.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tb_MouseUp);
            // 
            // txtMessageStartId
            // 
            this.txtMessageStartId.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageStartId.Location = new System.Drawing.Point(9, 22);
            this.txtMessageStartId.MaxLength = 8;
            this.txtMessageStartId.Name = "txtMessageStartId";
            this.txtMessageStartId.Size = new System.Drawing.Size(130, 31);
            this.txtMessageStartId.TabIndex = 4;
            this.txtMessageStartId.Text = "12345678";
            this.txtMessageStartId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMessageStartId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMessageStartStopIds_MouseClick);
            this.txtMessageStartId.Leave += new System.EventHandler(this.txtMessageStartStopIds_Leave);
            this.txtMessageStartId.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_MouseDown);
            this.txtMessageStartId.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tb_MouseMove);
            this.txtMessageStartId.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tb_MouseUp);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(150, 131);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(131, 34);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtHostPort
            // 
            this.txtHostPort.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostPort.Location = new System.Drawing.Point(212, 22);
            this.txtHostPort.Name = "txtHostPort";
            this.txtHostPort.Size = new System.Drawing.Size(69, 29);
            this.txtHostPort.TabIndex = 4;
            this.txtHostPort.Text = "00127";
            this.txtHostPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHostPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_MouseDown);
            this.txtHostPort.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tb_MouseMove);
            this.txtHostPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tb_MouseUp);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(198, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = ":";
            // 
            // txtHostIP
            // 
            this.txtHostIP.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHostIP.Location = new System.Drawing.Point(6, 22);
            this.txtHostIP.Name = "txtHostIP";
            this.txtHostIP.Size = new System.Drawing.Size(198, 29);
            this.txtHostIP.TabIndex = 2;
            this.txtHostIP.Text = "192.168.001.004";
            this.txtHostIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHostIP.TextChanged += new System.EventHandler(this.txtHostIP_TextChanged);
            this.txtHostIP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tb_MouseDown);
            this.txtHostIP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tb_MouseMove);
            this.txtHostIP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tb_MouseUp);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(10, 131);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(131, 34);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // grpBoxIpPort
            // 
            this.grpBoxIpPort.Controls.Add(this.txtHostIP);
            this.grpBoxIpPort.Controls.Add(this.txtHostPort);
            this.grpBoxIpPort.Controls.Add(this.label2);
            this.grpBoxIpPort.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxIpPort.Location = new System.Drawing.Point(1, 0);
            this.grpBoxIpPort.Name = "grpBoxIpPort";
            this.grpBoxIpPort.Size = new System.Drawing.Size(289, 59);
            this.grpBoxIpPort.TabIndex = 7;
            this.grpBoxIpPort.TabStop = false;
            this.grpBoxIpPort.Text = "Host/Ip && Port";
            // 
            // TCPClientSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 169);
            this.ControlBox = false;
            this.Controls.Add(this.grpBoxIpPort);
            this.Controls.Add(this.grpBoxMessageStartStop);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TCPClientSettingForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Connection Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCPClientSettingForm_FormClosing);
            this.grpBoxMessageStartStop.ResumeLayout(false);
            this.grpBoxMessageStartStop.PerformLayout();
            this.grpBoxIpPort.ResumeLayout(false);
            this.grpBoxIpPort.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
