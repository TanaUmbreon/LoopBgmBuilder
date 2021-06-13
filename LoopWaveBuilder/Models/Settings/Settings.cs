using System.Collections.Generic;

namespace LoopWaveBuilder.Models.Settings
{
    /// <summary>
    /// WAV ファイルのループ加工設定を格納します。
    /// </summary>
    public record Settings(
        OutputFormatSettings OutputFormat,
        IEnumerable<SoundBodyExtractionSettings> SoundBodyExtractions)
    {
        /// <summary>
        /// インスタンスの状態を検証し、検証に失敗した場合は例外をスローします。
        /// </summary>
        public void ThrowIfValidationFailed()
        {
            OutputFormat.ThrowIfValidationFailed();
            foreach(var extraction in SoundBodyExtractions)
            {
                extraction.ThrowIfValidationFailed();
            }
        }
    }
}
