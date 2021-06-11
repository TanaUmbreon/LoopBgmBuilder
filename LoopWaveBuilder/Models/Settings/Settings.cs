using System.Collections.Generic;

namespace LoopWaveBuilder.Models.Settings
{
    /// <summary>
    /// アプリケーション設定を格納します。
    /// </summary>
    public record Settings(
        OutputFormatSettings OutputFormat,
        IEnumerable<SoundBodyExtractionSettings> SoundBodyExtractions);
}
