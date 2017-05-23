using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder.GUI
{
    class NavigationPane : System.Windows.Forms.TreeView
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName,
                                                string pszSubIdList);
        protected override void CreateHandle()
        {
            base.CreateHandle();
            SetWindowTheme(this.Handle, "explorer", null);
        }

        public void InitializeComponent()
        {
            this.ImageList.Images.Add("FolderIcon", BLL.ShellIcon.GetLargeFolderIcon());

            this.AddDesktop();
            this.AddDownloads();
            this.AddDocuments();
            this.AddPictures();
            this.AddMyComputer();
            RefreshNavigationPane("This PC");
        }

        private TreeNode FindNode(string Tag, TreeNode parentNode)
        {
            parentNode.Expand();

            if (Tag.Equals(parentNode.Tag.ToString()))
                return parentNode;

            foreach (TreeNode node in parentNode.Nodes)
            {
                string tagStr = node.Tag.ToString();
                if (tagStr.LastIndexOf('\\') == tagStr.Length - 1)
                    tagStr = tagStr.Remove(tagStr.LastIndexOf('\\'));
                if (Tag.Contains(tagStr) && (Tag.Equals(tagStr) || Tag[tagStr.Length].Equals('\\')))
                    return FindNode(Tag, node);
            }

            return null;
        }

        public void RefreshNavigationPane(string path)
        {
            if (path.Equals("This PC"))
            {
                this.SelectedNode = this.Nodes["MyComputer"];
                return;
            }
            TreeNode parentNode = this.Nodes["MyComputer"];

            foreach (TreeNode node in this.Nodes)
                if (path.Contains(node.Tag.ToString()))
                {
                    parentNode = node;
                    break;
                }

            this.SelectedNode = FindNode(path, parentNode);
        }

        //Thêm một node gốc vào treeView
        private void AddRoot(string strName, string ImageKey, string ItemTag)
        {
            TreeNode node = new TreeNode();
            node.Name = strName;
            node.Text = strName;
            node.ImageKey = ImageKey;
            node.SelectedImageKey = ImageKey;
            node.Tag = ItemTag;
            if (isExistsChildNode(node))
                node.Nodes.Add("temp");
            this.Nodes.Add(node);
        }

        //Thêm một node con vào dưới một node cha có trước
        private void AddNode(TreeNode parentNode, string strName, string ImageKey, string ItemTag)
        {
            TreeNode node = new TreeNode();
            node.Name = strName;
            node.Text = strName;
            node.ImageKey = ImageKey;
            node.SelectedImageKey = ImageKey;
            node.Tag = ItemTag;
            if (isExistsChildNode(node))
                node.Nodes.Add("temp");
            parentNode.Nodes.Add(node);
        }

        private void AddDesktop()
        {
            this.ImageList.Images.Add("DesktopIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop.ToString()));

            this.AddRoot("Desktop", "DesktopIcon", Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop);
        }

        private void AddDownloads()
        {

            string strPathTemp = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments; ;

            string Tag = strPathTemp.Remove(strPathTemp.LastIndexOf('\\') + 1) + "Downloads";

            this.ImageList.Images.Add("DownloadsIcon", BLL.ShellIcon.GetLargeIcon(Tag));

            this.AddRoot("Downloads", "DownloadsIcon", Tag);
        }

        private void AddDocuments()
        {
            this.ImageList.Images.Add("DocumentsIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments.ToString()));

            this.AddRoot("Documents", "DocumentsIcon", Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments);
        }

        private void AddPictures()
        {
            this.ImageList.Images.Add("PicturesIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.MyPictures.ToString()));

            this.AddRoot("Pictures", "PicturesIcon", Microsoft.VisualBasic.FileIO.SpecialDirectories.MyPictures);
        }

        private void AddMyComputer()
        {
            TreeNode node = new TreeNode();
            node.Name = "MyComputer";
            node.Text = "My Computer";
            node.ImageKey = "MyComputerIcon";
            node.SelectedImageKey = "MyComputerIcon";
            node.Tag = "This PC";


            node.Expand();
            //Add Drives
            DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                string name;
                try
                {
                    name = ((string.IsNullOrEmpty(drive.VolumeLabel)) ? "Local Drive" : drive.VolumeLabel) + " (" + drive.Name + ")";
                }
                catch(Exception ex)
                {
                    name = drive.Name;
                }

                TreeNode driveNode = new TreeNode();

                driveNode.Name = drive.Name.Remove(drive.Name.LastIndexOf('\\'));
                driveNode.Text = name;
                this.ImageList.Images.Add(drive.Name, BLL.ShellIcon.GetLargeIcon(drive.Name));
                driveNode.ImageKey = drive.Name;
                driveNode.SelectedImageKey = drive.Name;
                driveNode.Tag = drive.Name;
                if (isExistsChildNode(driveNode))
                    driveNode.Nodes.Add("temp");
                node.Nodes.Add(driveNode);
            }
            this.Nodes.Add(node);
        }

        //Add các thư mục vào treeView
        public void AddDirectory(TreeNode parentNode)
        {
            DirectoryInfo[] directories = new DirectoryInfo(parentNode.Tag.ToString()).GetDirectories();
            foreach (DirectoryInfo directory in directories)
            {
                string[] temp = directory.Attributes.ToString().Split(',');
                if (temp.Contains("Hidden") || temp.Contains(" Hidden"))
                    continue;

                this.AddNode(parentNode, directory.Name, "FolderIcon", directory.FullName);
            }
        }

        private bool isExistsChildNode(TreeNode node)
        {
            try
            {
                string pathParent = node.Tag.ToString();//Lấy đường dẫn từ node cha
                if (Directory.GetDirectories(pathParent).Length > 0)
                    return true;
            }
            catch (Exception ex) { }
            return false;
        }
    }
}
