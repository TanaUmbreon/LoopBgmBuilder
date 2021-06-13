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
        /// <see cref="MainFormModel"/> の新しいインスタンスを生成します。
        /// </summary>
        public MainFormModel()
        {
            settings = null;
            LoadedSettingsFilePath = "";
        }

        #region イベント

        #region LoadSettingsFileCompleted イベント

        /// <summary>
        /// ループ加工設定ファイルの読み込みが完了した時に呼び出されます。
        /// </summary>
        public event EventHandler? LoadSettingsFileCompleted;

        /// <summary>
        /// <see cref="LoadSettingsFileCompleted"/> イベントを呼び出します。
        /// </summary>
        protected virtual void OnLoadSettingsFileCompleted()
        {
            LoadSettingsFileCompleted?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region LoadSettingsFileFailed イベント

        /// <summary>
        /// ループ加工設定ファイルの読み込みに失敗した時に呼び出されます。
        /// </summary>
        public event EventHandler<ErrorEventArgs>? LoadSettingsFileFailed;

        /// <summary>
        /// <see cref="LoadSettingsFileFailed"/> イベントを呼び出します。
        /// </summary>
        /// <param name="ex">失敗の原因となった例外。</param>
        protected virtual void OnLoadSettingsFileFailed(Exception ex)
        {
            LoadSettingsFileFailed?.Invoke(this, new ErrorEventArgs(ex));
        }

        #endregion

        #endregion

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
                OnLoadSettingsFileCompleted();
            }
            catch (Exception ex)
            {
                settings = null;
                LoadedSettingsFilePath = "";
                OnLoadSettingsFileFailed(ex);
            }
        }
    }
}
