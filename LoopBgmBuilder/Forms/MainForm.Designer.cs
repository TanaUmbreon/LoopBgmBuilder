
namespace LoopBgmBuilder.Forms
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
            this.LoadedSettingsFilePathTextBox = new System.Windows.Forms.TextBox();
            this.OpenSettingsFileButton = new System.Windows.Forms.Button();
            this.SettingsOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.BrowseOutputFolderButton = new System.Windows.Forms.Button();
            this.SelectedOutputFolderPathTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OutputFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ExtractionsListView = new System.Windows.Forms.ListView();
            this.InputFileNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.ExtractorNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.InputFolderPathColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step 1: ループ加工設定ファイルを読み込む";
            // 
            // LoadedSettingsFilePathTextBox
            // 
            this.LoadedSettingsFilePathTextBox.AllowDrop = true;
            this.LoadedSettingsFilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadedSettingsFilePathTextBox.Location = new System.Drawing.Point(12, 27);
            this.LoadedSettingsFilePathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.LoadedSettingsFilePathTextBox.Name = "LoadedSettingsFilePathTextBox";
            this.LoadedSettingsFilePathTextBox.ReadOnly = true;
            this.LoadedSettingsFilePathTextBox.Size = new System.Drawing.Size(514, 23);
            this.LoadedSettingsFilePathTextBox.TabIndex = 1;
            this.LoadedSettingsFilePathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.LoadedSettingsFilePathTextBox_DragDrop);
            this.LoadedSettingsFilePathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.LoadedSettingsFilePathTextBox_DragEnter);
            // 
            // OpenSettingsFileButton
            // 
            this.OpenSettingsFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenSettingsFileButton.Location = new System.Drawing.Point(532, 27);
            this.OpenSettingsFileButton.Name = "OpenSettingsFileButton";
            this.OpenSettingsFileButton.Size = new System.Drawing.Size(80, 23);
            this.OpenSettingsFileButton.TabIndex = 3;
            this.OpenSettingsFileButton.Text = "開く...";
            this.OpenSettingsFileButton.UseVisualStyleBackColor = true;
            this.OpenSettingsFileButton.Click += new System.EventHandler(this.OpenSettingsFileButton_Click);
            // 
            // SettingsOpenFileDialog
            // 
            this.SettingsOpenFileDialog.DefaultExt = "json";
            this.SettingsOpenFileDialog.Filter = "設定ファイル|*.settings.json|すべてのファイル|*.*";
            // 
            // BrowseOutputFolderButton
            // 
            this.BrowseOutputFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseOutputFolderButton.Location = new System.Drawing.Point(532, 279);
            this.BrowseOutputFolderButton.Name = "BrowseOutputFolderButton";
            this.BrowseOutputFolderButton.Size = new System.Drawing.Size(80, 23);
            this.BrowseOutputFolderButton.TabIndex = 9;
            this.BrowseOutputFolderButton.Text = "選択...";
            this.BrowseOutputFolderButton.UseVisualStyleBackColor = true;
            this.BrowseOutputFolderButton.Click += new System.EventHandler(this.BrowseOutputFolderButton_Click);
            // 
            // SelectedOutputFolderPathTextBox
            // 
            this.SelectedOutputFolderPathTextBox.AllowDrop = true;
            this.SelectedOutputFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectedOutputFolderPathTextBox.Location = new System.Drawing.Point(12, 280);
            this.SelectedOutputFolderPathTextBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.SelectedOutputFolderPathTextBox.Name = "SelectedOutputFolderPathTextBox";
            this.SelectedOutputFolderPathTextBox.ReadOnly = true;
            this.SelectedOutputFolderPathTextBox.Size = new System.Drawing.Size(514, 23);
            this.SelectedOutputFolderPathTextBox.TabIndex = 8;
            this.SelectedOutputFolderPathTextBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectedOutputDirectoryPathTextBox_DragDrop);
            this.SelectedOutputFolderPathTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.SelectedOutputDirectoryPathTextBox_DragEnter);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(378, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Step 2: 出力先フォルダーを選択する (※入力ファイルと同じ名前で上書き出力)";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(261, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 10);
            this.button2.TabIndex = 12;
            this.button2.Text = "実行";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ExecuteButton.Location = new System.Drawing.Point(226, 315);
            this.ExecuteButton.Margin = new System.Windows.Forms.Padding(3, 3, 9, 9);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(80, 28);
            this.ExecuteButton.TabIndex = 11;
            this.ExecuteButton.Text = "実行";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ClearButton.Location = new System.Drawing.Point(318, 315);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(80, 28);
            this.ClearButton.TabIndex = 12;
            this.ClearButton.Text = "クリア";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ExtractionsListView
            // 
            this.ExtractionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtractionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.InputFileNameColumnHeader,
            this.ExtractorNameColumnHeader,
            this.InputFolderPathColumnHeader});
            this.ExtractionsListView.FullRowSelect = true;
            this.ExtractionsListView.HideSelection = false;
            this.ExtractionsListView.Location = new System.Drawing.Point(12, 62);
            this.ExtractionsListView.Margin = new System.Windows.Forms.Padding(3, 3, 3, 9);
            this.ExtractionsListView.Name = "ExtractionsListView";
            this.ExtractionsListView.Size = new System.Drawing.Size(600, 191);
            this.ExtractionsListView.TabIndex = 13;
            this.ExtractionsListView.UseCompatibleStateImageBehavior = false;
            this.ExtractionsListView.View = System.Windows.Forms.View.Details;
            // 
            // InputFileNameColumnHeader
            // 
            this.InputFileNameColumnHeader.Text = "入力ファイル名";
            this.InputFileNameColumnHeader.Width = 120;
            // 
            // ExtractorNameColumnHeader
            // 
            this.ExtractorNameColumnHeader.Text = "BGM 抽出パターン";
            this.ExtractorNameColumnHeader.Width = 120;
            // 
            // InputFolderPathColumnHeader
            // 
            this.InputFolderPathColumnHeader.Text = "入力ファイルの場所";
            this.InputFolderPathColumnHeader.Width = 120;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 361);
            this.Controls.Add(this.ExtractionsListView);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.BrowseOutputFolderButton);
            this.Controls.Add(this.SelectedOutputFolderPathTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OpenSettingsFileButton);
            this.Controls.Add(this.LoadedSettingsFilePathTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoadedSettingsFilePathTextBox;
        private System.Windows.Forms.Button OpenSettingsFileButton;
        private System.Windows.Forms.OpenFileDialog SettingsOpenFileDialog;
        private System.Windows.Forms.Button BrowseOutputFolderButton;
        private System.Windows.Forms.TextBox SelectedOutputFolderPathTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FolderBrowserDialog OutputFolderBrowserDialog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.ListView ExtractionsListView;
        private System.Windows.Forms.ColumnHeader InputFileNameColumnHeader;
        private System.Windows.Forms.ColumnHeader ExtractorNameColumnHeader;
        private System.Windows.Forms.ColumnHeader InputFolderPathColumnHeader;
    }
}

