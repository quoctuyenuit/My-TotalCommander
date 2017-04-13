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
        Stack<string> listBack;

        public delegate string DgetCopyPath();

        public DgetCopyPath getCopyPath;

        public delegate void DgetCopyAction(string path, bool isCopy);

        public DgetCopyAction getCopyAction;

        public delegate void DgetPasteAction(string path);

        public DgetPasteAction getPasteAction;

        public delegate void DgetRefreshAction(Form1.Drefresh refresh);

        public DgetRefreshAction getRefreshAction;

        public uc_DirectoryList()
        {
            InitializeComponent();
            listBack = new Stack<string>();

        }

        void refreshScreen()
        {
            lvMain.Clear();
            showDirectoryAndFiles(this.listBack.Peek());
        }

        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Initialy

        private void initTreeView()
        {

        }

        private void uc_DirectoryList_Load(object sender, EventArgs e)
        {
            showDrives();
            cbPath.Text = "This PC";
            cbPath.Properties.Items.Add("This PC");
            listBack.Push(cbPath.Text);
            if (getRefreshAction != null)
                getRefreshAction(new Form1.Drefresh(refreshScreen));
        }
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Show Directory and files
        private void showDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                string name = ((string.IsNullOrEmpty(drive.VolumeLabel)) ? "Local Drive" : drive.VolumeLabel) + " (" + drive.Name + ")";

                imList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(drive.Name).ToBitmap());
                ListViewItem item = new ListViewItem(name, imList.Images.Count - 1);

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

                imList.Images.Add(BLL.ShellIcon.GetLargeFolderIcon().ToBitmap());

                lvMain.Items.Add(new ListViewItem(direc.Name, imList.Images.Count - 1));
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

                imList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(f.FullName).ToBitmap());

                ListViewItem item = new ListViewItem(f.Name, imList.Images.Count - 1);
                lvMain.Items.Add(item);
            }
        }

        private string showDirectoryAndFiles(string path)//Hiển thị các thư mục và các file hiện tại, hàm trả về đường dẫn mới
        {
            try
            {
                if (path.Equals("This PC"))
                {
                    showDrives();
                    return path;
                }

                showDirectory(path);

                showFiles(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return path;
        }
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Click

        private void lvMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(cbPath.Text + @"\" + lvMain.FocusedItem.Text))
                {
                    System.Diagnostics.Process.Start(cbPath.Text + @"\" + lvMain.FocusedItem.Text);
                    return;
                }

                string lvOnFocus = lvMain.FocusedItem.Text;

                if (lvOnFocus.Contains(':'))
                {
                    int indexStart = lvOnFocus.LastIndexOf('(') + 1;
                    int indexEnd = lvOnFocus.LastIndexOf(')');
                    char[] array = new char[indexEnd - indexStart];
                    lvOnFocus.CopyTo(indexStart, array, 0, indexEnd - indexStart);
                    lvOnFocus = string.Concat(array);
                }

                if (!this.listBack.Peek().Equals("This PC"))
                    this.listBack.Push(this.listBack.Peek() + @"\" + lvOnFocus);
                else
                    this.listBack.Push(lvOnFocus);


                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                string path = showDirectoryAndFiles(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                cbPath.Properties.Items.Add(path);

                if (string.IsNullOrEmpty(path))
                {
                    cbPath.Properties.Items.Clear();
                    cbPath.Text = "This PC";
                    cbPath.Properties.Items.Add("This PC");
                }
                else
                    cbPath.Text = path;
            }
            catch (Exception ex)
            { }
        }

        private void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.listBack.Count == 1)//Nếu trong stack chỉ có một phần tử đó là This PC thì không cho back nữa
                    return;

                listBack.Pop();//Xóa đi thư mục vừa mới thoát ra

                if (cbPath.SelectedIndex >= 0)
                    cbPath.Properties.Items.RemoveAt(cbPath.SelectedIndex);//Xóa đường dẫn vừa mới thoát ra khỏi combobox

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                string path = showDirectoryAndFiles(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                if (string.IsNullOrEmpty(path))
                {
                    cbPath.Properties.Items.Clear();
                    cbPath.Text = "This PC";
                    cbPath.Properties.Items.Add("This PC");
                }
                else
                    cbPath.Text = path;
            }
            catch (Exception ex)
            { }
        }

        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Menu

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (getCopyPath != null)
            {
                if (string.IsNullOrEmpty(getCopyPath()))
                    menuItemPaste.Enabled = false;
                else
                    menuItemPaste.Enabled = true;

                menuItemOpen.Enabled = false;
                menuItemCopy.Enabled = false;
                menuItemCut.Enabled = false;
                menuItemDelete.Enabled = false;

                foreach (ListViewItem item in lvMain.Items)
                    if (item.Selected)
                    {
                        menuItemOpen.Enabled = true;
                        if (!cbPath.Text.Equals("This PC"))
                        {
                            menuItemCopy.Enabled = true;
                            menuItemCut.Enabled = true;
                            menuItemDelete.Enabled = true;
                        }
                    }
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (getCopyAction != null)
                    getCopyAction(cbPath.Text + lvMain.FocusedItem.Text, true);

                menuItemPaste.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void menuItemCut_Click(object sender, EventArgs e)
        {
            try
            {
                if (getCopyAction != null)
                    getCopyAction(cbPath.Text + lvMain.FocusedItem.Text, false);

                menuItemPaste.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            if (getPasteAction != null)
                getPasteAction(cbPath.Text);

            refreshScreen();
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string path = cbPath.Text + lvMain.FocusedItem.Text;

                if (Directory.Exists(path))
                {
                    if (MessageBox.Show("Do you want to delete folder " + new DirectoryInfo(path).Name + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                    Directory.Delete(path, true);
                }
                else
                {
                    if (MessageBox.Show("Do you want to delete file " + new FileInfo(path).Name + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        return;
                    File.Delete(path);
                }

                lvMain.Items.Remove(lvMain.FocusedItem);

                menuItemDelete.Enabled = false;
            }
            catch (Exception ex) { }
        }

        public delegate string DgetFolderName();

        public DgetFolderName getFolderName;

        public void getFolderNameAction(DgetFolderName getFolderName)
        {
            this.getFolderName = getFolderName;
        }

        private void menuItemNewFolder_Click(object sender, EventArgs e)
        {
            NewFolderForm frm = new NewFolderForm();
            frm.getAction = new NewFolderForm.DgetAction(getFolderNameAction);
            frm.ShowDialog();
            if (getFolderName != null)
                Directory.CreateDirectory(cbPath.Text + getFolderName());
            refreshScreen();
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            lvMain_DoubleClick(null, null);
        }

        private void lvMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                foreach (ListViewItem item in lvMain.Items)
                    if (item.Selected)
                        lvMain_DoubleClick(null, null);
            }
            else if (e.KeyChar == (char)Keys.Back)
                btnBack_ItemClick(null, null);

        }

        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Combobox event

        private void cbPath_Properties_QueryCloseUp(object sender, CancelEventArgs e)
        {
            try
            {
                this.listBack.Push(cbPath.Text);

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                if (cbPath.Text.Equals("This PC"))
                {
                    showDrives();
                    return;
                }

                showDirectory(cbPath.Text);

                showFiles(cbPath.Text);
            }
            catch (Exception ex)
            { }
        }
    }
}
