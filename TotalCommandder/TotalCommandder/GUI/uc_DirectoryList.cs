using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TotalCommandder.GUI
{
    public partial class uc_DirectoryList : UserControl
    {
        private static uc_DirectoryList instances;

        public static uc_DirectoryList Instances
        {
            get
            {
                if (instances == null)
                    instances = new uc_DirectoryList();
                return uc_DirectoryList.instances;
            }
            set { uc_DirectoryList.instances = value; }
        }

        Stack<string> listBack;

        public string copyPath { get; set; }

        public bool isCopy { get; set; }

        public delegate void DgetCopy(string path, bool isCopy);
        public DgetCopy getCopy;

        public uc_DirectoryList()
        {
            InitializeComponent();
            listBack = new Stack<string>();
        }

        private void uc_DirectoryList_Load(object sender, EventArgs e)
        {
            showDrives();
            cbPath.Text = "This PC";
            listBack.Push(cbPath.Text);
        }

        private void showDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                ListViewItem item = new ListViewItem(drive.Name, 0);
                lvMain.Items.Add(item);
            }

        }

        private void showDirectory(string path)
        {
            DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
            foreach (DirectoryInfo direc in directories)
            {
                string[] temp = direc.Attributes.ToString().Split(',');
                if (temp.Contains("Hidden") || temp.Contains(" Hidden"))
                    continue;
                lvMain.Items.Add(new ListViewItem(direc.Name, 1));
            }
        }

        private void showFiles(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            foreach (FileInfo f in files)
            {
                string[] temp = f.Attributes.ToString().Split(',');
                if (temp.Contains("Hidden") || temp.Contains(" Hidden"))
                    continue;
                if (f.Attributes != FileAttributes.Hidden)
                    lvMain.Items.Add(new ListViewItem(f.Name, 2));
            }
        }

        private string showDirectoryAndFiles()//Hiển thị các thư mục và các file hiện tại, hàm trả về đường dẫn mới
        {
            string strPath = "";

            foreach (string str in listBack.AsEnumerable())
                if (!str.Equals("This PC"))
                    strPath = str + strPath;

            if (string.IsNullOrEmpty(strPath))
            {
                showDrives();
                return strPath;
            }

            showDirectory(strPath);

            showFiles(strPath);

            return strPath;
        }

        private void lvMain_DoubleClick(object sender, EventArgs e)
        {
            string lvOnFocus = lvMain.FocusedItem.Text;

            //Thêm tên thư mục hiện tại vào stack
            if (!string.IsNullOrEmpty(lvOnFocus))
                if (lvOnFocus.Equals(@"\"))
                    this.listBack.Push(lvOnFocus);
                else
                    this.listBack.Push(lvOnFocus + @"\");

            lvMain.Clear();

            string path = showDirectoryAndFiles();

            cbPath.Properties.Items.Add(path);

            if (string.IsNullOrEmpty(path))
                cbPath.Text = "This PC";
            else
                cbPath.Text = path;
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            listBack.Pop();//Xóa đi thư mục vừa mới thoát ra

            if (cbPath.SelectedIndex >= 0)
                cbPath.Properties.Items.RemoveAt(cbPath.SelectedIndex);//Xóa đường dẫn vừa mới thoát ra khỏi combobox

            lvMain.Clear();

            string path = showDirectoryAndFiles();

            if (string.IsNullOrEmpty(path))
                cbPath.Text = "This PC";
            else
                cbPath.Text = path;
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(copyPath))
                menuItemPaste.Enabled = false;
            else
                menuItemPaste.Enabled = true;

            foreach (ListViewItem item in lvMain.Items)
                if (item.Selected && !cbPath.Text.Equals("This PC"))
                {
                    menuItemCopy.Enabled = true;
                    menuItemCut.Enabled = true;
                    menuItemDelete.Enabled = true;
                    return;
                }
            menuItemCopy.Enabled = false;
            menuItemCut.Enabled = false;
            menuItemDelete.Enabled = false;
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            if (getCopy != null)
                getCopy(cbPath.Text + lvMain.FocusedItem.Text, true);
        }

        private void menuItemCut_Click(object sender, EventArgs e)
        {
            this.copyPath = "";

            this.copyPath = cbPath.Text + lvMain.FocusedItem.Text;

            this.isCopy = false;
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            BLL.ClassBLL.Instances.copyDirectory(this.copyPath, cbPath.Text);

            if (!isCopy)//Nếu là chức năng Cut thì xóa bản cũ
            {
                if (Directory.Exists(copyPath))
                    Directory.Delete(copyPath, true);
                else
                    File.Delete(copyPath);
            }

            lvMain.Clear();

            showDirectoryAndFiles();
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            string path = cbPath.Text + lvMain.FocusedItem.Text;

            if (Directory.Exists(path))
                Directory.Delete(path, true);
            else
                File.Delete(path);

            lvMain.Items.Remove(lvMain.FocusedItem);
        }

        public delegate string DgetFolderName();
        public DgetFolderName getFolderName;
        public void getAction(DgetFolderName getFolderName)
        {
            this.getFolderName = getFolderName;
        }

        private void menuItemNewFolder_Click(object sender, EventArgs e)
        {
            NewFolderForm frm = new NewFolderForm();
            frm.getAction = new NewFolderForm.DgetAction(getAction);
            frm.ShowDialog();
            if (getFolderName != null)
                Directory.CreateDirectory(cbPath.Text + getFolderName());
            lvMain.Clear();
            showDirectoryAndFiles();
        }
    }
}
