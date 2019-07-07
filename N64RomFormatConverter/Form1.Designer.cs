namespace N64RomFormatConverter
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
            this.RomFileInputText = new System.Windows.Forms.TextBox();
            this.SelectFileButton = new System.Windows.Forms.Button();
            this.ConvertButton1 = new System.Windows.Forms.Button();
            this.ConvertButton2 = new System.Windows.Forms.Button();
            this.RomInfoText = new System.Windows.Forms.TextBox();
            this.ConvertButton3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.RomFileInputText.Location = new System.Drawing.Point(15, 15);
            this.RomFileInputText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RomFileInputText.Name = "RomFileInputText";
            this.RomFileInputText.ReadOnly = true;
            this.RomFileInputText.Size = new System.Drawing.Size(443, 23);
            this.RomFileInputText.TabIndex = 0;
            this.SelectFileButton.Location = new System.Drawing.Point(465, 14);
            this.SelectFileButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectFileButton.Name = "SelectFileButton";
            this.SelectFileButton.Size = new System.Drawing.Size(88, 25);
            this.SelectFileButton.TabIndex = 1;
            this.SelectFileButton.Text = "select file";
            this.SelectFileButton.UseVisualStyleBackColor = true;
            this.SelectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            this.ConvertButton1.Enabled = false;
            this.ConvertButton1.Location = new System.Drawing.Point(14, 162);
            this.ConvertButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConvertButton1.Name = "ConvertButton1";
            this.ConvertButton1.Size = new System.Drawing.Size(175, 32);
            this.ConvertButton1.TabIndex = 2;
            this.ConvertButton1.Text = "Convert to N64";
            this.ConvertButton1.UseVisualStyleBackColor = true;
            this.ConvertButton1.Click += new System.EventHandler(this.ConvertButton1_Click);
            this.ConvertButton2.Enabled = false;
            this.ConvertButton2.Location = new System.Drawing.Point(196, 162);
            this.ConvertButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConvertButton2.Name = "ConvertButton2";
            this.ConvertButton2.Size = new System.Drawing.Size(175, 32);
            this.ConvertButton2.TabIndex = 3;
            this.ConvertButton2.Text = "Convert to V64";
            this.ConvertButton2.UseVisualStyleBackColor = true;
            this.ConvertButton2.Click += new System.EventHandler(this.ConvertButton2_Click);
            this.RomInfoText.Location = new System.Drawing.Point(14, 45);
            this.RomInfoText.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RomInfoText.Multiline = true;
            this.RomInfoText.Name = "RomInfoText";
            this.RomInfoText.ReadOnly = true;
            this.RomInfoText.Size = new System.Drawing.Size(538, 109);
            this.RomInfoText.TabIndex = 4;
            this.ConvertButton3.Enabled = false;
            this.ConvertButton3.Location = new System.Drawing.Point(378, 162);
            this.ConvertButton3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ConvertButton3.Name = "ConvertButton3";
            this.ConvertButton3.Size = new System.Drawing.Size(175, 32);
            this.ConvertButton3.TabIndex = 5;
            this.ConvertButton3.Text = "Convert to Z64";
            this.ConvertButton3.UseVisualStyleBackColor = true;
            this.ConvertButton3.Click += new System.EventHandler(this.ConvertButton3_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 203);
            this.Controls.Add(this.ConvertButton3);
            this.Controls.Add(this.RomInfoText);
            this.Controls.Add(this.ConvertButton2);
            this.Controls.Add(this.ConvertButton1);
            this.Controls.Add(this.SelectFileButton);
            this.Controls.Add(this.RomFileInputText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "N64RomFormatConverter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox RomFileInputText;
        private System.Windows.Forms.Button SelectFileButton;
        private System.Windows.Forms.Button ConvertButton1;
        private System.Windows.Forms.Button ConvertButton2;
        private System.Windows.Forms.TextBox RomInfoText;
        private System.Windows.Forms.Button ConvertButton3;
    }
}

