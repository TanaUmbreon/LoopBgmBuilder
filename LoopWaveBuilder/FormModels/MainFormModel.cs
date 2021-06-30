using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoopBgmBuilder.Infrastracures;
using LoopBgmBuilder.Models;
using LoopBgmBuilder.Models.Extractors;

namespace LoopBgmBuilder.FormModels
{
    /// <summary>
    /// モデル化されたメイン画面です。
    /// </summary>
    public class MainFormModel
    {
        /// <summary>読み込んだループ加工設定リポジトリ</summary>
        private JsonSettingsRepository? repository;
        /// <summary>読み込んだ BGM データ抽出オブジェクト</summary>
        private IEnumerable<IWaveBgmExtractor>? extrators;

        /// <summary>
        /// 読み込んだループ加工設定ファイルの完全パスを取得します。
        /// </summary>
        public string LoadedSettingsFullName => repository?.FullName ?? "";

        /// <summary>
        /// 読み込んだループ加工設定ファイルが格納されているフォルダーのパスを取得します。
        /// </summary>
        public string LoadedSettingsFolderPath => repository?.FolderPath ?? "";

        /// <summary>
        /// 読み込んだループ加工設定ファイルの名前を取得します。
        /// </summary>
        public string LoadedSettingsFileName => repository?.FileName ?? "";

        /// <summary>
        /// 読み込んだループ加工設定から生成された BGM データ抽出オブジェクトのコレクションを取得します。
        /// </summary>
        public IEnumerable<IWaveBgmExtractor> Extractors
            => extrators ?? Array.Empty<IWaveBgmExtractor>();

        /// <summary>
        /// 選択した出力先フォルダーのパスを取得します。
        /// </summary>
        public string SelectedOutputFolderPath { get; private set; } = "";

        /// <summary>
        /// <see cref="MainFormModel"/> の新しいインスタンスを生成します。
        /// </summary>
        public MainFormModel() { }

        #region 状態遷移

        /// <summary>
        /// 画面の状態を取得します。
        /// </summary>
        public MainFormModelState State { get; private set; } = MainFormModelState.Initialized;

        /// <summary>
        /// 状態が遷移した時に呼び出されます。
        /// </summary>
        public event EventHandler? StateChanged;

        /// <summary>
        /// 指定した状態に遷移させ、<see cref="StateChanged"/> イベントを呼び出します。
        /// </summary>
        /// <param name="newState">遷移させる新しい状態。</param>
        protected virtual void OnStateChanged(MainFormModelState newState)
        {
            State = newState;
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region ループ加工設定ファイルの読み込み

        /// <summary>
        /// 指定したループ加工設定ファイルを読み込みます。
        /// </summary>
        /// <param name="fileName">読み込むループ加工設定ファイルの名前。</param>
        public void LoadSettingsFile(string fileName)
        {
            try
            {
                var repository = new JsonSettingsRepository(fileName);
                var extrators = repository.Settings.Extractions.Select(e => WaveBgmExtractorFactory.Create(e));

                this.repository = repository;
                this.extrators = extrators;
                OnStateChanged(MainFormModelState.LoadSettingsSuccessful);
            }
            catch
            {
                repository = null;
                extrators = null;
                OnStateChanged(MainFormModelState.LoadSettingsFailed);
                throw;
            }
        }

        #endregion

        #region 出力先フォルダーの選択

        /// <summary>
        /// 指定した出力先フォルダーを選択します。
        /// </summary>
        /// <param name="folderPath">選択する出力先フォルダーのパス。</param>
        public void SelectOutputFolder(string folderPath)
        {
            try
            {
                var dir = new DirectoryInfo(folderPath);

                SelectedOutputFolderPath = dir.FullName;
                OnStateChanged(MainFormModelState.LoadSettingsSuccessful);
            }
            catch
            {
                SelectedOutputFolderPath = "";
                OnStateChanged(MainFormModelState.LoadSettingsFailed);
                throw;
            }
        }

        #endregion

        #region ループ加工の実行

        /// <summary>
        /// ループ加工を非同期で実行します。
        /// </summary>
        /// <returns>非同期操作。</returns>
        public Task ExecuteAsync()
            => Task.Run(Execute);

        /// <summary>
        /// ループ加工を実行します。
        /// </summary>
        private void Execute()
        {
            if ((repository == null) || (extrators == null))
            {
                throw new InvalidOperationException("ループ加工設定ファイルを読み込んでください。");
            }
            if (!extrators.Any())
            {
                throw new InvalidOperationException("ループ加工設定ファイルに 1 つ以上の BGM 抽出設定を記述してください。");
            }
            if (string.IsNullOrWhiteSpace(SelectedOutputFolderPath))
            {
                throw new InvalidOperationException("出力先フォルダーを選択してください。");
            }

            try
            {
                OnStateChanged(MainFormModelState.Executing);

                foreach (IWaveBgmExtractor extrator in extrators)
                {
                    try
                    {
                        WaveBgm bgm = extrator.Extract();

                        string outFileName = Path.Combine(SelectedOutputFolderPath, Path.GetFileName(extrator.InputFileName));
                        using var writer = new WaveBgmWriter(outFileName, bgm, repository.Settings.DefaultOutputFormat);
                        writer.Write();
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
            }
            finally
            {
                OnStateChanged(MainFormModelState.Executed);
            }
        }

        #endregion

        /// <summary>
        /// 初期状態にクリアします。
        /// </summary>
        public void Clear()
        {
            repository = null;
            extrators = null;
            SelectedOutputFolderPath = "";

            OnStateChanged(MainFormModelState.Initialized);
        }
    }
}
