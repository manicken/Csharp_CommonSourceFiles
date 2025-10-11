/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-03-21
 * Time: 23:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project01
{
	/// <summary>
	/// Description of RF433_SFC_Form.
	/// </summary>
	public partial class RF433_SFC_Form : Form
	{
		public Action<string> SendMessage;
		
		public RF433_SFC_Form()
		{
			InitializeComponent();
		}
		
		private void Button1Click(object sender, EventArgs e)
		{
			if (SendMessage != null)
			{
				string btn = ((Button)sender).Tag.ToString();
				SendMessage("RF433_SFC:" + btn);
			}
		}
		
		private void RF433_SFC_FormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				this.Visible = false;
				
			}
		}
        
	}
}
