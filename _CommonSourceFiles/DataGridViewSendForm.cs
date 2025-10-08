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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualBasic;

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
        /// 
        public Action<string> SendData;

        public List<SendDataJsonFile> openJsonFiles = new List<SendDataJsonFile>();

        int currentSelectedTabIndex = 0;

        DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SendDataHandler"></param>
        public DataGridViewSendForm(Action<string> SendDataHandler)
        {
            SendData = SendDataHandler;
            
            InitializeComponent();

            btnColumn.HeaderText = "Action";
            btnColumn.Text = "Send";
            btnColumn.UseColumnTextForButtonValue = true;
            dgv.Columns.Add(btnColumn);

            dgv.DataSourceChanged += dgv_DataSourceChanged;
            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;

            tabCtrl.TabPages.Clear();

            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeNameWithoutExt = Path.GetFileNameWithoutExtension(exePath);

            string[] filePaths = Directory.GetFiles(Application.StartupPath + "\\" + exeNameWithoutExt, "*.json");

            foreach (string filePath in filePaths)
            {
                tabCtrl.TabPages.Add(Path.GetFileName(filePath));
                openJsonFiles.Add(new SendDataJsonFile(filePath));
            }
            if (openJsonFiles.Count != 0)
            {
                dgv.DataSource = openJsonFiles[0].data;
                btnColumn.DisplayIndex = 1;
            }
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx < 0 || idx >= openJsonFiles.Count) return;

            currentSelectedTabIndex = idx;

            dgv.CellClick -= dgv_CellClick;
            dgv.CellEnter -= dgv_CellEnter;

            dgv.DataSource = null;
            dgv.DataSource = openJsonFiles[idx].data;
            btnColumn.DisplayIndex = 1;

            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;
        }



        
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (openJsonFiles[currentSelectedTabIndex].data.Count == 0) return;
            if (e.ColumnIndex != 0) return;
            if (e.RowIndex == -1) return;

            string toSend = openJsonFiles[currentSelectedTabIndex].data[e.RowIndex].Data;
            SendData(toSend);
        }
        /// <summary>
        /// 
        /// </summary>
        public void SaveAllXml()
        {
            for (int i = 0; i < openJsonFiles.Count; i++)
            {
                openJsonFiles[i].Save();
            }
        }
        
        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllXml();
            
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
            
        }
        
        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgv.DataSource == null) return;
            if (dgv.Columns.Count != 3) return;
            //MessageBox.Show("dgv.Columns.Count" + dgv.Columns.Count);

            dgv.Columns[1].MinimumWidth = 128;
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[1].DefaultCellStyle.Font = Fonts.CourierNew;
            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgv.Columns[0].Width = 48;
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
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

        private void tsbtnNewFile_Click(object sender, EventArgs e)
        {
            string filePath = "";
            if (!QuickDialogs.FileSave(Application.StartupPath + "\\" + RuntimeProgramming.SOURCE_FILES_DIR_NAME, "Select the filename..", "JSON files|*.json", out filePath))
                return;

            tabCtrl.TabPages.Add(Path.GetFileName(filePath));
            openJsonFiles.Add(new SendDataJsonFile(filePath));
        }

        private void tsSaveAll_Click(object sender, EventArgs e)
        {
            SaveAllXml();
        }

        private void tsBtnSave_Click(object sender, EventArgs e)
        {
            openJsonFiles[currentSelectedTabIndex].Save();
        }

        private void tsbtnAddNewRowItem_Click(object sender, EventArgs e)
        {
            openJsonFiles[currentSelectedTabIndex].data.Add(new SendDataItem());
           // dgv.Refresh();
        }

        private void addNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openJsonFiles[currentSelectedTabIndex].data.Add(new SendDataItem());
            //dgv.Refresh();
        }

        private void insertNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            openJsonFiles[currentSelectedTabIndex].data.Insert(index, new SendDataItem());
            dgv.CurrentCell = dgv.Rows[index].Cells[1];
            // dgv.Refresh();
        }

        private void dgv_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            // Ignore clicks outside of valid cells (like column headers)
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                dgv.CurrentCell = null;
                return;
            }

            dgv.CurrentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]; // select inserted row
            
        }

        private void confirmDeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            openJsonFiles[currentSelectedTabIndex].data.RemoveAt(index);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Point pos = tabCtrl.PointToClient(Cursor.Position);
            for (int i = 0; i < tabCtrl.TabCount; i++)
            {
                if (tabCtrl.GetTabRect(i).Contains(pos))
                {
                    tabCtrl.SelectedIndex = i;
                    break;
                }
            }
        }

        private void editTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = Interaction.InputBox(
                "Enter new tab name:",
                "Edit tab Name",
                tabCtrl.SelectedTab.Text
            );

            if (!string.IsNullOrWhiteSpace(name))
            {
                tabCtrl.SelectedTab.Text = name;
            }
        }

        private void tabCtrl_MouseDown(object sender, MouseEventArgs e)
        {
            /*if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabCtrl.TabCount; i++)
                {
                    Rectangle r = tabCtrl.GetTabRect(i);
                    if (r.Contains(e.Location))
                    {
                        tabCtrl.SelectedIndex = i; // select the tab under the mouse
                        break;
                    }
                }
            }*/
        }
    }

    
}
