namespace TotalCommandder.DeleteDialog
{
    partial class DeleteDialog
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.LabelControl();
            this.labelType = new DevExpress.XtraEditors.LabelControl();
            this.labelSize = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtType = new DevExpress.XtraEditors.LabelControl();
            this.txtSize = new DevExpress.XtraEditors.LabelControl();
            this.txtDateModified = new DevExpress.XtraEditors.LabelControl();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYes = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(44, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(358, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Are you sure you want to  permanently delete this file?";
            // 
            // txtName
            // 
            this.txtName.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtName.Location = new System.Drawing.Point(184, 58);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(81, 18);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "labelControl1";
            // 
            // labelType
            // 
            this.labelType.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelType.Location = new System.Drawing.Point(184, 82);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(39, 18);
            this.labelType.TabIndex = 0;
            this.labelType.Text = "Type:";
            this.labelType.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // labelSize
            // 
            this.labelSize.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelSize.Location = new System.Drawing.Point(184, 107);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(30, 18);
            this.labelSize.TabIndex = 0;
            this.labelSize.Text = "Size:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl6.Location = new System.Drawing.Point(184, 131);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(95, 18);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Date modified:";
            // 
            // txtType
            // 
            this.txtType.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtType.Location = new System.Drawing.Point(240, 82);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(39, 18);
            this.txtType.TabIndex = 0;
            this.txtType.Text = "Type:";
            this.txtType.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // txtSize
            // 
            this.txtSize.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSize.Location = new System.Drawing.Point(229, 106);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(25, 18);
            this.txtSize.TabIndex = 0;
            this.txtSize.Text = "Size";
            this.txtSize.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // txtDateModified
            // 
            this.txtDateModified.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtDateModified.Location = new System.Drawing.Point(298, 131);
            this.txtDateModified.Name = "txtDateModified";
            this.txtDateModified.Size = new System.Drawing.Size(90, 18);
            this.txtDateModified.TabIndex = 0;
            this.txtDateModified.Text = "Date modified";
            this.txtDateModified.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(65, 58);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(100, 114);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            // 
            // btnNo
            // 
            this.btnNo.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Location = new System.Drawing.Point(478, 181);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(99, 31);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "&No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(373, 181);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(99, 31);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "&Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // DeleteDialog
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(589, 224);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.txtDateModified);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelControl1);
            this.Name = "DeleteDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DeleteFile";
            this.Load += new System.EventHandler(this.DeleteDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl txtName;
        private DevExpress.XtraEditors.LabelControl labelType;
        private DevExpress.XtraEditors.LabelControl labelSize;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl txtType;
        private DevExpress.XtraEditors.LabelControl txtSize;
        private DevExpress.XtraEditors.LabelControl txtDateModified;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYes;
    }
}