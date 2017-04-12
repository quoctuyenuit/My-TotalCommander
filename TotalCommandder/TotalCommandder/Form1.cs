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
        }

        private string copyPath{get; set;}

        private bool isCopy{get; set;}

        private void Form1_Load(object sender, EventArgs e)
        {
            GUI.uc_DirectoryList uc1 =new GUI.uc_DirectoryList();
            uc1.Dock = DockStyle.Fill;
            uc1.getCopyAction = new GUI.uc_DirectoryList.DgetCopyAction(copyAction);
            uc1.getCopyPath = new GUI.uc_DirectoryList.DgetCopyPath(getCopyPath);
            uc1.getPasteAction = new GUI.uc_DirectoryList.DgetPasteAction(getPasteAction);
            uc1.getRefreshAction = new GUI.uc_DirectoryList.DgetRefreshAction(getRefreshAction1);
            splitMain.Panel1.Controls.Add(uc1);
            //------------------------------------------------------------------------------------
            GUI.uc_DirectoryList uc2 = new GUI.uc_DirectoryList();
            uc2.Dock = DockStyle.Fill;
            uc2.getCopyAction = new GUI.uc_DirectoryList.DgetCopyAction(copyAction);
            uc2.getCopyPath = new GUI.uc_DirectoryList.DgetCopyPath(getCopyPath);
            uc2.getPasteAction = new GUI.uc_DirectoryList.DgetPasteAction(getPasteAction);
            uc2.getRefreshAction = new GUI.uc_DirectoryList.DgetRefreshAction(getRefreshAction2);
            splitMain.Panel2.Controls.Add(uc2);
        }

        private void copyAction(string path, bool isCopy)
        {
            this.isCopy = isCopy;
            this.copyPath = path;
        }

        public string getCopyPath()
        {
            return this.copyPath;
        }

        public void getPasteAction(string path)
        {
            BLL.ClassBLL.Instances.copyDirectory(this.copyPath, path);

            if (!isCopy)//Nếu là chức năng Cut thì xóa bản cũ
            {
                if (Directory.Exists(copyPath))
                    Directory.Delete(copyPath, true);
                else
                    File.Delete(copyPath);
            }
            if (this.refresh1 != null)
                this.refresh1();
            if (this.refresh2 != null)
                this.refresh2();
        }
    }
}
