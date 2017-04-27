using System;
using System.Collections.Generic;
using System.Drawing;
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
            treeView.ImageList.Images.Add("FolderIcon", BLL.ShellIcon.GetLargeFolderIcon());

            treeView.Nodes.Clear();

            AddDesktop(treeView);
            AddDownloads(treeView);
            AddDocuments(treeView);
            AddPictures(treeView);
            AddMyComputer(treeView);
        }

        #endregion

        #region AddTreeView

        //Thêm một node gốc vào treeView
        private void AddRoot(TreeView treeView, string strName, string ImageKey, string ItemTag)
        {
            TreeNode node = new TreeNode();
            node.Name = strName;
            node.Text = strName;
            node.ImageKey = ImageKey;
            node.SelectedImageKey = ImageKey;
            node.Tag = ItemTag;
            if (isExistsChildNode(node))
                node.Nodes.Add("temp");
            treeView.Nodes.Add(node);
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

        private void AddDesktop(TreeView treeView)
        {
            treeView.ImageList.Images.Add("DesktopIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop.ToString()));
            
            this.AddRoot(treeView, "Desktop", "DesktopIcon", Microsoft.VisualBasic.FileIO.SpecialDirectories.Desktop);
        }

        private void AddDownloads(TreeView treeView)
        {
            
            string strPathTemp = Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments; ;

            string Tag = strPathTemp.Remove(strPathTemp.LastIndexOf('\\') + 1) + "Downloads";

            treeView.ImageList.Images.Add("DownloadsIcon", BLL.ShellIcon.GetLargeIcon(Tag));

            this.AddRoot(treeView, "Downloads", "DownloadsIcon", Tag);
        }

        private void AddDocuments(TreeView treeView)
        {
            treeView.ImageList.Images.Add("DocumentsIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments.ToString()));
            
            this.AddRoot(treeView, "Documents", "DocumentsIcon", Microsoft.VisualBasic.FileIO.SpecialDirectories.MyDocuments);
        }

        private void AddPictures(TreeView treeView)
        {
            treeView.ImageList.Images.Add("PicturesIcon", BLL.ShellIcon.GetLargeIcon(Microsoft.VisualBasic.FileIO.SpecialDirectories.MyPictures.ToString()));
            
            this.AddRoot(treeView, "Pictures", "PicturesIcon", Microsoft.VisualBasic.FileIO.SpecialDirectories.MyPictures);
        }

        private void AddMyComputer(TreeView treeView)
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
                string name = ((string.IsNullOrEmpty(drive.VolumeLabel)) ? "Local Drive" : drive.VolumeLabel) + " (" + drive.Name + ")";
                TreeNode driveNode = new TreeNode();
                driveNode.Name = drive.Name;
                driveNode.Text = name;
                treeView.ImageList.Images.Add(drive.Name,BLL.ShellIcon.GetLargeIcon(drive.Name));
                driveNode.ImageKey = drive.Name;
                driveNode.SelectedImageKey = drive.Name;
                driveNode.Tag = drive.Name;
                if (isExistsChildNode(driveNode))
                    driveNode.Nodes.Add("temp");
                node.Nodes.Add(driveNode);
            }
            treeView.Nodes.Add(node);
        }

        //Add các thư mục vào treeView
        public void AddDirectory(TreeNode parentNode, TreeView treeView)
        {
            DirectoryInfo[] directories = new DirectoryInfo(parentNode.Tag.ToString()).GetDirectories();
            foreach(DirectoryInfo directory in directories)
            {
                string[] temp = directory.Attributes.ToString().Split(',');
                if (temp.Contains("Hidden") || temp.Contains(" Hidden"))
                    continue;


                this.AddNode(parentNode, directory.Name, "FolderIcon", directory.FullName);

            }
        }

        #endregion

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
