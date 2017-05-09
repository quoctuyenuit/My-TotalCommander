namespace TotalCommandder.Find
{
    partial class FindForm
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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSearchFor = new DevExpress.XtraEditors.TextEdit();
            this.btnStartSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cbSearchIn = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkSearchInAll = new DevExpress.XtraEditors.CheckEdit();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtStatus = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbSearchIn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSearchInAll.Properties)).BeginInit();
            this.txtStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(17, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Search for:";
            // 
            // txtSearchFor
            // 
            this.txtSearchFor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchFor.Location = new System.Drawing.Point(106, 10);
            this.txtSearchFor.Name = "txtSearchFor";
            this.txtSearchFor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSearchFor.Properties.Appearance.Options.UseFont = true;
            this.txtSearchFor.Size = new System.Drawing.Size(418, 24);
            this.txtSearchFor.TabIndex = 0;
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartSearch.Location = new System.Drawing.Point(363, 126);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.Size = new System.Drawing.Size(94, 34);
            this.btnStartSearch.TabIndex = 0;
            this.btnStartSearch.Text = "Start search";
            this.btnStartSearch.Click += new System.EventHandler(this.btnStartSearch_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Location = new System.Drawing.Point(17, 43);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(64, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Search in:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(463, 40);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(61, 24);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = ">>";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(463, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 34);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.cbSearchIn);
            this.panelControl1.Controls.Add(this.chkSearchInAll);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtSearchFor);
            this.panelControl1.Controls.Add(this.btnBrowse);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(545, 108);
            this.panelControl1.TabIndex = 4;
            // 
            // cbSearchIn
            // 
            this.cbSearchIn.Location = new System.Drawing.Point(106, 39);
            this.cbSearchIn.Name = "cbSearchIn";
            this.cbSearchIn.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cbSearchIn.Properties.Appearance.Options.UseFont = true;
            this.cbSearchIn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSearchIn.Size = new System.Drawing.Size(351, 24);
            this.cbSearchIn.TabIndex = 3;
            // 
            // chkSearchInAll
            // 
            this.chkSearchInAll.Location = new System.Drawing.Point(106, 70);
            this.chkSearchInAll.Name = "chkSearchInAll";
            this.chkSearchInAll.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkSearchInAll.Properties.Appearance.Options.UseFont = true;
            this.chkSearchInAll.Properties.Caption = "Search in all Local Disk";
            this.chkSearchInAll.Size = new System.Drawing.Size(197, 22);
            this.chkSearchInAll.TabIndex = 2;
            this.chkSearchInAll.CheckedChanged += new System.EventHandler(this.chkSearchInAll_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtStatus
            // 
            this.txtStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.txtStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.txtStatus.Location = new System.Drawing.Point(0, 163);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(569, 25);
            this.txtStatus.TabIndex = 7;
            this.txtStatus.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(262, 20);
            this.toolStripStatusLabel1.Text = "Finding... Click Cancel button to cancel";
            // 
            // FindForm
            // 
            this.AcceptButton = this.btnStartSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(569, 188);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStartSearch);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(587, 235);
            this.MinimumSize = new System.Drawing.Size(587, 235);
            this.Name = "FindForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FindForm";
            this.Load += new System.EventHandler(this.FindForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchFor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbSearchIn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSearchInAll.Properties)).EndInit();
            this.txtStatus.ResumeLayout(false);
            this.txtStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSearchFor;
        private DevExpress.XtraEditors.SimpleButton btnStartSearch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnBrowse;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit chkSearchInAll;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip txtStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraEditors.ComboBoxEdit cbSearchIn;
    }
}