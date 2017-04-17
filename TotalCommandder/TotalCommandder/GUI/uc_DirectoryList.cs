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

        public Stack<string> ListBack
        {
            get { return listBack; }
            set { listBack = value; }
        }

        Stack<string> listForward;

        public delegate List<string> DgetCopyPath();

        public DgetCopyPath getCopyPath;

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

        private string oldNameItem;

        //public delegate void DgetLoadViewDelegate(Form1.DloadListView loadListView);

        ////public DgetLoadViewDelegate getLoadView

        public uc_DirectoryList(ContextMenuStrip contextMenu)
        {
            InitializeComponent();
            this.listBack = new Stack<string>();

            this.listForward = new Stack<string>();

            this.contextMenu = contextMenu;

            BLL.MyTreeView.Instances.initTreeView(tvMain);
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
        #region Show Directory And Files

        private void showDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
                BLL.ClassBLL.Instances.showDrive(drive, lvMain);
        }

        private void showDirectories(string path)
        {
            DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
            foreach (DirectoryInfo direc in directories)
            {
                if ((direc.Attributes | FileAttributes.Hidden) != direc.Attributes)//Chỉ hiển thị những file không ẩn
                    BLL.ClassBLL.Instances.showDirectory(direc, lvMain);
            }
        }

        private void showFiles(string path)
        {
            FileInfo[] files = new DirectoryInfo(path).GetFiles();

            foreach (FileInfo file in files)
            {
                if ((file.Attributes | FileAttributes.Hidden) != file.Attributes)//Chỉ hiển thị những file không ẩn
                    BLL.ClassBLL.Instances.showFile(file, lvMain);
            }
        }

        public void showDirectoryAndFiles(string path)//Hiển thị các thư mục và các file hiện tại, hàm trả về đường dẫn mới
        {
            lvMain.LargeImageList.Images.Clear();
            lvMain.SmallImageList.Images.Clear();
            lvMain.StateImageList.Images.Clear();

            if (path.Equals("This PC"))
            {
                showDrives();
                return;
            }

            showDirectories(path);

            showFiles(path);
        }
        #endregion
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Click
        #region EventClick
        private void lvMain_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string path = lvMain.SelectedItems[0].Tag.ToString();

                if (File.Exists(path))
                {
                    System.Diagnostics.Process.Start(path);
                    return;
                }

                this.listBack.Push(path);

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                showDirectoryAndFiles(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                cbPath.Text = path;

                cbPath.Properties.Items.Add(path);

                btnBack.Enabled = true;//Bật back

                btnForward.Enabled = false;//Tắt forward

                this.listForward.Clear();//Xóa ngăn xếp forward
            }
            catch (Exception ex)
            { }
        }

        public void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //if (this.listBack.Count == 1)//Nếu trong stack chỉ có một phần tử đó là This PC thì không cho back nữa
                //    return;

                this.listForward.Push(listBack.Pop());//Xóa đi thư mục vừa mới thoát ra đồng thời lưu hoạt động đó vào Forward

                if (cbPath.SelectedIndex >= 0)
                    cbPath.Properties.Items.RemoveAt(cbPath.SelectedIndex);//Xóa đường dẫn vừa mới thoát ra khỏi combobox

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                string path = this.listBack.Peek();

                showDirectoryAndFiles(path);

                this.Cursor = Cursors.Arrow;

                if (string.IsNullOrEmpty(path))
                {
                    cbPath.Properties.Items.Clear();
                    cbPath.Text = "This PC";
                    cbPath.Properties.Items.Add("This PC");
                }
                else
                    cbPath.Text = path;

                if (this.listBack.Count == 1)//Nếu stack chỉ còn phần tử This PC thì không cho back nữa
                    btnBack.Enabled = false;

                btnForward.Enabled = true;//Bật forward
            }
            catch (Exception ex)
            { }
        }

        private void btnForward_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //Xóa đi thư mục vừa mới thoát ra trong Forward đồng thời lưu vào combobox
            string pathForward = listForward.Pop();

            this.listBack.Push(pathForward);

            lvMain.Clear();

            this.Cursor = Cursors.AppStarting;

            showDirectoryAndFiles(pathForward);

            this.Cursor = Cursors.Arrow;

            cbPath.Text = pathForward;

            cbPath.Properties.Items.Add(pathForward);

            if (this.listForward.Count == 0)//Nếu hết phần tử trong forward thì tắt forward
                btnForward.Enabled = false;

            btnBack.Enabled = true;//Bật back
        }

        private void btnUpTo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                string path;

                if (cbPath.Text.Equals("This PC"))
                    path = tvMain.TopNode.Tag.ToString();
                else
                {
                    DirectoryInfo direc = new DirectoryInfo(cbPath.Text).Parent;
                    path = (direc != null) ? direc.FullName : "This PC";
                }

                this.listBack.Push(path);

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                showDirectoryAndFiles(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                cbPath.Text = path;

                cbPath.Properties.Items.Add(path);

                btnBack.Enabled = true;

                btnForward.Enabled = false;

                this.listForward.Clear();
            }
            catch (Exception ex)
            { }
        }
        #endregion
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Menu
        #region InitContextMenu
        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (getCopyPath != null)
            {
                if (getCopyPath().Count == 0)
                    menuItemPaste.Enabled = false;
                else
                    menuItemPaste.Enabled = true;

                menuItemOpen.Enabled = false;
                menuItemPack.Enabled = false;
                menuItemUnpack.Enabled = false;
                menuItemCopy.Enabled = false;
                menuItemCut.Enabled = false;
                menuItemDelete.Enabled = false;

                if (lvMain.SelectedItems.Count > 0)
                {
                    menuItemOpen.Enabled = true;
                    if (!cbPath.Text.Equals("This PC"))
                    {
                        menuItemPack.Enabled = true;
                        menuItemUnpack.Enabled = true;
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

                    foreach (ListViewItem item in lvMain.SelectedItems)
                        listPathCopy.Add(item.Tag.ToString());

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

                    foreach (ListViewItem item in lvMain.SelectedItems)
                        listPathCopy.Add(item.Tag.ToString());
                    getCopyAction(listPathCopy, false);
                }

                menuItemPaste.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            if (getPasteAction != null)
            {
                if (!Directory.Exists(cbPath.Text))//Nếu pastePath không tồn tại thì thoát
                    return;

                getPasteAction(cbPath.Text);
            }


            refreshScreen();
        }

        private void menuItemDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listPath = new List<string>();

                foreach (ListViewItem item in lvMain.SelectedItems)
                    listPath.Add(item.Tag.ToString());

                if (BLL.ClassBLL.Instances.deleteSendToRecycleBin(listPath))
                {
                    foreach (ListViewItem item in lvMain.SelectedItems)
                        lvMain.Items.RemoveByKey(item.Name);
                }

                menuItemDelete.Enabled = false;
            }
            catch (Exception ex) { }
        }

        private void menuItemNewFolder_Click(object sender, EventArgs e)
        {
            GetNewFolderName.GetFolderNameForm dialog = new GetNewFolderName.GetFolderNameForm();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            Directory.CreateDirectory(cbPath.Text + @"\" + dialog.FolderName);
            refreshScreen();
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            lvMain_DoubleClick(null, null);
        }

        private void lvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.lvMain.SelectedItems.Count > 0)
                    lvMain_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Back)
                btnBack_ItemClick(null, null);

            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Delete && !cbPath.Text.Equals("This PC"))//Nếu người dùng nhấn Shift + Delete và không phải ở This PC thì cho phép xóa
            {
                List<string> listPath = new List<string>();

                foreach (ListViewItem item in lvMain.SelectedItems)
                    listPath.Add(item.Tag.ToString());

                if (BLL.ClassBLL.Instances.deletePermanently(listPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs))
                {
                    foreach (ListViewItem item in lvMain.SelectedItems)
                        lvMain.Items.RemoveByKey(item.Name);
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                ListViewItem item = lvMain.SelectedItems[0];//Lấy Item đang được chọn

                this.oldNameItem = item.Name;

                item.BeginEdit();
            }
        }
        #endregion
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Combobox event
        #region ComboboxEvent

        public void cbPath_Properties_QueryCloseUp(object sender, CancelEventArgs e)
        {
            if (cbPath.Text.Equals(listBack.Peek()))//Nếu không thay đổi đường dẫn thì không làm gì
                return;
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
                if (!cbPath.Equals("This PC") || Directory.Exists(cbPath.Text))//Nếu đường dẫn khác This PC hoặc đường dẫn đó tồn tại thì mới được làm việc ngược lại thông báo và thoát
                    cbPath_Properties_QueryCloseUp(null, null);
                else
                {
                    cbPath.Text = this.listBack.Peek();
                    MessageBox.Show("The path is not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void cbPath_TextChanged(object sender, EventArgs e)
        {
            if (cbPath.Text.Equals("This PC"))
                menuItemNewFolder.Enabled = false;
            else
                menuItemNewFolder.Enabled = true;
        }
        #endregion
        //======================================================================================================================================
        //======================================================================================================================================
        //======================================================================================================================================
        //Get and set contextMenu
        #region Passing ContextMenu Between Parent And Child
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

        private void lvMain_Enter(object sender, EventArgs e)
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
        #endregion

        #region DragDrop

        private void lvMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                List<string> listPathCopy = new List<string>();

                List<ListViewItem> listItem = new List<ListViewItem>();

                listItem = e.Data.GetData(typeof(List<ListViewItem>)) as List<ListViewItem>;

                foreach (ListViewItem item in listItem)
                {
                    string pathDrop = item.Tag.ToString();

                    string fileName = pathDrop.Substring(pathDrop.LastIndexOf('\\') + 1);

                    if (string.IsNullOrEmpty(fileName))
                        return;

                    //Nếu file chuyển tới đã tồn tại thì thông báo và thoát ra
                    if (lvMain.Items[fileName] != null)
                    {
                        MessageBox.Show("The folder \"" + item.Text + "\" already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }

                    listPathCopy.Add(item.Tag.ToString());
                }

                if (getCopyAction != null)
                {
                    getCopyAction(listPathCopy, true);
                }

                if (getPasteAction != null)
                {
                    if (!Directory.Exists(cbPath.Text))//Nếu pastePath không tồn tại thì thoát
                        return;

                    getPasteAction(cbPath.Text);
                }
                refreshScreen();
            }
            catch (Exception ex) { }


        }

        private void lvMain_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<ListViewItem> list = new List<ListViewItem>();

            foreach (ListViewItem item in lvMain.SelectedItems)
                list.Add(item);

            lvMain.DoDragDrop(list, DragDropEffects.Copy);
        }
        #endregion

        #region TreeView Event

        private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string path = e.Node.Tag.ToString();

                this.listBack.Push(path);

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                showDirectoryAndFiles(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                cbPath.Text = path;

                cbPath.Properties.Items.Add(path);

                btnBack.Enabled = true;
            }
            catch (Exception ex)
            { }
        }

        private void tvMain_AfterExpand(object sender, TreeViewEventArgs e)
        {
            string nameNode = e.Node.Name;

            if (!nameNode.Equals("MyComputer"))
            {
                e.Node.Nodes.Clear();
                BLL.MyTreeView.Instances.AddDirectory(e.Node, tvMain);
            }
        }
        #endregion

        #region Pack
        private void menuItemPack_Click(object sender, EventArgs e)
        {
            string zipFilePath = lvMain.SelectedItems[0].Tag.ToString();//Lấy đường dẫn hiện tại

            zipFilePath = zipFilePath.Remove(zipFilePath.LastIndexOf('\\'));//Xóa tên file/folder sau cùng của đường dẫn

            GetNewFolderName.GetFolderNameForm getFolderName = new GetNewFolderName.GetFolderNameForm();//Mở form lấy tên folder mới

            if (getFolderName.ShowDialog() != DialogResult.OK)//Nếu người dùng nhấn No hoặc tắt hộp dialog thì thoát ra và không làm gì
                return;

            zipFilePath += "\\" + getFolderName.FolderName + ".zip";//Nối đường dẫn với tên folder mới và đuôi của file nén

            while (File.Exists(zipFilePath) || Directory.Exists(zipFilePath))//Nếu đường dẫn đã tồn tại thì yêu cầu nhập lại
            {
                MessageBox.Show("The Folder already exists! Please try again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (getFolderName.ShowDialog() != DialogResult.OK)
                    return;

                zipFilePath = zipFilePath.Remove(zipFilePath.LastIndexOf('\\'));

                zipFilePath += "\\" + getFolderName.FolderName + ".zip";
            }

            //Nếu tạo đóng gói thành công thì thêm file đó vào listView
            List<string> arrPath = new List<string>();

            foreach (ListViewItem item in lvMain.SelectedItems)
                arrPath.Add(item.Tag.ToString());

            if (BLL.ClassBLL.Instances.packFile(arrPath, zipFilePath))
            {
                lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIconFromExtension(zipFilePath));

                FileInfo file = new FileInfo(zipFilePath);

                ListViewItem item = new ListViewItem(file.Name, lvMain.LargeImageList.Images.Count - 1);

                item.Tag = file.FullName;

                item.Name = file.Name;

                lvMain.Items.Add(item);
            }
        }

        private void menuItemUnpack_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvMain.SelectedItems)
            {
                string zipFilePath = item.Tag.ToString();

                GetNewFolderName.GetFolderNameForm getFolderName = new GetNewFolderName.GetFolderNameForm();

                if (getFolderName.ShowDialog() != DialogResult.OK)
                    return;

                zipFilePath = zipFilePath.Remove(zipFilePath.LastIndexOf('\\'));

                zipFilePath += "\\" + getFolderName.FolderName;

                while (Directory.Exists(zipFilePath) || File.Exists(zipFilePath))
                {
                    MessageBox.Show("The Folder already exists! Please try again!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (getFolderName.ShowDialog() != DialogResult.OK)
                        return;

                    zipFilePath = zipFilePath.Remove(zipFilePath.LastIndexOf('\\'));

                    zipFilePath += "\\" + getFolderName.FolderName;
                }

                //Nếu giải nén thành công thì thêm file đó vào listview
                if (BLL.ClassBLL.Instances.unpackFile(item.Tag.ToString(), zipFilePath))
                {
                    lvMain.LargeImageList.Images.Add(BLL.ShellIcon.GetLargeIcon(zipFilePath));//.Remove(zipFilePath.LastIndexOf('.'))));

                    FileInfo file = new FileInfo(zipFilePath);

                    ListViewItem lvItem = new ListViewItem(file.Name, lvMain.LargeImageList.Images.Count - 1);

                    lvItem.Tag = file.FullName;

                    lvItem.Name = file.Name;

                    lvMain.Items.Add(lvItem);
                }
                else
                    MessageBox.Show("Sorry!, The format file is not supported!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }

        #endregion

        private void lvMain_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                if (e.Label != null)
                {
                    ListViewItem item = lvMain.Items[this.oldNameItem];

                    if (item.Name.Contains(':'))
                    {
                        MessageBox.Show("Sorry!, You can't rename the drive", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        //Nếu folder cần sửa là một drive thì thông báo và không cho sửa tên
                        e.CancelEdit = true;

                        return;
                    }

                    if (item.Tag != null)
                    {
                        string oldPath = item.Tag.ToString();

                        string newPath = Path.Combine(oldPath.Remove(oldPath.LastIndexOf('\\') + 1), e.Label);

                        if (e.Label != item.Text && (Directory.Exists(newPath) || File.Exists(newPath)))
                        {
                            MessageBox.Show("The folder name already exists! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            e.CancelEdit = true;

                            return;
                        }
                        FileInfo info = new FileInfo(newPath);
                        if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                            Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(item.Tag.ToString(), e.Label);
                        else
                            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(item.Tag.ToString(), e.Label);

                        item.Tag = newPath;
                    }
                }
            }
            catch (Exception ex) { e.CancelEdit = true; }
        }

        private void lvMain_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = lvMain.SelectedItems[0];//Lấy Item đang được chọn

            this.oldNameItem = item.Name;
        }


    }
}
