using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private GUI.uc_DirectoryList gui1;

        private GUI.uc_DirectoryList gui2;
        private Task task;
        private List<string> listReviewFind;
        private bool isCopy;
        private List<string> listCopyPath;

        public Form1()
        {
            InitializeComponent();

            this.listCopyPath = new List<string>();

            //Set Skin for form
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "DevExpress Style";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gui1 = new GUI.uc_DirectoryList();
            gui1.Dock = DockStyle.Fill;
            gui1.CopyAction = new Action<List<string>, bool>(copyAction);
            gui1.getCopyPath = new GUI.uc_DirectoryList.DgetCopyPath(pushCopyPath);
            gui1.PasteAction = new Action<string>(pasteAction);
            gui1.getRefreshAll = new Action(refreshAll);
            gui1.setEnabledButton = new Action<bool>(setEnabledForSelectItem);
            splitMain.Panel1.Controls.Add(gui1);
            //------------------------------------------------------------------------------------
            this.gui2 = new GUI.uc_DirectoryList();
            gui2.Dock = DockStyle.Fill;
            gui2.CopyAction = new Action<List<string>, bool>(copyAction);
            gui2.getCopyPath = new GUI.uc_DirectoryList.DgetCopyPath(pushCopyPath);
            gui2.PasteAction = new Action<string>(pasteAction);
            gui2.getRefreshAll = new Action(refreshAll);
            gui2.setEnabledButton = new Action<bool>(setEnabledForSelectItem);
            splitMain.Panel2.Controls.Add(gui2);
        }

        public void refreshAll()
        {
            this.gui1.LvMain.ShowFromPath(this.gui1.ListBack.Peek());
            this.gui2.LvMain.ShowFromPath(this.gui2.ListBack.Peek());
        }

        #region Thao Tác với file

        private void copyAction(List<string> listPath, bool isCopy)
        {
            this.isCopy = isCopy;

            this.listCopyPath.Clear();

            this.listCopyPath = listPath;

        }

        //Đẩy đường dẫn cần copy(ListCopyPath) cho giao diện con
        public List<string> pushCopyPath()
        {
            return this.listCopyPath;
        }

        //Xử lý khi người dùng paste
        public void pasteAction(string pastePath)
        {
            if (isCopy)
            {
                foreach (string path in this.listCopyPath)
                    if (!BLL.ClassBLL.Instances.copyAction(path, pastePath))
                        return;
            }
            else
            {
                foreach (string path in this.listCopyPath)
                    if (!BLL.ClassBLL.Instances.moveAction(path, pastePath))
                        return;

                this.isCopy = true;

                this.listCopyPath.Clear();
            }
        }

        #endregion


        #region Packing Unpacking

        private void btnPack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Packing.PackingForm packingForm = new Packing.PackingForm();
            packingForm.ShowDialog();

            if (packingForm.isSuccessful)
            {

                string pathReturn = packingForm.filePath;

                this.gui1.CbPath.Text = pathReturn.Remove(pathReturn.LastIndexOf('\\'));

                #region LoadListView
                try
                {
                    this.gui1.ListBack.Push(this.gui1.CbPath.Text);

                    this.gui1.LvMain.Clear();

                    this.gui1.LvMain.ShowFromPath(this.gui1.ListBack.Peek());
                }
                catch (Exception ex)
                {
                    this.gui1.btnBack_ItemClick(null, null);
                    MessageBox.Show("The path is not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion

                this.gui1.LvMain.Focus();
                ListViewItem item = this.gui1.LvMain.Items[pathReturn.Substring(pathReturn.LastIndexOf('\\') + 1)];
                item.Selected = true;
                item.Focused = true;
            }
        }

        private void btnUnPack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Packing.UnPacking unpackingForm = new Packing.UnPacking();
            unpackingForm.ShowDialog();

            if (unpackingForm.isSuccessful)
            {

                string pathReturn = unpackingForm.pathFolder;

                this.gui1.CbPath.Text = pathReturn.Remove(pathReturn.LastIndexOf('\\'));

                #region LoadListView
                try
                {
                    this.gui1.ListBack.Push(this.gui1.CbPath.Text);

                    this.gui1.LvMain.Clear();

                    this.gui1.LvMain.ShowFromPath(this.gui1.ListBack.Peek());
                }
                catch (Exception ex)
                {
                    this.gui1.btnBack_ItemClick(null, null);
                    MessageBox.Show("The path is not exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                #endregion

                this.gui1.LvMain.Focus();
                ListViewItem item = this.gui1.LvMain.Items[pathReturn.Substring(pathReturn.LastIndexOf('\\') + 1)];
                item.Selected = true;
                item.Focused = true;
            }

        }
        #endregion

        #region View

        private void chkOneScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkOneScreen.Checked)
            {
                chkTwoScreen.Checked = false;
                splitMain.Panel2Collapsed = true;
            }
            else
                chkOneScreen.Checked = true;
        }

        private void chkTwoScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkTwoScreen.Checked)
            {
                chkOneScreen.Checked = false;
                splitMain.Panel2Collapsed = false;
            }
            else
                chkTwoScreen.Checked = true;
        }

        #endregion


        public void setEnabledForSelectItem(bool isSelected)
        {
            btnCopy.Enabled = btnCut.Enabled = btnDelete.Enabled = btnRename.Enabled = isSelected;
        }

        #region Button Click Event

        private void btnCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnPaste.Enabled = true;
            
            this.listCopyPath.Clear();

            if (this.gui1.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui1.LvMain.SelectedItems)
                    this.listCopyPath.Add(item.Tag.ToString());
            }
            else if (this.gui2.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui2.LvMain.SelectedItems)
                    this.listCopyPath.Add(item.Tag.ToString());
            }

            this.isCopy = true;
        }

        private void btnCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnPaste.Enabled = true;

            if (this.gui1.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui1.LvMain.SelectedItems)
                    this.listCopyPath.Add(item.Tag.ToString());
            }
            else if (this.gui2.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui2.LvMain.SelectedItems)
                    this.listCopyPath.Add(item.Tag.ToString());
            }

            this.isCopy = false;
        }

        private void btnPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnPaste.Enabled = false;

            string pathPaste = "";

            if (this.gui1.LvMain.Focused)
            {
                pathPaste = this.gui1.ListBack.Peek();
            }
            else if (this.gui2.LvMain.Focused)
            {
                pathPaste = this.gui2.ListBack.Peek();
            }
            else
            {
                MessageBox.Show("You must focus on one UI", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.task = Task.Run(() => pasteAction(pathPaste));
            timer.Start();

        }

        private void btnRecycleDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gui1.LvMain.Focused)
            {
                this.gui1.menuItemDelete_Click(null, null);
            }
            else if (this.gui2.LvMain.Focused)
            {
                this.gui2.menuItemDelete_Click(null, null);
            }
            else
            {
                MessageBox.Show("You must select some items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPermanentlyDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<string> listPath = new List<string>();

            ListView lvMain = null;

            if (this.gui1.LvMain.Focused)
                lvMain = this.gui1.LvMain;
            else if (this.gui2.LvMain.Focused)
                lvMain = this.gui2.LvMain;


            //Nếu không nhận được lvMain nào thì thoát ra
            if (lvMain == null)
            {
                MessageBox.Show("You must select some items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setEnabledForSelectItem(false);
                return;
            }
            
            foreach (ListViewItem item in lvMain.SelectedItems)
                listPath.Add(item.Tag.ToString());

            if (listPath.Count == 0)
            {
                MessageBox.Show("You must select some items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setEnabledForSelectItem(false);
                return;
            }
            else if (listPath.Count > 1)
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

        private void btnRename_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<string> listPath = new List<string>();

            ListView lvMain;

            if (this.gui1.LvMain.Focused)
            {
                lvMain = this.gui1.LvMain;
                if (lvMain.SelectedItems.Count > 0)
                {
                    ListViewItem item = lvMain.SelectedItems[0];//Lấy Item đang được chọn

                    this.gui1.oldNameItem = item.Text;

                    item.BeginEdit();
                }
            }
            else if (this.gui2.LvMain.Focused)
            {
                lvMain = this.gui2.LvMain;
                if (lvMain.SelectedItems.Count > 0)
                {
                    ListViewItem item = lvMain.SelectedItems[0];//Lấy Item đang được chọn

                    this.gui2.oldNameItem = item.Text;

                    item.BeginEdit();
                }
            }
            else
                MessageBox.Show("You must select some items", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void btnSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gui1.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui1.LvMain.Items)
                    item.Selected = true;
            }
            else if (this.gui2.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui2.LvMain.Items)
                    item.Selected = true;
            }
            else
                MessageBox.Show("You must focus on one display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnNoneSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gui1.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui1.LvMain.Items)
                    item.Selected = false;
            }
            else if (this.gui2.LvMain.Focused)
            {
                foreach (ListViewItem item in this.gui2.LvMain.Items)
                    item.Selected = false;
            }
            else
                MessageBox.Show("You must focus on one display", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.task.IsCompleted)
            {
                //Gọi hàm refresh lại tất cả các giao diện ở form1
                refreshAll();
                //Dừng bộ đếm khi đã kết thúc tiến trình
                this.timer.Stop();
            }
        }

        private void btnNotepad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("notepad.exe");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Soory! Can't open notepad now!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Find.FindForm findForm;

            if (this.gui1.LvMain.Focused)
            {
                string pathCurrent = this.gui1.ListBack.Peek();

                if (pathCurrent.Equals("This PC"))
                    pathCurrent = "C:\\";

                findForm = new Find.FindForm(pathCurrent);
            }
            else
            {
                string pathCurrent = this.gui2.ListBack.Peek();

                if (pathCurrent.Equals("This PC"))
                    pathCurrent = "C:\\";

                findForm = new Find.FindForm(pathCurrent);
            }

            if (findForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (findForm.listResultPath.Count > 0)
                {
                    this.gui1.CbPath.Text = "Search Results in " + findForm.pathCurrent;

                    BLL.ClassBLL.Instances.showDirectoryAndFiles(findForm.listResultPath, this.gui1.LvMain);

                    this.listReviewFind = findForm.listResultPath;

                    this.btnReviewFind.Enabled = true;
                }
                else
                    MessageBox.Show("No items match your search!", "Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReviewFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BLL.ClassBLL.Instances.showDirectoryAndFiles(this.listReviewFind, this.gui1.LvMain);
            this.gui1.CbPath.Text = "Search Results";
        }

        private void chkTwoScreen_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        

       

       

       

       

       

        

        
    }
}
