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

namespace TotalCommandder.Packing
{
    public partial class PackingForm : Form
    {
        private string Path;
        public PackingForm()
        {
            InitializeComponent();
        }

        public string filePath { get; set; }

        public bool isSuccessful { get; set; }//Biến thông báo cho bên ngoài biết thành đóng gói thành công hay bất bại
        
        private void addItemListBox(string[] folders, string[] files)
        {
            foreach (string folder in folders)
            {
                DirectoryInfo newDirectoryInfo = new DirectoryInfo(folder);

                //Chỉ thêm những folder không ẩn vào listBox
                if ((newDirectoryInfo.Attributes | FileAttributes.Hidden) != newDirectoryInfo.Attributes)
                    lbFoldersAndFiles.Items.Add(folder);
            }

            foreach (string file in files)
            {
                FileInfo newFileInfo = new FileInfo(file);

                //Chỉ thêm những file không ẩn vào listBox
                if ((newFileInfo.Attributes | FileAttributes.Hidden) != newFileInfo.Attributes)
                    lbFoldersAndFiles.Items.Add(file);
            }
        }

        #region Button Events

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialogFolder = new FolderBrowserDialog();
            if (dialogFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialogFolder.SelectedPath;;

                txtFolderPath.Text = path;

                //Lấy đường dẫn tới thư mục chứa thư mục mình cần giải nén
                this.Path = path.Remove(path.LastIndexOf('\\'));

                //Lấy tên theo tên của thư mục
                this.txtFileName.Text = path.Substring(path.LastIndexOf('\\') + 1);

                string[] directories = Directory.GetDirectories(txtFolderPath.Text);
                string[] files = Directory.GetFiles(txtFolderPath.Text);

                lbFoldersAndFiles.Items.Clear();

                addItemListBox(directories, files);

                if (lbFoldersAndFiles.Items.Count > 0)
                {
                    btnRemove.Enabled = true;
                    btnOpen.Enabled = true;
                    btnEdit.Enabled = true;
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialogFolder = new FolderBrowserDialog();
            if (dialogFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!lbFoldersAndFiles.Items.Contains(dialogFolder.SelectedPath))
                    lbFoldersAndFiles.Items.Add(dialogFolder.SelectedPath);

                lbFoldersAndFiles.SelectedItem = lbFoldersAndFiles.Items[lbFoldersAndFiles.Items.Count - 1];


                btnRemove.Enabled = true;
                btnOpen.Enabled = true;
                btnEdit.Enabled = true;
            }
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogFile = new OpenFileDialog();
            if (dialogFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!lbFoldersAndFiles.Items.Contains(dialogFile.FileName))
                    lbFoldersAndFiles.Items.Add(dialogFile.FileName);

                lbFoldersAndFiles.SelectedItem = lbFoldersAndFiles.Items[lbFoldersAndFiles.Items.Count - 1];

                btnRemove.Enabled = true;
                btnOpen.Enabled = true;
                btnEdit.Enabled = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            lbFoldersAndFiles.Items.Remove(lbFoldersAndFiles.SelectedItem);
            if (lbFoldersAndFiles.SelectedItem == null)
            {
                btnRemove.Enabled = false;
                btnOpen.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DirectoryInfo direcInfo = new DirectoryInfo(lbFoldersAndFiles.SelectedItem.ToString());


            //Nếu là Folder thì mở Dialog foder ngược lại thì mở OpenFileDialog
            if ((direcInfo.Attributes | FileAttributes.Directory) == direcInfo.Attributes)
            {
                FolderBrowserDialog dialogFolder = new FolderBrowserDialog();
                if (dialogFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!lbFoldersAndFiles.Items.Contains(dialogFolder.SelectedPath))
                        lbFoldersAndFiles.Items.Add(dialogFolder.SelectedPath);

                    btnRemove.Enabled = true;
                    btnOpen.Enabled = true;
                    btnEdit.Enabled = true;
                }
                else
                    return;
            }
            else
            {
                OpenFileDialog dialogFile = new OpenFileDialog();
                if (dialogFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!lbFoldersAndFiles.Items.Contains(dialogFile.FileName))
                        lbFoldersAndFiles.Items.Add(dialogFile.FileName);

                   

                    btnRemove.Enabled = true;
                    btnOpen.Enabled = true;
                    btnEdit.Enabled = true;
                }
                else
                    return;
            }

            lbFoldersAndFiles.Items.Remove(lbFoldersAndFiles.SelectedItem);

            lbFoldersAndFiles.SelectedItem = lbFoldersAndFiles.Items[lbFoldersAndFiles.Items.Count - 1];
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((string)lbFoldersAndFiles.SelectedItem);
            }
            catch (Exception ex) { }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Nếu textBox hoặc listBox rỗng thì thông báo và không đóng gói
            if (lbFoldersAndFiles.Items.Count == 0 || string.IsNullOrEmpty(txtFolderPath.Text))
            {
                MessageBox.Show("The folder need pack is null! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                this.isSuccessful = false;

                DialogResult = System.Windows.Forms.DialogResult.Ignore;

                return;
            }

            if (File.Exists(System.IO.Path.Combine(this.Path, txtFileName.Text) + ".zip"))
            {
                MessageBox.Show("The Folder Name already exists! Please try again with new folder name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                txtFileName.Focus();

                DialogResult = System.Windows.Forms.DialogResult.Ignore;

                return;
            }

            //Lấy danh sách các đường dẫn có trong ListBox
            List<string> listPath = new List<string>();

            foreach (string path in lbFoldersAndFiles.Items)
                listPath.Add(path);

            if (BLL.ClassBLL.Instances.packFile(listPath, System.IO.Path.Combine(this.Path, txtFileName.Text) + ".zip"))
            {
                this.filePath = System.IO.Path.Combine(this.Path, txtFileName.Text) + ".zip";

                MessageBox.Show("Pack folder successful!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.isSuccessful = true;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Pack folder failure! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                this.isSuccessful = false;

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region ListBox Events

        private void lbFoldersAndFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string path = (string)lbFoldersAndFiles.SelectedItem;

                    System.Diagnostics.Process.Start(path);
                }
                catch (Exception ex) { }
            }
        }

        private void lbFoldersAndFiles_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string path = (string)lbFoldersAndFiles.SelectedItem;
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex) { }
        }

        #endregion

        private void PackingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Ignore)
                e.Cancel = true;
        }

    }
}
