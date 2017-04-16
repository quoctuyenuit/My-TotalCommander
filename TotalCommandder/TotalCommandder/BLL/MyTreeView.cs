using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder.BLL
{
    class MyTreeView
    {

        private static MyTreeView instances;

        public static MyTreeView Instances
        {
            get
            {
                if (instances == null)
                    instances = new MyTreeView();
                return MyTreeView.instances; }
            set { MyTreeView.instances = value; }
        }

        #region Init TreeView
        public void initTreeView(TreeView treeView)
        {
            treeView.ImageList.Images.Add("MyDocumentsIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments.ToString()));
            treeView.ImageList.Images.Add("DesktopIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop.ToString()));
            treeView.ImageList.Images.Add("MyPicturesIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.MyPictures.ToString()));
            treeView.ImageList.Images.Add("FolderIcon", BLL.ShellIcon.GetLargeFolderIcon());

            treeView.Nodes.Clear();
            TreeNode root = new TreeNode();
            root.Name = "Desktop";
            root.Text = "DeskTop";
            root.ImageKey = "DesktopIcon";
            root.SelectedImageKey = "DesktopIcon";
            root.Tag = Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop;
            treeView.Nodes.Add(root);//Add root
            root.Expand();

            AddMyDocuments(root);
            AddMyComputer(root, treeView);
        }
        #endregion

        #region Add For Tree View
        private void AddMyComputer(TreeNode root, TreeView treeView)
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
            foreach(DriveInfo drive in drives)
            {
                string name = ((string.IsNullOrEmpty(drive.VolumeLabel)) ? "Local Drive" : drive.VolumeLabel) + " (" + drive.Name + ")";
                TreeNode driveNode = new TreeNode();
                driveNode.Name = drive.Name;
                driveNode.Text = name;
                treeView.ImageList.Images.Add(drive.Name, BLL.ShellIcon.GetLargeIcon(drive.Name));
                driveNode.ImageKey = drive.Name;
                driveNode.SelectedImageKey = drive.Name;
                driveNode.Tag = drive.Name;
                if (isExistsChildNode(driveNode))
                    driveNode.Nodes.Add("temp");
                node.Nodes.Add(driveNode);
            }
            root.Nodes.Add(node);
        }

        private void AddMyDocuments(TreeNode root)
        {
            TreeNode node = new TreeNode();
            node.Name = "MyDocuments";
            node.Text = "My Documents";
            node.ImageKey = "MyDocumentsIcon";
            node.SelectedImageKey = "MyDocumentsIcon";
            node.Tag = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments;
            if (isExistsChildNode(node))
                node.Nodes.Add("temp");
            root.Nodes.Add(node);
        }

        public void AddSubDirectory(TreeNode parentNode, TreeView treeView)
        {
            DirectoryInfo[] directories = new DirectoryInfo(parentNode.Tag.ToString()).GetDirectories();
            foreach(DirectoryInfo directory in directories)
            {
                string[] temp = directory.Attributes.ToString().Split(',');
                if (temp.Contains("Hidden") || temp.Contains(" Hidden"))
                    continue;

                TreeNode node = new TreeNode();
                node.Name = directory.Name;
                node.Text = directory.Name;
                node.ImageKey = "FolderIcon";
                node.SelectedImageKey = "FolderIcon";
                node.Tag = directory.FullName;
                if (isExistsChildNode(node))
                    node.Nodes.Add("temp");
                parentNode.Nodes.Add(node);
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
        #endregion
    }
}
