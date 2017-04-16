using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder.Packing
{
    public partial class UnPacking : Form
    {
        public UnPacking()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogFile = new OpenFileDialog();
            if (dialogFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialogFile.FileName;
                txtSourcePath.Text = path;

                //Tách lấy tên của file cần giải nén
                string pathSubstring = path.Substring(path.LastIndexOf('\\') + 1);

                txtFolderName.Text = pathSubstring.Remove(pathSubstring.LastIndexOf('.'));

                txtDestinationPath.Text = path.Remove(path.LastIndexOf('\\'));
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialogFolder = new FolderBrowserDialog();
            if (dialogFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDestinationPath.Text = dialogFolder.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string destinationPath = System.IO.Path.Combine(txtDestinationPath.Text, txtFolderName.Text);

            if (string.IsNullOrEmpty(txtSourcePath.Text))
            {
                MessageBox.Show("The source path is null! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.isSuccessful = false;

                DialogResult = System.Windows.Forms.DialogResult.Ignore;

                return;
            }

            if (System.IO.Directory.Exists(destinationPath))
            {
                MessageBox.Show("The Folder Name already exists! Please try again with new folder name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                txtFolderName.Focus();

                DialogResult = System.Windows.Forms.DialogResult.Ignore;
               
                return;
            }

            if (BLL.ClassBLL.Instances.unpackFile(txtSourcePath.Text, destinationPath))
            {
                this.pathFolder = destinationPath;

                this.isSuccessful = true;

                DialogResult = System.Windows.Forms.DialogResult.OK;
                //if (MessageBox.Show("Unpack successful! Do you want to Open the new folder?", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                //{

                //    //string argument = "/select, \"" + destinationPath + "\"";

                //    //System.Diagnostics.Process.Start("explorer.exe", argument);
                //}
                //else
                //    return;
            }
            else
            {
                MessageBox.Show("Unpack was failure! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = System.Windows.Forms.DialogResult.OK;

                this.isSuccessful = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.Close();
        }

        public string pathFolder { get; set; }

        public bool isSuccessful { get; set; }

        private void UnPacking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Ignore)
                e.Cancel = true;
        }
    }
}
