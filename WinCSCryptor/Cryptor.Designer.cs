namespace WinCSCryptor
{
    partial class Encryptor
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Encryptor));
            this.labelFilename = new System.Windows.Forms.Label();
            this.BoxFilename = new System.Windows.Forms.TextBox();
            this.ButtonBrowse = new System.Windows.Forms.Button();
            this.ButtonEncrypt = new System.Windows.Forms.Button();
            this.BoxInfo = new System.Windows.Forms.RichTextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.BoxPassword = new System.Windows.Forms.TextBox();
            this.ButtonDecrypt = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.ButtonCreateKey = new System.Windows.Forms.Button();
            this.labelKey = new System.Windows.Forms.Label();
            this.ButtonExport = new System.Windows.Forms.Button();
            this.ButtonImport = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.OpenKeyFile = new System.Windows.Forms.OpenFileDialog();
            this.CheckBoxPrivate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelFilename
            // 
            this.labelFilename.AutoSize = true;
            this.labelFilename.Location = new System.Drawing.Point(13, 13);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(52, 13);
            this.labelFilename.TabIndex = 0;
            this.labelFilename.Text = "Filename:";
            // 
            // BoxFilename
            // 
            this.BoxFilename.Location = new System.Drawing.Point(16, 29);
            this.BoxFilename.Name = "BoxFilename";
            this.BoxFilename.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.BoxFilename.Size = new System.Drawing.Size(350, 20);
            this.BoxFilename.TabIndex = 1;
            this.BoxFilename.TextChanged += new System.EventHandler(this.BoxFilename_TextChanged);
            // 
            // ButtonBrowse
            // 
            this.ButtonBrowse.Location = new System.Drawing.Point(373, 27);
            this.ButtonBrowse.Name = "ButtonBrowse";
            this.ButtonBrowse.Size = new System.Drawing.Size(75, 23);
            this.ButtonBrowse.TabIndex = 2;
            this.ButtonBrowse.Text = "Browse";
            this.ButtonBrowse.UseVisualStyleBackColor = true;
            this.ButtonBrowse.Click += new System.EventHandler(this.ButtonBrowse_Click);
            // 
            // ButtonEncrypt
            // 
            this.ButtonEncrypt.Location = new System.Drawing.Point(16, 185);
            this.ButtonEncrypt.Name = "ButtonEncrypt";
            this.ButtonEncrypt.Size = new System.Drawing.Size(75, 38);
            this.ButtonEncrypt.TabIndex = 3;
            this.ButtonEncrypt.Text = "Encrypt File";
            this.ButtonEncrypt.UseVisualStyleBackColor = true;
            this.ButtonEncrypt.Click += new System.EventHandler(this.ButtonEncrypt_Click);
            // 
            // BoxInfo
            // 
            this.BoxInfo.Location = new System.Drawing.Point(16, 76);
            this.BoxInfo.Name = "BoxInfo";
            this.BoxInfo.ReadOnly = true;
            this.BoxInfo.Size = new System.Drawing.Size(203, 103);
            this.BoxInfo.TabIndex = 4;
            this.BoxInfo.Text = "";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(13, 60);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(28, 13);
            this.labelInfo.TabIndex = 5;
            this.labelInfo.Text = "Info:";
            // 
            // BoxPassword
            // 
            this.BoxPassword.Location = new System.Drawing.Point(244, 76);
            this.BoxPassword.Name = "BoxPassword";
            this.BoxPassword.Size = new System.Drawing.Size(235, 20);
            this.BoxPassword.TabIndex = 6;
            // 
            // ButtonDecrypt
            // 
            this.ButtonDecrypt.Location = new System.Drawing.Point(144, 185);
            this.ButtonDecrypt.Name = "ButtonDecrypt";
            this.ButtonDecrypt.Size = new System.Drawing.Size(75, 38);
            this.ButtonDecrypt.TabIndex = 7;
            this.ButtonDecrypt.Text = "Decrypt File";
            this.ButtonDecrypt.UseVisualStyleBackColor = true;
            this.ButtonDecrypt.Click += new System.EventHandler(this.ButtonDecrypt_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(241, 60);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "Password:";
            // 
            // ButtonCreateKey
            // 
            this.ButtonCreateKey.Location = new System.Drawing.Point(244, 124);
            this.ButtonCreateKey.Name = "ButtonCreateKey";
            this.ButtonCreateKey.Size = new System.Drawing.Size(75, 36);
            this.ButtonCreateKey.TabIndex = 9;
            this.ButtonCreateKey.Text = "Create Key";
            this.ButtonCreateKey.UseVisualStyleBackColor = true;
            this.ButtonCreateKey.Click += new System.EventHandler(this.ButtonCreateKey_Click);
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(241, 102);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(28, 13);
            this.labelKey.TabIndex = 10;
            this.labelKey.Text = "Key:";
            // 
            // ButtonExport
            // 
            this.ButtonExport.Location = new System.Drawing.Point(325, 124);
            this.ButtonExport.Name = "ButtonExport";
            this.ButtonExport.Size = new System.Drawing.Size(75, 36);
            this.ButtonExport.TabIndex = 11;
            this.ButtonExport.Text = "Export Key";
            this.ButtonExport.UseVisualStyleBackColor = true;
            this.ButtonExport.Click += new System.EventHandler(this.ButtonExport_Click);
            // 
            // ButtonImport
            // 
            this.ButtonImport.Location = new System.Drawing.Point(406, 124);
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.Size = new System.Drawing.Size(75, 36);
            this.ButtonImport.TabIndex = 12;
            this.ButtonImport.Text = "Import Key";
            this.ButtonImport.UseVisualStyleBackColor = true;
            this.ButtonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // CheckBoxPrivate
            // 
            this.CheckBoxPrivate.AutoSize = true;
            this.CheckBoxPrivate.Location = new System.Drawing.Point(295, 166);
            this.CheckBoxPrivate.Name = "CheckBoxPrivate";
            this.CheckBoxPrivate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CheckBoxPrivate.Size = new System.Drawing.Size(153, 17);
            this.CheckBoxPrivate.TabIndex = 13;
            this.CheckBoxPrivate.Text = "Include Private Parameters";
            this.CheckBoxPrivate.UseVisualStyleBackColor = true;
            // 
            // Encryptor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 235);
            this.Controls.Add(this.CheckBoxPrivate);
            this.Controls.Add(this.ButtonImport);
            this.Controls.Add(this.ButtonExport);
            this.Controls.Add(this.labelKey);
            this.Controls.Add(this.ButtonCreateKey);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.ButtonDecrypt);
            this.Controls.Add(this.BoxPassword);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.BoxInfo);
            this.Controls.Add(this.ButtonEncrypt);
            this.Controls.Add(this.ButtonBrowse);
            this.Controls.Add(this.BoxFilename);
            this.Controls.Add(this.labelFilename);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Encryptor";
            this.Text = "File Encryptor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.TextBox BoxFilename;
        private System.Windows.Forms.Button ButtonBrowse;
        private System.Windows.Forms.Button ButtonEncrypt;
        private System.Windows.Forms.RichTextBox BoxInfo;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.TextBox BoxPassword;
        private System.Windows.Forms.Button ButtonDecrypt;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button ButtonCreateKey;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.Button ButtonExport;
        private System.Windows.Forms.Button ButtonImport;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.OpenFileDialog OpenKeyFile;
        private System.Windows.Forms.CheckBox CheckBoxPrivate;
    }
}

