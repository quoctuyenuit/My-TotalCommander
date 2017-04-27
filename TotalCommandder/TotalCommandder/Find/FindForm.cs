using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder.Find
{
    public partial class FindForm : Form
    {
        private CancellationTokenSource cancelTask;
        private Task task;
        public string pathCurrent { get; set; }

        public List<string> listResultPath { get; set; }

        public FindForm(string pathCurrent)
        {
            InitializeComponent();
            this.cbSearchIn.Text = pathCurrent;
            this.txtStatus.Hide();
            this.pathCurrent = pathCurrent;

            List<string> listDisk = new List<string>();

            DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                listDisk.Add(drive.Name);
            }
            cbSearchIn.Properties.Items.AddRange(listDisk);
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            this.txtSearchFor.Focus();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                cbSearchIn.Text = dialog.SelectedPath;
        }

        private void chkSearchInAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearchInAll.Checked)
                cbSearchIn.Text = "This PC";
            else
                cbSearchIn.Text = this.pathCurrent;
        }

        private void btnStartSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchFor.Text))
            {
                MessageBox.Show("Search for is emptying", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.txtStatus.Show();

            this.Cursor = Cursors.AppStarting;

            cancelTask = new CancellationTokenSource();

            CancellationToken ct = cancelTask.Token;

            this.task = Task.Factory.StartNew(x =>
            {
                if (cbSearchIn.Text.Equals("This PC"))
                {
                    DriveInfo[] drives = DriveInfo.GetDrives();

                    List<string> list = new List<string>();

                    foreach (DriveInfo d in drives)
                        list.AddRange(BLL.ClassBLL.Instances.findAction(d.Name, txtSearchFor.Text));

                    this.listResultPath.AddRange(listResultPath);

                    listResultPath.Sort(new ComparerSort());

                }
                else
                {
                    this.listResultPath = BLL.ClassBLL.Instances.findAction(cbSearchIn.Text, txtSearchFor.Text);

                    listResultPath.Sort(new ComparerSort());
                }
            }, cancelTask);

            timer1.Start();
        }

        private class ComparerSort : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (!string.IsNullOrEmpty(x) && x.Contains('\\'))
                    x = x.Substring(x.LastIndexOf('\\') + 1);

                if (!string.IsNullOrEmpty(y) && y.Contains('\\'))
                    y = y.Substring(y.LastIndexOf('\\') + 1);

                if (x.Length < y.Length)
                    return -1;
                else
                    return 1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (cancelTask != null)
                cancelTask.Cancel();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.task.IsCompleted)
            {
                this.txtStatus.Hide();
                this.timer1.Stop();
                this.Cursor = Cursors.Arrow;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
    }
}
