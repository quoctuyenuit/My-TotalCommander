using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TotalCommandder
{
    public partial class NewFolderForm : Form
    {
        public NewFolderForm()
        {
            InitializeComponent();
            
        }

        public delegate void DgetAction(GUI.uc_DirectoryList.DgetFolderName getFolderName);
        public DgetAction getAction;

        public string getFolderName()
        {
            return txtFolderName.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (getAction != null)
                getAction(new GUI.uc_DirectoryList.DgetFolderName(getFolderName));
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
