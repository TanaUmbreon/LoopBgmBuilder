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
using LoopBgmBuilder.FormModels;
using LoopBgmBuilder.Models;
using LoopBgmBuilder.Settings;

namespace LoopBgmBuilder.Forms
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

        #region 状態遷移

        private void Model_StateChanged(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => { Model_StateChanged(sender, e); }));
                return;
            }

            try
            {
                SuspendLayout();

                LoadedSettingsFilePathTextBox.Text = model.LoadedSettingsFullName;
                SelectedOutputFolderPathTextBox.Text = model.SelectedOutputFolderPath;

                ExtractionsListView.Items.Clear();
                if (model.Extractors.Any())
                {
                    var items = new List<ListViewItem>(model.Extractors.Count());
                    foreach (IWaveBgmExtractor extractor in model.Extractors)
                    {
                        var item = new ListViewItem(extractor.InputFileName);
                        item.SubItems.Add(extractor.ExtractorName);
                        item.SubItems.Add(extractor.InputFolderPath);

                        items.Add(item);
                    }
                    ExtractionsListView.Items.AddRange(items.ToArray());
                }
                ExtractionsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                switch (model.State)
                {
                    case MainFormModelState.Initialized:
                        OpenSettingsFileButton.Enabled = true;
                        BrowseOutputFolderButton.Enabled = true;
                        ExecuteButton.Enabled = true;
                        ClearButton.Enabled = true;

                        Text = AssemblyInfo.Title;
                        break;

                    case MainFormModelState.Executing:
                        OpenSettingsFileButton.Enabled = false;
                        BrowseOutputFolderButton.Enabled = false;
                        ExecuteButton.Enabled = false;
                        ClearButton.Enabled = false;
                        break;

                    case MainFormModelState.Executed:
                        OpenSettingsFileButton.Enabled = true;
                        BrowseOutputFolderButton.Enabled = true;
                        ExecuteButton.Enabled = true;
                        ClearButton.Enabled = true;
                        break;
                }
            }
            finally
            {
                ResumeLayout(false);
            }
        }

        #endregion

        #region ループ加工設定ファイルの読み込み

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
                ShowErrorDialog(Text, "ループ加工設定ファイルを読み込めません", ex);
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
                ShowErrorDialog(Text, "ループ加工設定ファイルを読み込めません", ex);
            }
        }

        private void OpenSettingsFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsOpenFileDialog.InitialDirectory = model.LoadedSettingsFolderPath;
                SettingsOpenFileDialog.FileName = model.LoadedSettingsFileName;
                var result = SettingsOpenFileDialog.ShowDialog(this);
                if (result != DialogResult.OK) { return; }

                model.LoadSettingsFile(SettingsOpenFileDialog.FileName);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "ループ加工設定ファイルを読み込めません", ex);
            }
        }

        #endregion

        #region 出力先フォルダーの選択

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
                ShowErrorDialog(Text, "出力先フォルダーを選択できません", ex);
            }
        }

        private void SelectedOutputDirectoryPathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath);
                if (droppedItemPath == null) { return; }

                model.SelectOutputFolder(droppedItemPath);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "出力先フォルダーを選択できません", ex);
            }
        }

        private void BrowseOutputFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                var result = OutputFolderBrowserDialog.ShowDialog(this);
                if (result != DialogResult.OK) { return; }

                model.SelectOutputFolder(OutputFolderBrowserDialog.SelectedPath);
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
