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

        void getRefreshAction1(Drefresh refresh)
        {
            this.refresh1 = refresh;
        }

        void getRefreshAction2(Drefresh refresh)
        {
            this.refresh2 = refresh;
        }

        public Form1()
        {
            InitializeComponent();

            this.listCopyPath = new List<string>();
        }

        private List<string> listCopyPath{get; set;}

        private bool isCopy{get; set;}

        private void Form1_Load(object sender, EventArgs e)
        {
            GUI.uc_DirectoryList uc1 =new GUI.uc_DirectoryList(contextMenu);
            uc1.Dock = DockStyle.Fill;
            uc1.getCopyAction = new GUI.uc_DirectoryList.DgetCopyAction(copyAction);
            uc1.getCountCopyPath = new GUI.uc_DirectoryList.DgetCountCopyPath(getCountCopyPath);
            uc1.getPasteAction = new GUI.uc_DirectoryList.DgetPasteAction(getPasteAction);
            uc1.getRefreshAction = new GUI.uc_DirectoryList.DgetRefreshAction(getRefreshAction1);
            uc1.getContextMenu = new GUI.uc_DirectoryList.DgetContextMenu(pushContextMenu);
            uc1.pushContextMenuForParent = new GUI.uc_DirectoryList.DpushContextMenuForParent(setContextMenu);
            splitMain.Panel1.Controls.Add(uc1);
            //------------------------------------------------------------------------------------
            GUI.uc_DirectoryList uc2 = new GUI.uc_DirectoryList(contextMenu);
            uc2.Dock = DockStyle.Fill;
            uc2.getCopyAction = new GUI.uc_DirectoryList.DgetCopyAction(copyAction);
            uc2.getCountCopyPath = new GUI.uc_DirectoryList.DgetCountCopyPath(getCountCopyPath);
            uc2.getPasteAction = new GUI.uc_DirectoryList.DgetPasteAction(getPasteAction);
            uc2.getRefreshAction = new GUI.uc_DirectoryList.DgetRefreshAction(getRefreshAction2);
            uc2.getContextMenu = new GUI.uc_DirectoryList.DgetContextMenu(pushContextMenu);
            uc2.pushContextMenuForParent = new GUI.uc_DirectoryList.DpushContextMenuForParent(setContextMenu);
            splitMain.Panel2.Controls.Add(uc2);
        }

        private void copyAction(List<string> listPath, bool isCopy)
        {
            this.isCopy = isCopy;

            this.listCopyPath.Clear();

            this.listCopyPath = listPath;
        }

        public int getCountCopyPath()
        {
            return this.listCopyPath.Count;
        }

        public void getPasteAction(string pastePath)
        {
            foreach(string path in this.listCopyPath)
                BLL.ClassBLL.Instances.copyDirectory(path, pastePath);

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

        public ContextMenuStrip pushContextMenu()
        {
            setContextMenu();
            return this.contextMenu;
        }

        public void setContextMenu(ContextMenuStrip contextMenu)
        {
            this.contextMenu = contextMenu;
        }
    }
}
