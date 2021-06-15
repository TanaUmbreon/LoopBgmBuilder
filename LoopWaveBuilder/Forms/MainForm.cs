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
            Text = AssemblyInfo.Title;

            model = new MainFormModel();
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

        #region 設定ファイルの選択

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
            finally
            {
                LoadedSettingsFilePathTextBox.Text = model.LoadedSettingsFilePath;
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
            finally
            {
                LoadedSettingsFilePathTextBox.Text = model.LoadedSettingsFilePath;
            }
        }

        #endregion

        #region 入力元フォルダーの選択

        private void BrowseInputDirectoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = InputFolderBrowserDialog.ShowDialog(this);
                if (result != DialogResult.OK) { return; }

                model.SelectInputDirectory(InputFolderBrowserDialog.SelectedPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "入力元フォルダーを選択できません", ex);
            }
            finally
            {
                SelectedInputDirectoryPathTextBox.Text = model.SelectedInputDirecotryPath;
            }
        }

        private void SelectedInputDirectoryPathTextBox_DragEnter(object sender, DragEventArgs e)
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
                ShowErrorDialog(Text, "入力元フォルダーをドラッグ アンド ドロップできません", ex);
            }
        }

        private void SelectedInputDirectoryPathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath);
                if (droppedItemPath == null) { return; }

                model.SelectInputDirectory(droppedItemPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "入力元フォルダーを選択できません", ex);
            }
            finally
            {
                SelectedInputDirectoryPathTextBox.Text = model.SelectedInputDirecotryPath;
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
            finally
            {
                SelectedOutputDirectoryPathTextBox.Text = model.SelectedOutputDirecotryPath;
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
            finally
            {
                SelectedOutputDirectoryPathTextBox.Text = model.SelectedOutputDirecotryPath;
            }
        }

        #endregion

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenSettingsFileButton.Enabled = false;
                BrowseInputDirectoryButton.Enabled = false;
                BrowseOutputDirectoryButton.Enabled = false;
                ExecuteButton.Enabled = false;
                ClearButton.Enabled = false;

                await model.ExecuteAsync();
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "ループ加工を実行できません", ex);
            }
            finally
            {
                OpenSettingsFileButton.Enabled = true;
                BrowseInputDirectoryButton.Enabled = true;
                BrowseOutputDirectoryButton.Enabled = true;
                ExecuteButton.Enabled = true;
                ClearButton.Enabled = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            model.Clear();

            LoadedSettingsFilePathTextBox.Text = model.LoadedSettingsFilePath;
            SelectedInputDirectoryPathTextBox.Text = model.SelectedInputDirecotryPath;
            SelectedOutputDirectoryPathTextBox.Text = model.SelectedOutputDirecotryPath;
        }
    }
}
