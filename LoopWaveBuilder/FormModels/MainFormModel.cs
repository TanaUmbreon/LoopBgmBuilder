using System;
using System.IO;
using LoopWaveBuilder.Infrastracures;
using LoopWaveBuilder.Models.Settings;

namespace LoopWaveBuilder.FormModels
{
    public class MainFormModel
    {
        /// <summary>読み込んだループ加工設定</summary>
        private Settings? settings;

        /// <summary>
        /// 読み込んだループ加工設定ファイルのパスを取得します。
        /// </summary>
        public string LoadedSettingsFilePath { get; private set; }

        /// <summary>
        /// 選択した入力元フォルダーのパスを取得します。
        /// </summary>
        public string SelectedInputDirecotryPath { get; private set; }

        /// <summary>
        /// 選択した出力先フォルダーのパスを取得します。
        /// </summary>
        public string SelectedOutputDirecotryPath { get; private set; }

        /// <summary>
        /// <see cref="MainFormModel"/> の新しいインスタンスを生成します。
        /// </summary>
        public MainFormModel()
        {
            settings = null;
            LoadedSettingsFilePath = "";
            SelectedInputDirecotryPath = "";
            SelectedOutputDirecotryPath = "";
        }

        /// <summary>
        /// 指定したループ加工設定ファイルを読み込みます。
        /// </summary>
        /// <param name="fileName">読み込むループ加工設定ファイルの名前。</param>
        public void LoadSettingsFile(string fileName)
        {
            try
            {
                var file = new FileInfo(fileName);
                if (!file.Exists) { throw new FileNotFoundException(); }

                var repository = new JsonSettingsRepository(file.FullName);
                repository.Settings.ThrowIfValidationFailed();

                settings = repository.Settings;
                LoadedSettingsFilePath = file.FullName;
            }
            catch (Exception)
            {
                settings = null;
                LoadedSettingsFilePath = "";
                throw;
            }
        }

        /// <summary>
        /// 指定した入力元フォルダーを選択します。
        /// </summary>
        /// <param name="direcoryName">入力元フォルダーの名前。</param>
        public void SelectInputDirectory(string direcoryName)
        {
            try
            {
                var dir = new DirectoryInfo(direcoryName);
                if (!dir.Exists) { throw new DirectoryNotFoundException(); }

                SelectedInputDirecotryPath = dir.FullName;
            }
            catch (Exception)
            {
                SelectedInputDirecotryPath = "";
                throw;
            }
        }

        /// <summary>
        /// 指定した出力先フォルダーを選択します。
        /// </summary>
        /// <param name="direcoryName">入力元フォルダーの名前。</param>
        public void SelectOutputDirectory(string direcoryName)
        {
            try
            {
                var dir = new DirectoryInfo(direcoryName);
                if (!dir.Exists) { throw new DirectoryNotFoundException(); }

                SelectedOutputDirecotryPath = dir.FullName;
            }
            catch (Exception)
            {
                SelectedOutputDirecotryPath = "";
                throw;
            }
        }
    }
}
