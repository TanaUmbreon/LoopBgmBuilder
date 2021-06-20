using System.Collections.Generic;

namespace LoopWaveBuilder.Models.Settings
{
    /// <summary>
    /// WAVE 形式の BGM をループ加工する為の設定を格納します。
    /// </summary>
    public record WaveBgmProcessingSettings(
        OutputFormatSettings DefaultOutputFormat,
        IEnumerable<ExtractionEntrySettings> ExtractionEntries)
    {
        /// <summary>
        /// インスタンスの状態を検証し、検証に失敗した場合は例外をスローします。
        /// </summary>
        public void ThrowIfValidationFailed()
        {
            DefaultOutputFormat.ThrowIfValidationFailed();
            foreach(var entry in ExtractionEntries)
            {
                entry.ThrowIfValidationFailed();
            }
        }
    }
}
