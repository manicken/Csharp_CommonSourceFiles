/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-05
 * Time: 23:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Crom.Controls.TabbedDocument;

namespace Microsan
{
    /// <summary>
    /// Description of DataGridViewSendForm.
    /// </summary>
    public partial class DataGridViewSendForm : Form
    {
        private void DebugMessage(string msg) { if (Microsan.Debugger.Message != null) Microsan.Debugger.Message(msg + "\n"); }
        
        /// <summary>
        /// 
        /// </summary>
        public Action<string> SendData;
        
        public DataSet ds;
        public DataTable dt;
        
        string SaveFileName = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SendDataHandler"></param>
        public DataGridViewSendForm(Action<string> SendDataHandler)
        {
            SendData = SendDataHandler;
            
            InitializeComponent();
            
            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;
            
            TabbedView tabView = new TabbedView();
            tabView.Font = Fonts.CourierNew;
            tabView.Location = new Point(0, 0);
            tabView.Size = new Size(this.Width-16, 20);
            tabView.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(tabView);
            
  
            tabView.Add(new TabPageView("hello1.xml"));
            tabView.Add(new TabPageView("hello2.xml"));
            tabView.Add(new TabPageView("hello3.xml"));

            //ds = new DataSet("MultiData");
            
            dt = new DataTable("SendDataTable");
            dt.Columns.Add("Data", typeof(string));
            dt.Columns.Add("click to\nSend", typeof(string));
            dt.Columns.Add("description", typeof(string));
                
            dt.TableNewRow += dt_TableNewRow;
            
            //ds.Tables.Add(dt);
            //ds.Tables.Add(new DataTable(
            
            if (File.Exists(SaveFileName))
            {
                dt.ReadXml(SaveFileName);
            }
            
            /*if (File.Exists(SaveFileName + "2.xml"))
            {
                ds.ReadXml(SaveFileName + "2.xml");
            }*/
   
            dgv.DataSource = dt;
        }

        

        
        private void dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            e.Row[1] = "send";
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 1) && (e.RowIndex != -1) && (dt.Rows.Count != 0))
            {
                SendData(dt.Rows[e.RowIndex][0].ToString());
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        public void SaveXml()
        {
            dt.WriteXml(SaveFileName, XmlWriteMode.WriteSchema, true);
            /*
            DataTable dt2 = new DataTable("SendDataTable2");
            dt2.Columns.Add("Data", typeof(string));
            dt2.Columns.Add("click to\nSend", typeof(string));
            dt2.Columns.Add("description", typeof(string));
            dt2.Rows.Add("hello", "send", "world");
            ds.Tables.Add(dt2);
            ds.WriteXml(SaveFileName + "2.xml", XmlWriteMode.WriteSchema);
            */
            /*
            StreamWriter sw = new StreamWriter(SaveFileName + "2.xml");
            //dt.DataSet.DataSetName = "MultiPassData";
            
            dt.WriteXml(sw, XmlWriteMode.WriteSchema, true);
            sw.WriteLine();
            
            dt.TableName = "HelloWorld";
            dt.WriteXml(sw, true);
            sw.Flush();
            sw.Close();
            */
        }
        
        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveXml();
            
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
            
        }
        
        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            
            dgv.Columns[0].MinimumWidth = 128;
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[0].DefaultCellStyle.Font = Fonts.CourierNew;
            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgv.Columns[1].Width = 48;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[1].ReadOnly = true;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgv.Columns[2].MinimumWidth = 128;
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
        }

        void dgv_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //DebugMessage("dgv_CellEnter" + MousePosition);
            dgv.BeginEdit(false);
            if (dgv.EditingControl != null)
            {
                TextBox tb = (TextBox)dgv.EditingControl;
                int currIndex = tb.GetCharIndexFromPosition(tb.PointToClient(MousePosition));
            
            tb.SelectionStart = currIndex;
            tb.SelectionLength = 0;
                
            }
        }

        void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           // DebugMessage("dgv_EditingControlShowing" + MousePosition);
            TextBox tb = (TextBox)e.Control;
            if (tb.IsHandleCreated)
                return;
            tb.MouseUp += tb_MouseUp;
            tb.MouseDown += tb_MouseDown;
            tb.MouseMove += tb_MouseMove;
        }

         private void tb_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Tag = tb.GetCharIndexFromPosition(e.Location);
        }

        private void tb_MouseMove(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.Button == MouseButtons.Left)
            {
                int downIndex = (int)tb.Tag;
                int currIndex = tb.GetCharIndexFromPosition(e.Location);
                if (downIndex > currIndex)
                {
                    tb.SelectionStart = currIndex;
                    tb.SelectionLength = downIndex - currIndex;
                }
                else
                {
                    tb.SelectionStart = downIndex;
                    tb.SelectionLength = currIndex - downIndex;
                }
            }
        }

        private void tb_MouseUp(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            int downIndex = (int)tb.Tag;
            int currIndex = tb.GetCharIndexFromPosition(e.Location);
            if (downIndex == currIndex)
            {
                tb.SelectionStart = currIndex;
                tb.SelectionLength = 0;
            }
            //DebugMessage("dgvCellEditTextBox @ GetCharIndexFromPosition:" + index);
        }
                
        
    }
}
