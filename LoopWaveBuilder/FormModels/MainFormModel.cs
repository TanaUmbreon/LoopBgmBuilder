using System;
using System.IO;
using LoopWaveBuilder.Infrastracures;
using LoopWaveBuilder.Models.Settings;

namespace LoopWaveBuilder.FormModels
{
    public class MainFormModel
    {
        private Settings? settings;

        public string OpenedSettingsFilePath { get; private set; }

        /// <summary>
        /// <see cref="MainFormModel"/> の新しいインスタンスを生成します。
        /// </summary>
        public MainFormModel()
        {
            OpenedSettingsFilePath = "";
        }

        #region イベント

        #region OpenSettingsFileCompleted イベント

        /// <summary>
        /// 設定ファイルが開けた時に呼び出されます。
        /// </summary>
        public event EventHandler? OpenSettingsFileCompleted;

        /// <summary>
        /// <see cref="OpenSettingsFileCompleted"/> イベントを呼び出します。
        /// </summary>
        protected virtual void OnOpenSettingsFileCompleted()
        {
            OpenSettingsFileCompleted?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region OpenSettingsFileFailed イベント

        /// <summary>
        /// 設定ファイルが開けなかった時に呼び出されます。
        /// </summary>
        public event EventHandler? OpenSettingsFileFailed;

        /// <summary>
        /// <see cref="OpenSettingsFileFailed"/> イベントを呼び出します。
        /// </summary>
        protected virtual void OnOpenSettingsFileFailed()
        {
            OpenSettingsFileFailed?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #endregion

        /// <summary>
        /// 指定した設定ファイルを開きます。
        /// </summary>
        /// <param name="fileName"></param>
        public void OpenSettingsFile(string fileName)
        {
            try
            {
                var file = new FileInfo(fileName);
                if (!file.Exists) { throw new FileNotFoundException(); }

                var repository = new JsonSettingsRepository(file.FullName);
 
                settings = repository.Settings;
                OpenedSettingsFilePath = file.FullName;
                OnOpenSettingsFileCompleted();
            }
            catch (Exception)
            {
                settings = null;
                OpenedSettingsFilePath = "";
                OnOpenSettingsFileFailed();
            }
        }
    }
}