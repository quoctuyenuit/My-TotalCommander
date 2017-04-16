using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TotalCommandder
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public delegate void Drefresh();

        public Drefresh refresh1;
        public Drefresh refresh2;

        //Lấy sự kiện refresh listview ở giao diện con 2
        void getRefreshAction1(Drefresh refresh)
        {
            this.refresh1 = refresh;
        }

        //Lấy sự kiện refresh listview ở giao diện con 1
        void getRefreshAction2(Drefresh refresh)
        {
            this.refresh2 = refresh;
        }

        private GUI.uc_DirectoryList gui1;

        private GUI.uc_DirectoryList gui2;

        public GUI.uc_DirectoryList Gui2
        {
            get { return gui2; }
            set { gui2 = value; }
        }

        public GUI.uc_DirectoryList Gui1
        {
            get { return gui1; }
            set { gui1 = value; }
        }

        public Form1()
        {
            InitializeComponent();

            this.listCopyPath = new List<string>();

            //Set Skin for form
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2010 Blue";

        }

        private List<string> listCopyPath { get; set; }

        private bool isCopy { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.gui1 = new GUI.uc_DirectoryList(contextMenu);
            gui1.Dock = DockStyle.Fill;
            gui1.getCopyAction = new GUI.uc_DirectoryList.DgetCopyAction(copyAction);
            gui1.getCopyPath = new GUI.uc_DirectoryList.DgetCopyPath(pushCopyPath);
            gui1.getPasteAction = new GUI.uc_DirectoryList.DgetPasteAction(pasteAction);
            gui1.getRefreshAction = new GUI.uc_DirectoryList.DgetRefreshAction(getRefreshAction1);
            gui1.getContextMenu = new GUI.uc_DirectoryList.DgetContextMenu(pushContextMenu);
            gui1.pushContextMenuForParent = new GUI.uc_DirectoryList.DpushContextMenuForParent(setContextMenu);
            splitMain.Panel1.Controls.Add(gui1);
            //------------------------------------------------------------------------------------
            this.gui2 = new GUI.uc_DirectoryList(contextMenu);
            gui2.Dock = DockStyle.Fill;
            gui2.getCopyAction = new GUI.uc_DirectoryList.DgetCopyAction(copyAction);
            gui2.getCopyPath = new GUI.uc_DirectoryList.DgetCopyPath(pushCopyPath);
            gui2.getPasteAction = new GUI.uc_DirectoryList.DgetPasteAction(pasteAction);
            gui2.getRefreshAction = new GUI.uc_DirectoryList.DgetRefreshAction(getRefreshAction2);
            gui2.getContextMenu = new GUI.uc_DirectoryList.DgetContextMenu(pushContextMenu);
            gui2.pushContextMenuForParent = new GUI.uc_DirectoryList.DpushContextMenuForParent(setContextMenu);
            splitMain.Panel2.Controls.Add(gui2);
        }

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
            foreach (string path in this.listCopyPath)
                if (!BLL.ClassBLL.Instances.copyDirectory(path, pastePath))
                    return;

            if (!isCopy)//Nếu là chức năng Cut thì xóa bản cũ
            {
                foreach (string path in this.listCopyPath)
                {
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                    else
                        File.Delete(path);
                }

                this.listCopyPath.Clear();
            }

            if (this.refresh1 != null)
                this.refresh1();
            if (this.refresh2 != null)
                this.refresh2();
        }

        private void setContextMenu()//Thay đổi giá trị item của contextMenu tương ứng với các item bên ngoài
        {
            this.contextMenu.Items[0].Enabled = menuItemOpen.Enabled;
            this.contextMenu.Items[1].Enabled = menuItemCopy.Enabled;
            this.contextMenu.Items[2].Enabled = menuItemCut.Enabled;
            this.contextMenu.Items[3].Enabled = menuItemPaste.Enabled;
        }

        //Đẩy contextMenu cho giao diện con
        public ContextMenuStrip pushContextMenu()
        {
            setContextMenu();
            return this.contextMenu;
        }

        //Lấy contextMenu từ giao diện con
        public void setContextMenu(ContextMenuStrip contextMenu)
        {
            this.contextMenu = contextMenu;
        }

        #region Button Events

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

                    this.gui1.showDirectoryAndFiles(this.gui1.CbPath.Text);
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

                    this.gui1.showDirectoryAndFiles(this.gui1.CbPath.Text);
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
    }
}
