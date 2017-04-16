namespace TotalCommandder.Packing
{
    partial class UnPacking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSourcePath = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowseSource = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnStart = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDestinationPath = new DevExpress.XtraEditors.TextEdit();
            this.txtFolderName = new DevExpress.XtraEditors.TextEdit();
            this.btnBrowseDestination = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourcePath.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestinationPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtSourcePath);
            this.groupBox1.Controls.Add(this.btnBrowseSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 68);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sources";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourcePath.Location = new System.Drawing.Point(14, 31);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSourcePath.Properties.Appearance.Options.UseFont = true;
            this.txtSourcePath.Properties.ReadOnly = true;
            this.txtSourcePath.Size = new System.Drawing.Size(624, 24);
            this.txtSourcePath.TabIndex = 0;
            // 
            // btnBrowseSource
            // 
            this.btnBrowseSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSource.Location = new System.Drawing.Point(655, 27);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(103, 29);
            this.btnBrowseSource.TabIndex = 2;
            this.btnBrowseSource.Text = "Browse...";
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(667, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStart.Location = new System.Drawing.Point(558, 185);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(103, 29);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Controls.Add(this.txtDestinationPath);
            this.groupBox2.Controls.Add(this.txtFolderName);
            this.groupBox2.Controls.Add(this.btnBrowseDestination);
            this.groupBox2.Location = new System.Drawing.Point(12, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 93);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Path";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(420, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(73, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Folder Name";
            // 
            // txtDestinationPath
            // 
            this.txtDestinationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestinationPath.Location = new System.Drawing.Point(14, 47);
            this.txtDestinationPath.Name = "txtDestinationPath";
            this.txtDestinationPath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtDestinationPath.Properties.Appearance.Options.UseFont = true;
            this.txtDestinationPath.Size = new System.Drawing.Size(400, 24);
            this.txtDestinationPath.TabIndex = 0;
            // 
            // txtFolderName
            // 
            this.txtFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderName.Location = new System.Drawing.Point(420, 48);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtFolderName.Properties.Appearance.Options.UseFont = true;
            this.txtFolderName.Size = new System.Drawing.Size(218, 24);
            this.txtFolderName.TabIndex = 0;
            // 
            // btnBrowseDestination
            // 
            this.btnBrowseDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDestination.Location = new System.Drawing.Point(655, 43);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.Size = new System.Drawing.Size(103, 29);
            this.btnBrowseDestination.TabIndex = 2;
            this.btnBrowseDestination.Text = "Browse...";
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);
            // 
            // UnPacking
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 227);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnPacking";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UnPacking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnPacking_FormClosing);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSourcePath.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestinationPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtSourcePath;
        private DevExpress.XtraEditors.SimpleButton btnBrowseSource;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit txtDestinationPath;
        private DevExpress.XtraEditors.SimpleButton btnBrowseDestination;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtFolderName;
    }
}