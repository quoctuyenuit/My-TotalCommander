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

        public delegate int DgetCountCopyPath();

        public DgetCountCopyPath getCountCopyPath;

        public delegate void DgetCopyAction(List<string> listPath, bool isCopy);

        public DgetCopyAction getCopyAction;

        public delegate void DgetPasteAction(string path);

        public DgetPasteAction getPasteAction;

        public delegate void DgetRefreshAction(Form1.Drefresh refresh);

        public DgetRefreshAction getRefreshAction;

        public delegate ContextMenuStrip DgetContextMenu();

        public DgetContextMenu getContextMenu;//delegate lấy contextMenu từ hàm form cha

        public delegate void DpushContextMenuForParent(ContextMenuStrip contextMenu);

        public DpushContextMenuForParent pushContextMenuForParent;//delegate đẩy contextMenu về cho hàm form1 cha

        public uc_DirectoryList(ContextMenuStrip contextMenu)
        {
            InitializeComponent();
            listBack = new Stack<string>();
            this.contextMenu = contextMenu;
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

        private Bitmap showImageAsThumbnailView(string path)
        {
            Bitmap bmp = new Bitmap(128, 128);

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(Image.FromFile(path), 0, 0, 128, 128);

            return bmp;
        }

        private void showFiles(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();

            foreach (FileInfo f in files)
            {
                string[] temp = f.Attributes.ToString().Split(',');

                if (temp.Contains("Hidden") || temp.Contains(" Hidden"))
                    continue;

                string extension = f.Extension;

                Bitmap bmp;

                extension = extension.ToLower();

                if (extension.Equals(".bmp") || extension.Equals(".ico") || extension.Equals(".gif") || extension.Equals(".jpeg") || extension.Equals(".jpg") || extension.Equals(".jfif") || extension.Equals(".png") || extension.Equals(".tif") || extension.Equals(".tiff") || extension.Equals(".wmf") || extension.Equals(".emf"))
                    bmp = showImageAsThumbnailView(path + "\\" + f.Name);
                else
                    bmp = BLL.ShellIcon.GetLargeIconFromExtension(f.FullName).ToBitmap();

                imList.Images.Add(bmp);

                ListViewItem item = new ListViewItem(f.Name, imList.Images.Count - 1);

                lvMain.Items.Add(item);
            }
        }

        private string showDirectoryAndFiles(string path)//Hiển thị các thư mục và các file hiện tại, hàm trả về đường dẫn mới
        {
            if (path.Equals("This PC"))
            {
                showDrives();
                return path;
            }

            showDirectory(path);

            showFiles(path);
            
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
            if (getCountCopyPath != null)
            {
                if (getCountCopyPath() == 0)
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
                {
                    List<string> listPathCopy = new List<string>();

                    foreach (ListViewItem item in lvMain.Items)
                        if (item.Selected)
                            listPathCopy.Add(cbPath.Text + @"\" + item.Text);
                    getCopyAction(listPathCopy, true);
                }

                menuItemPaste.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void menuItemCut_Click(object sender, EventArgs e)
        {
            try
            {
                if (getCopyAction != null)
                {
                    List<string> listPathCopy = new List<string>();

                    foreach (ListViewItem item in lvMain.Items)
                        if (item.Selected)
                            listPathCopy.Add(cbPath.Text + item.Text);
                    getCopyAction(listPathCopy, false);
                }

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

                showDirectoryAndFiles(cbPath.Text);
            }
            catch (Exception ex)
            {
                btnBack_ItemClick(null, null);
                MessageBox.Show("The path is not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvMain.Items)
                if (item.Selected)
                {
                    menuItemOpen.Enabled = true;
                    menuItemCopy.Enabled = true;
                    menuItemCut.Enabled = true;
                    menuItemDelete.Enabled = true;
                    return;
                }
            menuItemOpen.Enabled = false;
            menuItemCopy.Enabled = false;
            menuItemCut.Enabled = false;
            menuItemDelete.Enabled = false;
        }

        private void cbPath_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Directory.Exists(cbPath.Text))
                    cbPath_Properties_QueryCloseUp(null, null);
                else
                {
                    cbPath.Text = this.listBack.Peek();
                    MessageBox.Show("The path is not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Get and set contextMenu

        private void setContextMenu()//Thay đổi giá trị item của contextMenu tương ứng với các item bên ngoài
        {
            this.contextMenu.Items[0].Enabled = menuItemOpen.Enabled;
            this.contextMenu.Items[1].Enabled = menuItemCopy.Enabled;
            this.contextMenu.Items[2].Enabled = menuItemCut.Enabled;
            this.contextMenu.Items[3].Enabled = menuItemPaste.Enabled;
        }

        private void setItemOfContextMenu()//Thay đổi giá trị của các item trong contextMenu
        {
            menuItemOpen.Enabled = this.contextMenu.Items[0].Enabled;
            menuItemCopy.Enabled = this.contextMenu.Items[1].Enabled;
            menuItemCut.Enabled = this.contextMenu.Items[2].Enabled;
            menuItemPaste.Enabled = this.contextMenu.Items[3].Enabled;
        }

        private void lvMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (getContextMenu != null)
                this.contextMenu = getContextMenu();
            setItemOfContextMenu();
        }

        private void uc_DirectoryList_Leave(object sender, EventArgs e)
        {
            setContextMenu();
            if (pushContextMenuForParent != null)
                pushContextMenuForParent(this.contextMenu);
        }

        private void lvMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
            // MessageBox.Show("abc");
        }

        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
                MessageBox.Show(file);
        }

        private void lvMain_DragLeave(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        
    }
}
