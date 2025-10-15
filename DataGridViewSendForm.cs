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

        /// <summary>
        /// This is only a stored local reference
        /// </summary>
        List<SendDataJsonItems> sendGroups;

        int currentSelectedTabIndex = 0;

        DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SendDataHandler"></param>
        public DataGridViewSendForm(Action<string> SendDataHandler)
        {
            InitializeComponent();

            SendData = SendDataHandler;

            btnColumn.HeaderText = "Action";
            btnColumn.Text = "Send";
            btnColumn.UseColumnTextForButtonValue = true;
            dgv.Columns.Add(btnColumn);

            dgv.DataSourceChanged += dgv_DataSourceChanged;
            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendGroups"></param>
        public void SetData(List<SendDataJsonItems> sendGroups)
        {
            this.sendGroups = sendGroups;
            if (sendGroups.Count == 0)
            {
                sendGroups.Add(new SendDataJsonItems("New Group", "Rename this group to something meaningful"));
            }
            tabCtrl.TabPages.Clear();
            for (int i = 0;i<sendGroups.Count; i++)
            {
                tabCtrl.TabPages.Add(TabPageExt.Create(sendGroups[i].Name, sendGroups[i].Note));
            }
            if (sendGroups.Count != 0)
            {
                dgv.DataSource = sendGroups[0].items;
                btnColumn.DisplayIndex = 1;
            }
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx < 0 || idx >= sendGroups.Count) return;

            currentSelectedTabIndex = idx;

            dgv.CellClick -= dgv_CellClick;
            dgv.CellEnter -= dgv_CellEnter;

            dgv.DataSource = null;
            dgv.DataSource = sendGroups[idx].items;
            btnColumn.DisplayIndex = 1;

            dgv.CellClick += dgv_CellClick;
            dgv.CellEnter += dgv_CellEnter;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sendGroups[currentSelectedTabIndex].items.Count == 0) return;
            if (e.ColumnIndex != 0) return;
            if (e.RowIndex == -1) return;

            string toSend = sendGroups[currentSelectedTabIndex].items[e.RowIndex].Data;
            SendData(toSend);
        }
        
        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            TextBox tb = (TextBox)e.Control;
            if (tb.IsHandleCreated)
                return;
            tb.MouseUp += tb_MouseUp;
            tb.MouseDown += tb_MouseDown;
            tb.MouseMove += tb_MouseMove;
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

        private void addNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendGroups[currentSelectedTabIndex].items.Add(new SendDataItem());
        }

        private void insertNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv.CurrentCell?.RowIndex ?? 0;
            sendGroups[currentSelectedTabIndex].items.Insert(index, new SendDataItem());
            dgv.CurrentCell = dgv.Rows[index].Cells[1];
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
            sendGroups[currentSelectedTabIndex].items.RemoveAt(index);
        }

        private void tabsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
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
            int idx = tabCtrl.SelectedIndex;
            tsmiMoveTabToLeft.Enabled = (idx > 0);
            tsmiMoveTabToRight.Enabled = (idx >= 0 && idx < tabCtrl.TabPages.Count - 1);
        }

        private void editTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", tabCtrl.SelectedTab.Text },
                { "Note", tabCtrl.SelectedTab.ToolTipText }
            };
            var result = MultiInputDialog.Show("Edit Tab Name", projectMeta);
            if (result != null)
            {
                string Name = result["Name"];
                string Note = result["Note"];
                tabCtrl.SelectedTab.Text = Name;
                tabCtrl.SelectedTab.ToolTipText = Note;
                sendGroups[currentSelectedTabIndex].Name = Name;
                sendGroups[currentSelectedTabIndex].Note = Note;
            }
        }

        private void addTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var projectMeta = new Dictionary<string, string>()
            {
                { "Name", tabCtrl.SelectedTab.Text },
                { "Note", tabCtrl.SelectedTab.ToolTipText }
            };
            var result = MultiInputDialog.Show("Add New Tab", projectMeta);
            if (result != null)
            {
                string Name = result["Name"];
                string Note = result["Note"];
                tabCtrl.TabPages.Add(TabPageExt.Create(Name, Note));
                sendGroups.Add(new SendDataJsonItems(Name, Note));
            }
        }

        private void toLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx > 0)
            {
                tabCtrl.SelectedIndexChanged -= tabCtrl_SelectedIndexChanged;
                var tp = tabCtrl.TabPages[idx];
                tabCtrl.TabPages.RemoveAt(idx);
                tabCtrl.TabPages.Insert(idx - 1, tp);
                tabCtrl.SelectedIndex = idx - 1; // keep it selected
                var sendGroupsItem = sendGroups[idx];
                sendGroups.RemoveAt(idx);
                sendGroups.Insert(idx - 1, sendGroupsItem);
                tabCtrl.SelectedIndexChanged += tabCtrl_SelectedIndexChanged;
            }
        }

        private void tsmiMoveTabToRight_Click(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            if (idx >= 0 && idx < tabCtrl.TabPages.Count - 1)
            {
                tabCtrl.SelectedIndexChanged -= tabCtrl_SelectedIndexChanged;
                var tp = tabCtrl.TabPages[idx];
                tabCtrl.TabPages.RemoveAt(idx);
                tabCtrl.TabPages.Insert(idx + 1, tp);
                tabCtrl.SelectedIndex = idx + 1; // keep it selected
                var sendGroupsItem = sendGroups[idx];
                sendGroups.RemoveAt(idx);
                sendGroups.Insert(idx + 1, sendGroupsItem);
                tabCtrl.SelectedIndexChanged += tabCtrl_SelectedIndexChanged;
            }
        }

        private void confirmToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int idx = tabCtrl.SelectedIndex;
            tabCtrl.TabPages.RemoveAt(idx);
            sendGroups.RemoveAt(idx);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class TabPageExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="tooltip"></param>
        /// <returns></returns>
        public static TabPage Create(string title, string tooltip = null)
        {
            var tp = new TabPage(title);
            if (tooltip != null)
                tp.ToolTipText = tooltip;
            return tp;
        }
    }
}
