using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoopWaveBuilder.FormModels;

namespace LoopWaveBuilder.Forms
{
    public partial class MainForm : Form
    {
        private readonly MainFormModel model;

        public MainForm()
        {
            InitializeComponent();

            model = new MainFormModel();
            model.StateChanged += Model_StateChanged;
            model.Clear();
        }

        private void Model_StateChanged(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Model_StateChanged(sender, e); }));
                return;
            }

            LoadedSettingsFilePathTextBox.Text = model.LoadedSettingsFilePath;
            SelectedOutputDirectoryPathTextBox.Text = model.SelectedOutputDirecotryPath;

            switch (model.State)
            {
                case MainFormModelState.Initialized:
                    OpenSettingsFileButton.Enabled = true;
                    ExtractionEntriesListView.Items.Clear();

                    BrowseOutputDirectoryButton.Enabled = true;

                    ExecuteButton.Enabled = true;
                    ClearButton.Enabled = true;

                    Text = AssemblyInfo.Title;
                    break;

                case MainFormModelState.LoadedSettings:
                    //ExtractionEntriesListView.Items.Add();
                    break;

                case MainFormModelState.Executing:
                    OpenSettingsFileButton.Enabled = false;
                    BrowseOutputDirectoryButton.Enabled = false;
                    ExecuteButton.Enabled = false;
                    ClearButton.Enabled = false;
                    break;

                case MainFormModelState.Executed:
                    OpenSettingsFileButton.Enabled = true;
                    BrowseOutputDirectoryButton.Enabled = true;
                    ExecuteButton.Enabled = true;
                    ClearButton.Enabled = true;
                    break;
            }
        }

        #region 汎用

        /// <summary>
        /// 指定したタイトル、見出し、例外でエラーを表すダイアログを表示します。例外はプライマリ テキストとして表示します。
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="heading"></param>
        /// <param name="ex"></param>
        private void ShowErrorDialog(string caption, string heading, Exception ex)
        {
            TaskDialog.ShowDialog(this, new TaskDialogPage()
            {
                Caption = caption,
                Icon = TaskDialogIcon.Error,
                Heading = heading,
                Text = ex.Message,
                Buttons = { TaskDialogButton.Close }
            });
        }

        /// <summary>
        /// 指定したドラッグ アンド ドロップのデータが単独のファイルまたはフォルダーのドロップの場合、そのパスを取得します。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <returns>単独のファイルまたはフォルダーをドロップしてパスを取得できた場合は true、そうでない場合は false。</returns>
        private static bool TryGetPathIfSingleItemDropped(IDataObject data, out string? path)
        {
            path = null;
            if (!data.GetDataPresent(DataFormats.FileDrop)) { return false; }
            if (data.GetData(DataFormats.FileDrop) is not string[] droppedItems) { return false; }
            if (droppedItems.Length != 1) { return false; }

            path = droppedItems[0];
            return true;
        }

        #endregion

        #region 設定ファイルの読み込み

        private void OpenSettingsFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = SettingsOpenFileDialog.ShowDialog(this);
                if (result != DialogResult.OK) { return; }

                model.LoadSettingsFile(SettingsOpenFileDialog.FileName);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "設定ファイルを読み込めません", ex);
            }
        }

        private void LoadedSettingsFilePathTextBox_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                // 単独ファイル選択のドラッグアンドドロップのみ受け付け、複数選択やフォルダー選択は受け付けない
                e.Effect = DragDropEffects.None;
                if (!TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath)) { return; }
                if (!File.Exists(droppedItemPath)) { return; }

                e.Effect = DragDropEffects.All;
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "設定ファイルをドラッグ アンド ドロップできません", ex);
            }
        }

        private void LoadedSettingsFilePathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath);
                if (droppedItemPath == null) { return; }

                model.LoadSettingsFile(droppedItemPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "設定ファイルを読み込めません", ex);
            }
        }

        #endregion

        #region 出力先フォルダーの選択

        private void BrowseOutputDirectoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = OutputFolderBrowserDialog.ShowDialog(this);
                if (result != DialogResult.OK) { return; }

                model.SelectOutputDirectory(OutputFolderBrowserDialog.SelectedPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "出力先フォルダーを選択できません", ex);
            }
        }

        private void SelectedOutputDirectoryPathTextBox_DragEnter(object sender, DragEventArgs e)
        {
            try
            {
                // 単独フォルダー選択のドラッグアンドドロップのみ受け付け、複数選択やファイル選択は受け付けない
                e.Effect = DragDropEffects.None;
                if (!TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath)) { return; }
                if (!Directory.Exists(droppedItemPath)) { return; }

                e.Effect = DragDropEffects.All;
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "出力先フォルダーをドラッグ アンド ドロップできません", ex);
            }
        }

        private void SelectedOutputDirectoryPathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath);
                if (droppedItemPath == null) { return; }

                model.SelectOutputDirectory(droppedItemPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "出力先フォルダーを選択できません", ex);
            }
        }

        #endregion

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                await model.ExecuteAsync();
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "ループ加工を実行できません", ex);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            model.Clear();
        }
    }
}
