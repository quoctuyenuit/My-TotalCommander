namespace TotalCommandder.Mail
{
    partial class SendMailForm
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
            this.txtMailFrom = new DevExpress.XtraEditors.TextEdit();
            this.txtBodyMail = new System.Windows.Forms.RichTextBox();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.informationGroup = new System.Windows.Forms.GroupBox();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtMailTo = new DevExpress.XtraEditors.TextEdit();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.txtCc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkAttachFIle = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.lvAttachFile = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtMailFrom.Properties)).BeginInit();
            this.informationGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCc.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(17, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mail from:";
            // 
            // txtMailFrom
            // 
            this.txtMailFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailFrom.Location = new System.Drawing.Point(112, 28);
            this.txtMailFrom.Name = "txtMailFrom";
            this.txtMailFrom.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailFrom.Properties.Appearance.Options.UseFont = true;
            this.txtMailFrom.Size = new System.Drawing.Size(312, 24);
            this.txtMailFrom.TabIndex = 1;
            // 
            // txtBodyMail
            // 
            this.txtBodyMail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBodyMail.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBodyMail.Location = new System.Drawing.Point(17, 33);
            this.txtBodyMail.Name = "txtBodyMail";
            this.txtBodyMail.Size = new System.Drawing.Size(407, 165);
            this.txtBodyMail.TabIndex = 2;
            this.txtBodyMail.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(305, 600);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(83, 36);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // informationGroup
            // 
            this.informationGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.informationGroup.Controls.Add(this.txtMailFrom);
            this.informationGroup.Controls.Add(this.txtPassword);
            this.informationGroup.Controls.Add(this.txtMailTo);
            this.informationGroup.Controls.Add(this.txtSubject);
            this.informationGroup.Controls.Add(this.txtCc);
            this.informationGroup.Controls.Add(this.labelControl5);
            this.informationGroup.Controls.Add(this.labelControl4);
            this.informationGroup.Controls.Add(this.labelControl3);
            this.informationGroup.Controls.Add(this.labelControl2);
            this.informationGroup.Controls.Add(this.labelControl1);
            this.informationGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.informationGroup.Location = new System.Drawing.Point(21, 21);
            this.informationGroup.Name = "informationGroup";
            this.informationGroup.Size = new System.Drawing.Size(456, 243);
            this.informationGroup.TabIndex = 4;
            this.informationGroup.TabStop = false;
            this.informationGroup.Text = "Information";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(112, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Size = new System.Drawing.Size(312, 24);
            this.txtPassword.TabIndex = 1;
            // 
            // txtMailTo
            // 
            this.txtMailTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailTo.Location = new System.Drawing.Point(112, 112);
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailTo.Properties.Appearance.Options.UseFont = true;
            this.txtMailTo.Size = new System.Drawing.Size(312, 24);
            this.txtMailTo.TabIndex = 1;
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(112, 156);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Properties.Appearance.Options.UseFont = true;
            this.txtSubject.Size = new System.Drawing.Size(312, 24);
            this.txtSubject.TabIndex = 1;
            // 
            // txtCc
            // 
            this.txtCc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCc.Location = new System.Drawing.Point(112, 199);
            this.txtCc.Name = "txtCc";
            this.txtCc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCc.Properties.Appearance.Options.UseFont = true;
            this.txtCc.Size = new System.Drawing.Size(312, 24);
            this.txtCc.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl5.Location = new System.Drawing.Point(17, 202);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(21, 18);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Cc:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl4.Location = new System.Drawing.Point(17, 159);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(53, 18);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Subject:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Location = new System.Drawing.Point(17, 115);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(47, 18);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Mail to:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Location = new System.Drawing.Point(17, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 18);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Password:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.linkAttachFIle);
            this.groupBox2.Controls.Add(this.lvAttachFile);
            this.groupBox2.Controls.Add(this.txtBodyMail);
            this.groupBox2.Location = new System.Drawing.Point(21, 270);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 324);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Content";
            // 
            // linkAttachFIle
            // 
            this.linkAttachFIle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkAttachFIle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkAttachFIle.Location = new System.Drawing.Point(17, 204);
            this.linkAttachFIle.Name = "linkAttachFIle";
            this.linkAttachFIle.Size = new System.Drawing.Size(62, 16);
            this.linkAttachFIle.TabIndex = 5;
            this.linkAttachFIle.Text = "Attach file:";
            this.linkAttachFIle.Click += new System.EventHandler(this.linkAttachFIle_Click);
            // 
            // lvAttachFile
            // 
            this.lvAttachFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAttachFile.LargeImageList = this.imageList1;
            this.lvAttachFile.Location = new System.Drawing.Point(17, 226);
            this.lvAttachFile.MultiSelect = false;
            this.lvAttachFile.Name = "lvAttachFile";
            this.lvAttachFile.Size = new System.Drawing.Size(407, 82);
            this.lvAttachFile.SmallImageList = this.imageList1;
            this.lvAttachFile.TabIndex = 4;
            this.lvAttachFile.UseCompatibleStateImageBehavior = false;
            this.lvAttachFile.View = System.Windows.Forms.View.Details;
            this.lvAttachFile.Click += new System.EventHandler(this.lvAttachFile_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(394, 600);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 36);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 646);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(501, 25);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(155, 20);
            this.toolStripStatusLabel1.Text = "Click to file to remove";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // SendMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 671);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.informationGroup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Name = "SendMailForm";
            this.Text = "SendMailForm";
            ((System.ComponentModel.ISupportInitialize)(this.txtMailFrom.Properties)).EndInit();
            this.informationGroup.ResumeLayout(false);
            this.informationGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMailTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCc.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMailFrom;
        private System.Windows.Forms.RichTextBox txtBodyMail;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private System.Windows.Forms.GroupBox informationGroup;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCc;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.TextEdit txtMailTo;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.HyperlinkLabelControl linkAttachFIle;
        private System.Windows.Forms.ListView lvAttachFile;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}