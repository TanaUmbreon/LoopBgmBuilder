using System;
using System.IO;
using LoopWaveBuilder.Settings;
using Newtonsoft.Json;

namespace LoopWaveBuilder.Infrastracures
{
    /// <summary>
    /// JSON 形式で記述された BGM のループ加工設定ファイルへの物理的な操作を実装します。
    /// </summary>
    public class JsonSettingsRepository
    {
        /// <summary>
        /// BGM のループ加工設定を取得します。
        /// </summary>
        public LoopProcessingSettings Settings { get; }

        /// <summary>
        /// このループ加工設定ファイルの完全パスを取得します。
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// このループ加工設定ファイルが格納されているフォルダーのパスを取得します。
        /// </summary>
        public string FolderPath { get; }

        /// <summary>
        /// このループ加工設定ファイルの名前を取得します。
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// <see cref="JsonSettingsRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fileName">操作対象となる、JSON 形式で記述された BGM のループ加工設定ファイルの名前。</param>
        public JsonSettingsRepository(string fileName)
            : this(new FileInfo(fileName)) { }

        /// <summary>
        /// <see cref="JsonSettingsRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="file">操作対象となる、JSON 形式で記述された BGM のループ加工設定ファイル。</param>
        public JsonSettingsRepository(FileInfo file)
        {
            if (!file.Exists)
            {
                throw new FileNotFoundException($"ループ加工設定ファイル '{file.FullName}' が見つかりません。");
            }

            LoopProcessingSettings settings = JsonConvert.DeserializeObject<LoopProcessingSettings>(File.ReadAllText(file.FullName))
                ?? throw new InvalidCastException($"ループ加工設定ファイルの形式ではありません。");
            settings.ThrowIfValidationFailed();

            Settings = settings;
            FullName = file.FullName;
            FolderPath = file.DirectoryName ?? "";
            FileName = file.Name;
        }
    }
}
