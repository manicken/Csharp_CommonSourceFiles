/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-05-05
 * Time: 23:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Microsan
{
    partial class DataGridViewSendForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.DataGridView dgv;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewSendForm));
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dgwContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNewFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAddNewRowItem = new System.Windows.Forms.ToolStripButton();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddNewTab = new System.Windows.Forms.Button();
            this.editTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.dgwContextMenu.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tabsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.ContextMenuStrip = this.dgwContextMenu;
            this.dgv.Location = new System.Drawing.Point(0, 67);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.Size = new System.Drawing.Size(434, 343);
            this.dgv.TabIndex = 0;
            this.dgv.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseDown);
            this.dgv.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellMouseEnter);
            this.dgv.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_EditingControlShowing);
            // 
            // dgwContextMenu
            // 
            this.dgwContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewRowToolStripMenuItem,
            this.insertNewRowToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deleteRowToolStripMenuItem});
            this.dgwContextMenu.Name = "dgwContextMenu";
            this.dgwContextMenu.Size = new System.Drawing.Size(152, 76);
            // 
            // addNewRowToolStripMenuItem
            // 
            this.addNewRowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addNewRowToolStripMenuItem.Image")));
            this.addNewRowToolStripMenuItem.Name = "addNewRowToolStripMenuItem";
            this.addNewRowToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addNewRowToolStripMenuItem.Text = "Add new row";
            this.addNewRowToolStripMenuItem.Click += new System.EventHandler(this.addNewRowToolStripMenuItem_Click);
            // 
            // insertNewRowToolStripMenuItem
            // 
            this.insertNewRowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insertNewRowToolStripMenuItem.Image")));
            this.insertNewRowToolStripMenuItem.Name = "insertNewRowToolStripMenuItem";
            this.insertNewRowToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.insertNewRowToolStripMenuItem.Text = "Insert new row";
            this.insertNewRowToolStripMenuItem.Click += new System.EventHandler(this.insertNewRowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // deleteRowToolStripMenuItem
            // 
            this.deleteRowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmToolStripMenuItem});
            this.deleteRowToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteRowToolStripMenuItem.Image")));
            this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
            this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteRowToolStripMenuItem.Text = "Delete row";
            // 
            // confirmToolStripMenuItem
            // 
            this.confirmToolStripMenuItem.Name = "confirmToolStripMenuItem";
            this.confirmToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.confirmToolStripMenuItem.Text = "Confirm";
            this.confirmToolStripMenuItem.Click += new System.EventHandler(this.confirmDeleteRowToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsBtnOpen,
            this.tsbtnNewFile,
            this.toolStripSeparator1,
            this.tsBtnSave,
            this.toolStripSeparator3,
            this.tsSaveAll,
            this.toolStripSeparator4,
            this.tsbtnAddNewRowItem});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(434, 39);
            this.toolStrip.TabIndex = 14;
            this.toolStrip.Text = "toolStrip";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsBtnOpen
            // 
            this.tsBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnOpen.Enabled = false;
            this.tsBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnOpen.Image")));
            this.tsBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnOpen.Name = "tsBtnOpen";
            this.tsBtnOpen.Size = new System.Drawing.Size(36, 36);
            this.tsBtnOpen.Text = "Open";
            // 
            // tsbtnNewFile
            // 
            this.tsbtnNewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNewFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnNewFile.Image")));
            this.tsbtnNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNewFile.Name = "tsbtnNewFile";
            this.tsbtnNewFile.Size = new System.Drawing.Size(36, 36);
            this.tsbtnNewFile.Text = "Save";
            this.tsbtnNewFile.Click += new System.EventHandler(this.tsbtnNewFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsBtnSave
            // 
            this.tsBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnSave.Image")));
            this.tsBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnSave.Name = "tsBtnSave";
            this.tsBtnSave.Size = new System.Drawing.Size(36, 36);
            this.tsBtnSave.Text = "Save";
            this.tsBtnSave.Click += new System.EventHandler(this.tsBtnSave_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tsSaveAll
            // 
            this.tsSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSaveAll.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveAll.Image")));
            this.tsSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveAll.Name = "tsSaveAll";
            this.tsSaveAll.Size = new System.Drawing.Size(36, 36);
            this.tsSaveAll.Text = "Save All";
            this.tsSaveAll.Click += new System.EventHandler(this.tsSaveAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbtnAddNewRowItem
            // 
            this.tsbtnAddNewRowItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAddNewRowItem.Image")));
            this.tsbtnAddNewRowItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddNewRowItem.Name = "tsbtnAddNewRowItem";
            this.tsbtnAddNewRowItem.Size = new System.Drawing.Size(118, 36);
            this.tsbtnAddNewRowItem.Text = "Add New Row";
            this.tsbtnAddNewRowItem.ToolTipText = "addNewRowItem";
            this.tsbtnAddNewRowItem.Click += new System.EventHandler(this.tsbtnAddNewRowItem_Click);
            // 
            // tabCtrl
            // 
            this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl.ContextMenuStrip = this.tabsContextMenu;
            this.tabCtrl.Controls.Add(this.tabPage1);
            this.tabCtrl.Controls.Add(this.tabPage2);
            this.tabCtrl.Location = new System.Drawing.Point(0, 42);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(407, 23);
            this.tabCtrl.TabIndex = 15;
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.tabCtrl_SelectedIndexChanged);
            this.tabCtrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabCtrl_MouseDown);
            // 
            // tabsContextMenu
            // 
            this.tabsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTabToolStripMenuItem,
            this.removeTabToolStripMenuItem,
            this.toolStripSeparator5,
            this.editTextToolStripMenuItem});
            this.tabsContextMenu.Name = "tabsContextMenu";
            this.tabsContextMenu.Size = new System.Drawing.Size(181, 98);
            this.tabsContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addTabToolStripMenuItem
            // 
            this.addTabToolStripMenuItem.Name = "addTabToolStripMenuItem";
            this.addTabToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addTabToolStripMenuItem.Text = "Add tab";
            // 
            // removeTabToolStripMenuItem
            // 
            this.removeTabToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmToolStripMenuItem1});
            this.removeTabToolStripMenuItem.Name = "removeTabToolStripMenuItem";
            this.removeTabToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeTabToolStripMenuItem.Text = "Remove tab";
            // 
            // confirmToolStripMenuItem1
            // 
            this.confirmToolStripMenuItem1.Name = "confirmToolStripMenuItem1";
            this.confirmToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.confirmToolStripMenuItem1.Text = "Confirm";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(399, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(399, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnAddNewTab
            // 
            this.btnAddNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewTab.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewTab.Location = new System.Drawing.Point(405, 39);
            this.btnAddNewTab.Name = "btnAddNewTab";
            this.btnAddNewTab.Size = new System.Drawing.Size(26, 26);
            this.btnAddNewTab.TabIndex = 17;
            this.btnAddNewTab.Text = "+";
            this.btnAddNewTab.UseVisualStyleBackColor = true;
            // 
            // editTextToolStripMenuItem
            // 
            this.editTextToolStripMenuItem.Name = "editTextToolStripMenuItem";
            this.editTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editTextToolStripMenuItem.Text = "Change Name";
            this.editTextToolStripMenuItem.Click += new System.EventHandler(this.editTextToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // DataGridViewSendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.ControlBox = false;
            this.Controls.Add(this.btnAddNewTab);
            this.Controls.Add(this.tabCtrl);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dgv);
            this.Name = "DataGridViewSendForm";
            this.Text = "DataGridViewSendForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.dgwContextMenu.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tabCtrl.ResumeLayout(false);
            this.tabsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnOpen;
        private System.Windows.Forms.ToolStripButton tsbtnNewFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton tsbtnAddNewRowItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip dgwContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addNewRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertNewRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip tabsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeTabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmToolStripMenuItem1;
        private System.Windows.Forms.Button btnAddNewTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem editTextToolStripMenuItem;
    }
}
