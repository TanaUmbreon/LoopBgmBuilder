
namespace LoopWaveBuilder.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SettingsFilePathTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BrowseSettingsFileButton = new System.Windows.Forms.Button();
            this.SettingsFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step 1: 設定ファイルを選択する";
            // 
            // SettingsFilePathTextBox
            // 
            this.SettingsFilePathTextBox.AllowDrop = true;
            this.SettingsFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsFilePathTextBox.Location = new System.Drawing.Point(12, 27);
            this.SettingsFilePathTextBox.Name = "SettingsFilePathTextBox";
            this.SettingsFilePathTextBox.Size = new System.Drawing.Size(537, 23);
            this.SettingsFilePathTextBox.TabIndex = 1;
            this.SettingsFilePathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.SettingsFilePathTextBox_DragDrop);
            this.SettingsFilePathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.SettingsFilePathTextBox_DragEnter);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MessageLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 340);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(599, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MessageLabel
            // 
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(81, 17);
            this.MessageLabel.Text = "MessageLabel";
            // 
            // BrowseSettingsFileButton
            // 
            this.BrowseSettingsFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseSettingsFileButton.Location = new System.Drawing.Point(555, 26);
            this.BrowseSettingsFileButton.Name = "BrowseSettingsFileButton";
            this.BrowseSettingsFileButton.Size = new System.Drawing.Size(32, 23);
            this.BrowseSettingsFileButton.TabIndex = 3;
            this.BrowseSettingsFileButton.Text = "...";
            this.BrowseSettingsFileButton.UseVisualStyleBackColor = true;
            this.BrowseSettingsFileButton.Click += new System.EventHandler(this.BrowseSettingsFileButton_Click);
            // 
            // SettingsFileOpenFileDialog
            // 
            this.SettingsFileOpenFileDialog.DefaultExt = "json";
            this.SettingsFileOpenFileDialog.Filter = "設定ファイル|*.settings.json|すべてのファイル|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 362);
            this.Controls.Add(this.BrowseSettingsFileButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SettingsFilePathTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SettingsFilePathTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MessageLabel;
        private System.Windows.Forms.Button BrowseSettingsFileButton;
        private System.Windows.Forms.OpenFileDialog SettingsFileOpenFileDialog;
    }
}

