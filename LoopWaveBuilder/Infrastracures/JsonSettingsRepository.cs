using System;
using System.IO;
using LoopWaveBuilder.Settings;
using Newtonsoft.Json;

namespace LoopWaveBuilder.Infrastracures
{
    /// <summary>
    /// JSON 形式で定義された設定ファイルへの物理的な操作を実装します。
    /// </summary>
    public class JsonSettingsRepository
    {
        /// <summary>
        /// WAVE 形式の BGM をループ加工する為の設定を取得します。
        /// </summary>
        public WaveBgmProcessingSettings Settings { get; }

        /// <summary>
        /// <see cref="JsonSettingsRepository"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fileName">操作対象となる JSON 形式の設定ファイルのパス。</param>
        public JsonSettingsRepository(string fileName)
        {
            var file = new FileInfo(fileName);
            if (!file.Exists)
            {
                throw new FileNotFoundException($"設定ファイル '{file.FullName}' が見つかりません。");
            }

            Settings = JsonConvert.DeserializeObject<WaveBgmProcessingSettings>(File.ReadAllText(file.FullName))
                ?? throw new InvalidCastException();
        }
    }
}
