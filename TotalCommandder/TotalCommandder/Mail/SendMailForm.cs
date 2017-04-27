using DevExpress.DXCore.Controls.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder.Mail
{
    public partial class SendMailForm : Form
    {
        private List<string> listAttachFile;
        public SendMailForm()
        {
            InitializeComponent();
            listAttachFile = new List<string>();
            BLL.ClassBLL.Instances.showDirectoryAndFiles(listAttachFile, lvAttachFile);
        }

        private void linkAttachFIle_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!this.listAttachFile.Contains(dialog.FileName))
                    this.listAttachFile.Add(dialog.FileName);
                BLL.ClassBLL.Instances.showDirectoryAndFiles(listAttachFile, lvAttachFile);
            }
        }

        private void lvAttachFile_Click(object sender, EventArgs e)
        {
            this.listAttachFile.Remove(this.lvAttachFile.SelectedItems[0].Tag.ToString());
            this.lvAttachFile.Items.Remove(this.lvAttachFile.SelectedItems[0]);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            foreach (Control ctrl in informationGroup.Controls)
                if (ctrl is DevExpress.XtraEditors.TextEdit && ctrl.Name!= txtCc.Name && string.IsNullOrEmpty(ctrl.Text))
                {
                    errorProvider1.SetError(ctrl, "This item is required");
                    return;
                }

            if (BLL.ClassBLL.Instances.sendMail(txtMailFrom.Text, txtPassword.Text, txtMailTo.Text, txtSubject.Text, txtCc.Text, listAttachFile, txtBodyMail.Text))
                MessageBox.Show("Sent successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Sent failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
