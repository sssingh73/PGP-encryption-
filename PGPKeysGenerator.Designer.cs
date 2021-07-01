namespace GeneratePGPKeys
{
    partial class PGPKeysGenerator
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
            this.folderSelector = new System.Windows.Forms.FolderBrowserDialog();
            this.fileSelector = new System.Windows.Forms.OpenFileDialog();
            this.Encryptor = new System.Windows.Forms.GroupBox();
            this.decryptBtn = new System.Windows.Forms.Button();
            this.encryptBtn = new System.Windows.Forms.Button();
            this.browseBtn2 = new System.Windows.Forms.Button();
            this.fileTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.expiryYrsCMB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.keyGenerateBtn = new System.Windows.Forms.Button();
            this.browseBtn = new System.Windows.Forms.Button();
            this.folderPathTxt = new System.Windows.Forms.TextBox();
            this.KeyNameTxt = new System.Windows.Forms.TextBox();
            this.PasswordTxt = new System.Windows.Forms.TextBox();
            this.IdentityTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.Label();
            this.Identity = new System.Windows.Forms.Label();
            this.Encryptor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderSelector
            // 
            this.folderSelector.HelpRequest += new System.EventHandler(this.folderSelector_HelpRequest);
            // 
            // fileSelector
            // 
            this.fileSelector.FileName = "openFileDialog1";
            this.fileSelector.Title = "Browse File";
            // 
            // Encryptor
            // 
            this.Encryptor.Controls.Add(this.decryptBtn);
            this.Encryptor.Controls.Add(this.encryptBtn);
            this.Encryptor.Controls.Add(this.browseBtn2);
            this.Encryptor.Controls.Add(this.fileTxt);
            this.Encryptor.Controls.Add(this.label1);
            this.Encryptor.Enabled = false;
            this.Encryptor.Location = new System.Drawing.Point(21, 268);
            this.Encryptor.Name = "Encryptor";
            this.Encryptor.Size = new System.Drawing.Size(445, 146);
            this.Encryptor.TabIndex = 13;
            this.Encryptor.TabStop = false;
            this.Encryptor.Text = "Verify encryption / decryption";
            // 
            // decryptBtn
            // 
            this.decryptBtn.Location = new System.Drawing.Point(329, 75);
            this.decryptBtn.Name = "decryptBtn";
            this.decryptBtn.Size = new System.Drawing.Size(92, 27);
            this.decryptBtn.TabIndex = 13;
            this.decryptBtn.Text = "Decrypt";
            this.decryptBtn.UseVisualStyleBackColor = true;
            this.decryptBtn.Click += new System.EventHandler(this.decryptBtn_Click_1);
            // 
            // encryptBtn
            // 
            this.encryptBtn.Location = new System.Drawing.Point(231, 75);
            this.encryptBtn.Name = "encryptBtn";
            this.encryptBtn.Size = new System.Drawing.Size(92, 27);
            this.encryptBtn.TabIndex = 13;
            this.encryptBtn.Text = "Encrypt";
            this.encryptBtn.UseVisualStyleBackColor = true;
            this.encryptBtn.Click += new System.EventHandler(this.encryptBtn_Click_1);
            // 
            // browseBtn2
            // 
            this.browseBtn2.Location = new System.Drawing.Point(392, 37);
            this.browseBtn2.Name = "browseBtn2";
            this.browseBtn2.Size = new System.Drawing.Size(29, 28);
            this.browseBtn2.TabIndex = 12;
            this.browseBtn2.Text = "...";
            this.browseBtn2.UseVisualStyleBackColor = true;
            this.browseBtn2.Click += new System.EventHandler(this.browseBtn2_Click);
            // 
            // fileTxt
            // 
            this.fileTxt.Location = new System.Drawing.Point(84, 37);
            this.fileTxt.Name = "fileTxt";
            this.fileTxt.Size = new System.Drawing.Size(302, 20);
            this.fileTxt.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "FileName :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.expiryYrsCMB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.keyGenerateBtn);
            this.groupBox1.Controls.Add(this.browseBtn);
            this.groupBox1.Controls.Add(this.folderPathTxt);
            this.groupBox1.Controls.Add(this.KeyNameTxt);
            this.groupBox1.Controls.Add(this.PasswordTxt);
            this.groupBox1.Controls.Add(this.IdentityTxt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.Identity);
            this.groupBox1.Location = new System.Drawing.Point(21, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 220);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Specify below values";
            // 
            // expiryYrsCMB
            // 
            this.expiryYrsCMB.FormattingEnabled = true;
            this.expiryYrsCMB.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5"});
            this.expiryYrsCMB.Location = new System.Drawing.Point(80, 153);
            this.expiryYrsCMB.Name = "expiryYrsCMB";
            this.expiryYrsCMB.Size = new System.Drawing.Size(146, 21);
            this.expiryYrsCMB.TabIndex = 12;
            this.expiryYrsCMB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Expiry (Yrs):";
            // 
            // keyGenerateBtn
            // 
            this.keyGenerateBtn.Location = new System.Drawing.Point(346, 178);
            this.keyGenerateBtn.Name = "keyGenerateBtn";
            this.keyGenerateBtn.Size = new System.Drawing.Size(75, 23);
            this.keyGenerateBtn.TabIndex = 10;
            this.keyGenerateBtn.Text = "Generate";
            this.keyGenerateBtn.UseVisualStyleBackColor = true;
            this.keyGenerateBtn.Click += new System.EventHandler(this.keyGenerateBtn_Click);
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(392, 122);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(29, 28);
            this.browseBtn.TabIndex = 9;
            this.browseBtn.Text = "...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.browseBtn_Click);
            // 
            // folderPathTxt
            // 
            this.folderPathTxt.Location = new System.Drawing.Point(80, 127);
            this.folderPathTxt.Name = "folderPathTxt";
            this.folderPathTxt.Size = new System.Drawing.Size(306, 20);
            this.folderPathTxt.TabIndex = 8;
            // 
            // KeyNameTxt
            // 
            this.KeyNameTxt.Location = new System.Drawing.Point(80, 94);
            this.KeyNameTxt.Name = "KeyNameTxt";
            this.KeyNameTxt.Size = new System.Drawing.Size(306, 20);
            this.KeyNameTxt.TabIndex = 7;
            // 
            // PasswordTxt
            // 
            this.PasswordTxt.Location = new System.Drawing.Point(80, 61);
            this.PasswordTxt.Name = "PasswordTxt";
            this.PasswordTxt.Size = new System.Drawing.Size(306, 20);
            this.PasswordTxt.TabIndex = 6;
            // 
            // IdentityTxt
            // 
            this.IdentityTxt.Location = new System.Drawing.Point(80, 32);
            this.IdentityTxt.Name = "IdentityTxt";
            this.IdentityTxt.Size = new System.Drawing.Size(306, 20);
            this.IdentityTxt.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "FolderPath :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "KeyName :";
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(21, 61);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(59, 13);
            this.Password.TabIndex = 1;
            this.Password.Text = "Password :";
            // 
            // Identity
            // 
            this.Identity.AutoSize = true;
            this.Identity.Location = new System.Drawing.Point(33, 32);
            this.Identity.Name = "Identity";
            this.Identity.Size = new System.Drawing.Size(47, 13);
            this.Identity.TabIndex = 0;
            this.Identity.Text = "Identity :";
            // 
            // PGPKeysGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 426);
            this.Controls.Add(this.Encryptor);
            this.Controls.Add(this.groupBox1);
            this.Name = "PGPKeysGenerator";
            this.Text = "PGPKeysGenerator";
            this.Load += new System.EventHandler(this.PGPKeysGenerator_Load);
            this.Encryptor.ResumeLayout(false);
            this.Encryptor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderSelector;
        private System.Windows.Forms.OpenFileDialog fileSelector;
        private System.Windows.Forms.GroupBox Encryptor;
        private System.Windows.Forms.Button decryptBtn;
        private System.Windows.Forms.Button encryptBtn;
        private System.Windows.Forms.Button browseBtn2;
        private System.Windows.Forms.TextBox fileTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button keyGenerateBtn;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox folderPathTxt;
        private System.Windows.Forms.TextBox KeyNameTxt;
        private System.Windows.Forms.TextBox PasswordTxt;
        private System.Windows.Forms.TextBox IdentityTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label Identity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox expiryYrsCMB;
    }
}

