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

namespace LoopWaveBuilder.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Text = AssemblyInfo.Title;
        }

        #region 設定ファイルの選択

        private void BrowseSettingsFileButton_Click(object sender, EventArgs e)
        {
            var result = SettingsFileOpenFileDialog.ShowDialog(this);
            if (result != DialogResult.OK) { return; }
            SettingsFilePathTextBox.Text = SettingsFileOpenFileDialog.FileName;
        }

        private void SettingsFilePathTextBox_DragEnter(object sender, DragEventArgs e)
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

        private void SettingsFilePathTextBox_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (!TryGetPathIfSingleItemDropped(e.Data, out string? droppedItemPath)) { return; }
                SettingsFilePathTextBox.Text = droppedItemPath;
            }
            catch (Exception ex)
            {
                ShowErrorDialog(Text, "設定ファイルをドラッグ アンド ドロップできません", ex);
            }
        }

        #endregion

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
        private bool TryGetPathIfSingleItemDropped(IDataObject data, out string? path)
        {
            path = null;
            if (!data.GetDataPresent(DataFormats.FileDrop)) { return false; }
            if (data.GetData(DataFormats.FileDrop) is not string[] droppedItems) { return false; }
            if (droppedItems.Length != 1) { return false; }

            path = droppedItems[0];
            return true;
        }

        #endregion
    }
}
