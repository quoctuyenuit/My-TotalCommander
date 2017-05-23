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
using System.Runtime.InteropServices;

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

        #region Delegate

        //Delegate lấy danh sách các đường dẫn copy ở form1
        public delegate List<string> DgetCopyPath();

        public DgetCopyPath getCopyPath;

        //Delegate kích hoạt sự kiện khi copy
        public Action<List<string>, bool> CopyAction;

        public Action<string> PasteAction;

        public Action getRefreshAll;

        //Delegate set Enabled cho các button Copy Cut Paste... ở form1 
        public Action<bool> setEnabledButton;

        #endregion

        //Biến lưu lại tên cũ khi thay đổi tên item
        public string oldNameItem { get; set; }

        //Biến lưu lại tiến trình khi kích hoạt đa tiến trình
        private Task task;

        public uc_DirectoryList()
        {
            InitializeComponent();

            this.tvMain.InitializeComponent();

            this.listBack = new Stack<string>();

            this.listForward = new Stack<string>();
        }

        #region Show

        private void uc_DirectoryList_Load(object sender, EventArgs e)
        {
            cbPath.Text = "This PC";
            cbPath.Properties.Items.Add("This PC");
            listBack.Push(cbPath.Text);
        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (setEnabledButton != null)
                setEnabledButton(false);

            this.lvMain.ShowFromPath(this.listBack.Peek());
        }

        #endregion
        //==============================================================================================================
        //==================================================EventClick==================================================
        #region Back Forward And Up

        public void btnBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (this.listBack.Count == 1)//Nếu trong stack chỉ có một phần tử đó là This PC thì không cho back nữa
                    return;

                this.listForward.Push(listBack.Pop());//Xóa đi thư mục vừa mới thoát ra đồng thời lưu hoạt động đó vào Forward

                if (cbPath.SelectedIndex >= 0)
                    cbPath.Properties.Items.RemoveAt(cbPath.SelectedIndex);//Xóa đường dẫn vừa mới thoát ra khỏi combobox

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                string path = this.listBack.Peek();

                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(listBack.Peek());

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

                this.tvMain.RefreshNavigationPane(path);
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

            if (setEnabledButton != null)
                setEnabledButton(false);

            this.Cursor = Cursors.AppStarting;

            this.lvMain.ShowFromPath(pathForward);

            this.Cursor = Cursors.Arrow;

            cbPath.Text = pathForward;

            cbPath.Properties.Items.Add(pathForward);

            if (this.listForward.Count == 0)//Nếu hết phần tử trong forward thì tắt forward
                btnForward.Enabled = false;

            btnBack.Enabled = true;//Bật back

            this.tvMain.RefreshNavigationPane(pathForward);

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

                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                cbPath.Text = path;

                cbPath.Properties.Items.Add(path);

                btnBack.Enabled = true;

                btnForward.Enabled = false;

                this.listForward.Clear();

                this.tvMain.RefreshNavigationPane(path);

            }
            catch (Exception ex)
            { }
        }
        #endregion
        //==============================================================================================================
        //================================================InitContextMenu===============================================
        #region ContextMenu Event

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem item in contextMenu.Items)
                item.Enabled = false;

            menuItemProperties.Enabled = true;
            menuItemRefresh.Enabled = true;

            if (!cbPath.Text.Equals("This PC"))
                menuItemNew.Enabled = true;

            if (this.lvMain.SelectedItems.Count > 0)
            {
                if (cbPath.Text.Equals("This PC"))
                    menuItemOpen.Enabled = true;
                else
                {
                    foreach (ToolStripItem item in contextMenu.Items)
                        item.Enabled = true;
                }
            }

            if (getCopyPath != null && getCopyPath().Count > 0)
                menuItemPaste.Enabled = true;
            else
                menuItemPaste.Enabled = false;
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            lvMain_DoubleClick(null, null);
        }

        private void menuItemRefresh_Click(object sender, EventArgs e)
        {
            if (setEnabledButton != null)
                setEnabledButton(false);
            this.lvMain.ShowFromPath(this.listBack.Peek());
        }

        private void menuItemRename_Click(object sender, EventArgs e)
        {
            this.lvMain.SelectedItems[0].BeginEdit();
        }

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

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (CopyAction != null)
                {
                    List<string> listPathCopy = new List<string>();

                    foreach (ListViewItem item in lvMain.SelectedItems)
                        listPathCopy.Add(item.Tag.ToString());

                    CopyAction(listPathCopy, true);
                }

                menuItemPaste.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void menuItemCut_Click(object sender, EventArgs e)
        {
            try
            {
                if (CopyAction != null)
                {
                    List<string> listPathCopy = new List<string>();

                    foreach (ListViewItem item in lvMain.SelectedItems)
                    {
                        listPathCopy.Add(item.Tag.ToString());
                        item.ForeColor = Color.LightGray;
                    }

                    CopyAction(listPathCopy, false);
                }

                menuItemPaste.Enabled = true;
            }
            catch (Exception ex) { }
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            if (PasteAction != null)
            {
                if (!Directory.Exists(cbPath.Text))//Nếu pastePath không tồn tại thì thoát
                    return;

                this.task = Task.Run(() => PasteAction(cbPath.Text));
                this.timer.Start();
            }
            if (setEnabledButton != null)
                setEnabledButton(false);
            this.lvMain.ShowFromPath(this.listBack.Peek());
        }

        public void menuItemDelete_Click(object sender, EventArgs e)
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

        private void menuItemNew_Click(object sender, EventArgs e)
        {
            subMenuItemNewFolder.Image = BLL.ShellIcon.GetSmallFolderIcon().ToBitmap();
            subMenuItemNewTextDocument.Image = BLL.ShellIcon.GetSmallIconFromExtension(".txt").ToBitmap();
        }

        private void subMenuItemNewFolder_Click(object sender, EventArgs e)
        {
            string newFolderName;
            string newFolderPath;

            int i = 0;
            do
            {
                if (i > 0)
                    newFolderName = "New Folder (" + i + ")";
                else
                    newFolderName = "New Folder";

                newFolderPath = Path.Combine(this.listBack.Peek(), newFolderName);

                i++;
            } while (Directory.Exists(newFolderPath));

            Directory.CreateDirectory(newFolderPath);

            BLL.ClassBLL.Instances.showDirectory(Microsoft.VisualBasic.FileIO.FileSystem.GetDirectoryInfo(newFolderPath), lvMain);

            lvMain.Items[newFolderName].BeginEdit();

        }

        private void subMenuItemNewTextDocument_Click(object sender, EventArgs e)
        {
            string newFileName;
            string newFilePath;

            int i = 0;
            do
            {
                if (i > 0)
                    newFileName = "New Text Document (" + i + ").txt";
                else
                    newFileName = "New Text Document" + ".txt";

                newFilePath = Path.Combine(this.listBack.Peek(), newFileName);

                i++;
            } while (File.Exists(newFilePath));


            using (FileStream fs = File.Create(newFilePath)) { };

            BLL.ClassBLL.Instances.showFile(Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(newFilePath), lvMain);

            lvMain.Items[newFileName].BeginEdit();
        }

        private void menuItemProperties_Click(object sender, EventArgs e)
        {
            if (this.lvMain.SelectedItems.Count > 0)
                BLL.FilePropertiesDialog.ShowFileProperties(this.lvMain.SelectedItems[0].Tag.ToString());
            else
                BLL.FilePropertiesDialog.ShowFileProperties(this.cbPath.Text);
        }

        #endregion
        //==============================================================================================================
        //================================================Combobox event================================================
        #region ComboboxEvent

        public void cbPath_Properties_QueryCloseUp(object sender, CancelEventArgs e)
        {


            if (cbPath.Text.Equals(listBack.Peek()))//Nếu không thay đổi đường dẫn thì không làm gì
                return;
            try
            {
                this.listBack.Push(cbPath.Text);

                lvMain.Clear();
                if (setEnabledButton != null)
                    setEnabledButton(false);
                this.lvMain.ShowFromPath(this.listBack.Peek());
            }
            catch (Exception ex)
            {
                btnBack_ItemClick(null, null);
                MessageBox.Show("The path is not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                menuItemNew.Enabled = false;
            else
                menuItemNew.Enabled = true;
        }

        #endregion
        //==============================================================================================================
        //===================================================ListView===================================================
        #region ListView Event

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

                cbPath.Text = path;

                cbPath.Properties.Items.Add(path);

                this.listBack.Push(path);

                this.Cursor = Cursors.AppStarting;

                if (setEnabledButton != null)
                    setEnabledButton(false);
                this.lvMain.ShowFromPath(listBack.Peek());

                this.Cursor = Cursors.Arrow;

                btnBack.Enabled = true;//Bật back

                btnForward.Enabled = false;//Tắt forward

                this.listForward.Clear();//Xóa ngăn xếp forward

                this.tvMain.RefreshNavigationPane(path);

            }
            catch (Exception ex)
            { }
        }

        private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.listBack.Peek().Equals("This PC") && setEnabledButton != null)
            {
                if (lvMain.SelectedItems.Count > 0)
                    setEnabledButton(true);
                else
                    setEnabledButton(false);
            }

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

                if (listPath.Count > 1)
                {
                    //Nếu có nhiều hơn 1 item thì hiển thị MessBox chung cho các items được xóa
                    if (MessageBox.Show("Are you sure you want to permanetly delete these " + listPath.Count + " items?", "Delete Multipe Items", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes
                        && BLL.ClassBLL.Instances.deletePermanently(listPath, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs))
                    {
                        foreach (ListViewItem item in lvMain.SelectedItems)
                            lvMain.Items.RemoveByKey(item.Name);
                    }
                }
                else
                {
                    //Nếu chỉ có một item thì hiển thị UI của nó
                    if (BLL.ClassBLL.Instances.deletePermanently(listPath, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs))
                    {
                        foreach (ListViewItem item in lvMain.SelectedItems)
                            lvMain.Items.RemoveByKey(item.Name);
                    }
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (lvMain.SelectedItems.Count > 0)
                {
                    ListViewItem item = lvMain.SelectedItems[0];//Lấy Item đang được chọn

                    this.oldNameItem = item.Name;

                    item.BeginEdit();
                }
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in lvMain.Items)
                    item.Selected = true;
            }
        }

        private void lvMain_Enter(object sender, EventArgs e)
        {
            if (getCopyPath != null && getCopyPath().Count > 0)
                menuItemPaste.Enabled = true;
            else
                menuItemPaste.Enabled = false;
        }


        #endregion
        //==============================================================================================================
        //===================================================DragDrop===================================================
        #region DragDrop

        private void lvMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lvMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //Danh sách chứa các file cần copy
                List<string> listPathCopy = new List<string>();

                //Lấy danh sách các item dropped
                string[] listItem = (string[])e.Data.GetData(DataFormats.FileDrop, false);

                foreach (string item in listItem)
                {
                    //Nếu Drop trong cùng một chỗ thì dừng lại
                    if (item.Remove(item.LastIndexOf('\\')).Equals(this.listBack.Peek()))
                        return;

                    //Lấy tên file
                    string fileName = item.Substring(item.LastIndexOf('\\') + 1);

                    //Nếu fileName null hoặc rỗng thì dừng
                    if (string.IsNullOrEmpty(fileName))
                        return;

                    //thêm path của item này vào listPathCopy
                    listPathCopy.Add(item);
                }

                if (CopyAction != null)
                {
                    //Gọi hàm copy các file trong danh sách
                    CopyAction(listPathCopy, true);
                }

                if (PasteAction != null)
                {
                    if (!Directory.Exists(cbPath.Text))//Nếu pastePath không tồn tại thì thoát
                        return;

                    //Gọi hàm paste vào folder đích
                    this.task = Task.Run(() => PasteAction(cbPath.Text));

                    //Khởi động bồ timer để kiểm tra khi tiến trình dừng
                    this.timer.Start();
                }
            }
            catch (Exception ex) { }
        }


        private void lvMain_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //Danh sách đường dẫn của các item cần Drag
            List<string> list = new List<string>();

            foreach (ListViewItem item in lvMain.SelectedItems)
            {
                list.Add(item.Tag.ToString());
            }

            //Chuyển đổi danh sách list trên thành mảng string
            var Items = list.Cast<string>().ToArray();

            //Tạo một đối tượng DataObject nhận giữ liệu của mảng đường dẫn trên
            DataObject data = new DataObject(DataFormats.FileDrop, Items);

            data.SetData(Items);

            lvMain.DoDragDrop(data, DragDropEffects.Copy);

        }

        #endregion
        //==============================================================================================================
        //================================================TreeView Event================================================
        #region TreeView Event

        private void tvMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string path = e.Node.Tag.ToString();

                if (path.Equals(cbPath.Text))
                    return;

                this.listBack.Push(path);

                lvMain.Clear();

                this.Cursor = Cursors.AppStarting;

                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());

                this.Cursor = Cursors.Arrow;

                cbPath.Text = path;

                cbPath.Properties.Items.Add(path);

                btnBack.Enabled = true;

                btnForward.Enabled = false;//Tắt forward

                this.listForward.Clear();//Xóa ngăn xếp forward

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
                this.tvMain.AddDirectory(e.Node);
            }
        }
        #endregion
        //==============================================================================================================
        //=====================================================Rename===================================================
        #region Rename

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

                        string extension = "";

                        if (File.Exists(oldPath))
                            extension = item.Tag.ToString().Substring(item.Tag.ToString().LastIndexOf('.'));

                        string newPath = Path.Combine(oldPath.Remove(oldPath.LastIndexOf('\\') + 1), e.Label) + extension;

                        if (e.Label != item.Text && (Directory.Exists(newPath) || File.Exists(newPath)))
                        {
                            MessageBox.Show("The folder name already exists! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            e.CancelEdit = true;

                            lvMain.Items[e.Item].BeginEdit();

                            return;
                        }

                        if (Directory.Exists(oldPath))
                            Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(item.Tag.ToString(), e.Label);
                        else
                            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(item.Tag.ToString(), e.Label + extension);

                        item.Tag = newPath;
                    }
                }
            }
            catch (Exception ex) { e.CancelEdit = true; }
        }

        private void lvMain_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (lvMain.SelectedItems.Count > 0)
            {
                ListViewItem item = lvMain.SelectedItems[0];//Lấy Item đang được chọn

                this.oldNameItem = item.Name;
            }
        }

        #endregion
        //==============================================================================================================
        //=====================================================View=====================================================
        #region View

        private void chkNavigationPane_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splitUserControl.Panel1Collapsed = (chkNavigationPane.Checked) ? false : true;
        }

        private void btnViewLarge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnViewLarge.Checked)
            {
                this.lvMain.View = View.LargeIcon;

                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());
                btnViewDetail.Checked = btnViewSmall.Checked = btnViewList.Checked = btnViewTiles.Checked = false;
            }
            else
                btnViewLarge.Checked = true;
        }

        private void btnViewSmall_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnViewSmall.Checked)
            {
                this.lvMain.View = View.SmallIcon;
                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());
                btnViewDetail.Checked = btnViewLarge.Checked = btnViewList.Checked = btnViewTiles.Checked = false;
            }
            else
                btnViewSmall.Checked = true;
        }

        private void btnViewList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnViewList.Checked)
            {
                this.lvMain.View = View.List;
                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());
                btnViewDetail.Checked = btnViewLarge.Checked = btnViewSmall.Checked = btnViewTiles.Checked = false;
            }
            else
                btnViewList.Checked = true;
        }

        private void btnViewDetail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnViewDetail.Checked)
            {
                this.lvMain.View = View.Details;

                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());
                btnViewList.Checked = btnViewLarge.Checked = btnViewSmall.Checked = btnViewTiles.Checked = false;
            }
            else
                btnViewDetail.Checked = true;
        }

        private void btnViewTiles_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (btnViewTiles.Checked)
            {
                this.lvMain.View = View.Tile;
                if (setEnabledButton != null)
                    setEnabledButton(false);

                this.lvMain.ShowFromPath(this.listBack.Peek());
                btnViewList.Checked = btnViewLarge.Checked = btnViewSmall.Checked = btnViewDetail.Checked = false;
            }
            else
                btnViewTiles.Checked = true;
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.task.IsCompleted)
            {
                //Gọi hàm refresh lại tất cả các giao diện ở form1
                if (getRefreshAll != null)
                    getRefreshAll();

                //Dừng bộ đếm khi đã kết thúc tiến trình
                this.timer.Stop();
            }
        }

        private void tvMain_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void tvMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MessageBox.Show(this.tvMain.SelectedNode.Tag.ToString());
        }


       

       

    }
}
