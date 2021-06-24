using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoopWaveBuilder.Infrastracures;
using LoopWaveBuilder.Models;
using LoopWaveBuilder.Models.Extractors;
using LoopWaveBuilder.Settings;
using NAudio.Wave;

namespace LoopWaveBuilder.FormModels
{
    /// <summary>
    /// モデル化されたメイン画面です。
    /// </summary>
    public class MainFormModel
    {
        /// <summary>読み込んだループ加工設定</summary>
        private WaveBgmProcessingSettings? settings;

        /// <summary>
        /// 画面の状態を取得します。
        /// </summary>
        public MainFormModelState State { get; private set; } = MainFormModelState.Initialized;

        /// <summary>
        /// 読み込んだループ加工設定ファイルのパスを取得します。
        /// </summary>
        public string LoadedSettingsFilePath { get; private set; } = "";

        /// <summary>
        /// 選択した出力先フォルダーのパスを取得します。
        /// </summary>
        public string SelectedOutputDirecotryPath { get; private set; } = "";

        /// <summary>
        /// <see cref="MainFormModel"/> の新しいインスタンスを生成します。
        /// </summary>
        public MainFormModel() { }

        #region イベント

        /// <summary>
        /// 状態が遷移した時に呼び出されます。
        /// </summary>
        public event EventHandler? StateChanged;

        /// <summary>
        /// <see cref="StateChanged"/> イベントを呼び出します。
        /// </summary>
        protected virtual void OnStateChanged()
            => StateChanged?.Invoke(this, EventArgs.Empty);

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

                State = MainFormModelState.LoadedSettings;
                OnStateChanged();
            }
            catch (Exception)
            {
                settings = null;
                LoadedSettingsFilePath = "";
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task ExecuteAsync()
        {
            return Task.Run(Execute);
        }

        private void Execute()
        {
            if (settings == null)
            {
                throw new InvalidOperationException("ループ加工設定ファイルを読み込んでください。");
            }
            if (string.IsNullOrWhiteSpace(SelectedOutputDirecotryPath))
            {
                throw new InvalidOperationException("出力先フォルダーを選択してください。");
            }

            try
            {
                State = MainFormModelState.Executing;
                OnStateChanged();

                var exceptions = new List<Exception>();
                foreach (ExtractionSettings extraction in settings.ExtractionEntries)
                {
                    try
                    {
                        IWaveBgmExtractor extrator = WaveBgmExtractorFactory.Create(extraction);
                        WaveBgm bgm = extrator.Extract();

                        string outFileName = Path.Combine(SelectedOutputDirecotryPath, Path.GetFileName(extraction.InputFileName));
                        using (var writer = new WaveFileWriter(outFileName, bgm.WaveFormat))
                        {
                            if (bgm.BeginingPart.Length > 0)
                            {
                                var begining = new List<float>(bgm.BeginingPart.Length * bgm.WaveFormat.Channels);
                                foreach (WaveSampleFrame frame in bgm.BeginingPart)
                                {
                                    begining.AddRange(frame.samplesInFrame);
                                }
                                writer.WriteSamples(begining.ToArray(), 0, begining.Count);
                            }

                            if (bgm.LoopPart.Length > 0 && settings.DefaultOutputFormat.LoopCount > 0)
                            {
                                var loop = new List<float>(bgm.LoopPart.Length * bgm.WaveFormat.Channels);
                                foreach (WaveSampleFrame frame in bgm.LoopPart)
                                {
                                    loop.AddRange(frame.samplesInFrame);
                                }
                                for (int loopCount = 0; loopCount < settings.DefaultOutputFormat.LoopCount; loopCount++)
                                {
                                    writer.WriteSamples(loop.ToArray(), 0, loop.Count);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }
                if (exceptions.Any())
                {
                    var ex = exceptions[0];
                    throw ex;
                }
            }
            finally
            {
                State = MainFormModelState.Executed;
                OnStateChanged();
            }
        }

        /// <summary>
        /// 初期状態にクリアします。
        /// </summary>
        public void Clear()
        {
            settings = null;
            LoadedSettingsFilePath = "";
            SelectedOutputDirecotryPath = "";

            State = MainFormModelState.Initialized;
            OnStateChanged();
        }
    }
}
