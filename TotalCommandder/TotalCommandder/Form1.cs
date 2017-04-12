using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TotalCommandder
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GUI.uc_DirectoryList uc1 =new GUI.uc_DirectoryList();
            uc1.Dock = DockStyle.Fill;
            splitMain.Panel1.Controls.Add(uc1);

            GUI.uc_DirectoryList uc2 = new GUI.uc_DirectoryList();
            uc2.Dock = DockStyle.Fill;
            splitMain.Panel2.Controls.Add(uc2);
        }
    }
}
