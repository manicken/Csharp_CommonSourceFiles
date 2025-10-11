/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-03-31
 * Time: 10:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Microsan
{
    /// <summary>
    /// Description of RichTextBoxForm.
    /// </summary>
    public partial class RichTextBoxForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public RichTextBoxForm()
        {

            InitializeComponent();
            
            rtxt_setRightClickContextMenu();
            
            rtxt.TextChanged += rtxt_TextChanged;
            
            this.VisibleChanged += this_VisibleChanged;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        public RichTextBoxForm(string title): this()
        {
            this.Text = title;
           
        }
        
        private void this_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && this.TopLevel)
            {
                this.TopMost = true;
                this.TopMost = false;
            }
        }
        
        private void rtxt_TextChanged(object sender, EventArgs e)
        {
            this.Visible = true;
        }
        
        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }
        
        private void rtxt_setRightClickContextMenu()
        {
            
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem;
                //MenuItem menuItem = new MenuItem("Cut");
                //menuItem.Click += new EventHandler(CutAction);
                //contextMenu.MenuItems.Add(menuItem);
                
                menuItem = new MenuItem("Copy");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                
                //menuItemPaste = new MenuItem("Paste");
                //menuItemPaste.Click += new EventHandler(PasteAction);
                //contextMenu.MenuItems.Add(menuItemPaste);
                
                contextMenu.Popup += new EventHandler(ContextMenu_Popup_Action);
                rtxt.ContextMenu = contextMenu;
        }
        
        private void ContextMenu_Popup_Action(object s, EventArgs ea)
        {
            //menuItemPaste.Enabled = Clipboard.ContainsText();
        }
        
        private void CutAction(object sender, EventArgs e)
        {
        //    rtxt.Cut();
        }

        private void CopyAction(object sender, EventArgs e)
        {
            //Clipboard.SetData(DataFormats.Text, fastColoredTextBox.SelectedText);
            //Clipboard.Clear();
            rtxt.Copy();
        }
        
        private void PasteAction(object sender, EventArgs e)
        {
        //    rtxtLog.Paste();
            //fastColoredTextBox.SelectedText = Clipboard.GetText();
        } 
        
        private void tsBtnSaveToFile_Click(object sender, EventArgs e)
        {
            string filePath;
            if (QuickDialogs.FileSave(Application.StartupPath, "", "RichText Files|*.rtf", out filePath))
            {
                rtxt.SaveFile(filePath, RichTextBoxStreamType.RichText);
            }
        }

        private void tsbtnClear_Click(object sender, EventArgs e)
        {
            rtxt.Clear();
        }
    }

    public static class RichTextExtenstions
    {
        public static void AppendTextLine(this RichTextBox thisRtxtBox, string t, HorizontalAlignment a)
        {
            thisRtxtBox.Select(thisRtxtBox.TextLength, 0);
            thisRtxtBox.SelectionAlignment = a;
            thisRtxtBox.SelectedText = t + "\n";
        }
            
    }
}
