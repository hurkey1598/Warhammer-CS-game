namespace Prototype_1._1
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GameBtn = new System.Windows.Forms.Button();
            this.SettingBtn = new System.Windows.Forms.Button();
            this.QuitBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::Prototype_1._1.Properties.Resources.ultramarine_logo;
            this.pictureBox1.Location = new System.Drawing.Point(16, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // GameBtn
            // 
            this.GameBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.GameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GameBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.GameBtn.Location = new System.Drawing.Point(217, 15);
            this.GameBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GameBtn.Name = "GameBtn";
            this.GameBtn.Size = new System.Drawing.Size(175, 37);
            this.GameBtn.TabIndex = 1;
            this.GameBtn.Text = "Start Game";
            this.GameBtn.UseVisualStyleBackColor = false;
            this.GameBtn.Click += new System.EventHandler(this.GameBtn_Click);
            // 
            // SettingBtn
            // 
            this.SettingBtn.BackColor = System.Drawing.Color.SeaGreen;
            this.SettingBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SettingBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SettingBtn.Location = new System.Drawing.Point(217, 57);
            this.SettingBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SettingBtn.Name = "SettingBtn";
            this.SettingBtn.Size = new System.Drawing.Size(175, 37);
            this.SettingBtn.TabIndex = 2;
            this.SettingBtn.Text = "Settings";
            this.SettingBtn.UseVisualStyleBackColor = false;
            this.SettingBtn.Click += new System.EventHandler(this.SettingBtn_Click);
            // 
            // QuitBtn
            // 
            this.QuitBtn.BackColor = System.Drawing.Color.Crimson;
            this.QuitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.QuitBtn.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuitBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.QuitBtn.Location = new System.Drawing.Point(217, 101);
            this.QuitBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.QuitBtn.Name = "QuitBtn";
            this.QuitBtn.Size = new System.Drawing.Size(175, 37);
            this.QuitBtn.TabIndex = 3;
            this.QuitBtn.Text = "Quit";
            this.QuitBtn.UseVisualStyleBackColor = false;
            this.QuitBtn.Click += new System.EventHandler(this.QuitBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(445, 149);
            this.Controls.Add(this.QuitBtn);
            this.Controls.Add(this.SettingBtn);
            this.Controls.Add(this.GameBtn);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button GameBtn;
        private System.Windows.Forms.Button SettingBtn;
        private System.Windows.Forms.Button QuitBtn;
    }
}

