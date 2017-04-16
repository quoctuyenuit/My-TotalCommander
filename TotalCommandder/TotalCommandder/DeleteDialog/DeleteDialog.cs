using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder.DeleteDialog
{
    public partial class DeleteDialog : Form
    {
        public DeleteDialog(string path)
        {
            InitializeComponent();
            if (System.IO.Directory.Exists(path))
            {
                System.IO.DirectoryInfo direc = new System.IO.DirectoryInfo(path);
                txtName.Text = direc.Name;
                txtType.Hide();
                txtSize.Hide();
                labelType.Hide();
                labelSize.Hide();
                txtDateModified.Text = direc.LastAccessTime.ToString();
                pic.Image = BLL.ShellIcon.GetLargeFolderIcon().ToBitmap();
            }
            else
            {
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                txtName.Text = file.Name;
                txtType.Text = file.Extension.ToString() + " File";
                txtDateModified.Text = file.LastAccessTime.ToString();

                double size = file.Length;

                string[] unit = { " KB", " MB", " GB", " TB" };

                string u = unit[0];

                int i = 0;

                while (size > 1000)
                {
                    size = size / Math.Pow(2, 10);
                    u = unit[i++];
                }

                txtSize.Text = Math.Round(size).ToString()  + u;

                pic.Image = BLL.ShellIcon.GetLargeIconFromExtension(file.FullName).ToBitmap();
            }
        }


        private void DeleteDialog_Load(object sender, EventArgs e)
        {
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
