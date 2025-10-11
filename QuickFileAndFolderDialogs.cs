/*
 * Created by SharpDevelop.
 * User: Microsan84
 * Date: 2017-03-31
 * Time: 12:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace Microsan
{
    /// <summary>
    /// Description of QuickFileAndFolderDialogs.
    /// </summary>
    public static class QuickDialogs
    {

        public static bool FileSave(string InitialDirectory, string Title, string Filter, out string FilePath)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (InitialDirectory != "")
                    sfd.InitialDirectory = InitialDirectory;
                else
                    sfd.InitialDirectory = "C:\\";
                
                if (Title != "")
                    sfd.Title = Title;
                else
                    sfd.Title = "Save File As...";
                
                if (Filter != "")
                    sfd.Filter = Filter;
                else
                    sfd.Filter = "All Files|*.*";
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FilePath = sfd.FileName;
                    return true;
                }
                else
                {
                    FilePath = null;
                    return false;
                }
            }
        }
        public static bool FileOpen(string InitialDirectory, string Title, string Filter, out string FilePath)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (InitialDirectory != "")
                    ofd.InitialDirectory = InitialDirectory;
                else
                    ofd.InitialDirectory = "C:\\";

                if (Title != "")
                    ofd.Title = Title;
                else
                    ofd.Title = "Save File As...";

                if (Filter != "")
                    ofd.Filter = Filter;
                else
                    ofd.Filter = "All Files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FilePath = ofd.FileName;
                    return true;
                }
                else
                {
                    FilePath = null;
                    return false;
                }
            }
        }
    }
}
