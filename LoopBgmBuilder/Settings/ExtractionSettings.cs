using System.ComponentModel.DataAnnotations;

namespace LoopBgmBuilder.Settings
{
    /// <summary>
    /// BGM データを抽出する為の設定を格納します。
    /// </summary>
    public record ExtractionSettings(
        string ExtractorName,
        string InputFileName,
        int? LoopBeginSamples,
        int? LoopEndSamples,
        bool? TrimsBeginingSilence,
        bool? TrimsEndingSilence)
    {
        /// <summary>
        /// インスタンスの状態を検証し、検証に失敗した場合は例外をスローします。
        /// </summary>
        public void ThrowIfValidationFailed()
        {
            if (string.IsNullOrEmpty(ExtractorName))
            {
                throw new ValidationException(
                    $"{nameof(ExtractorName)} を指定してください。");
            }

            if (string.IsNullOrEmpty(InputFileName))
            {
                throw new ValidationException(
                    $"{nameof(InputFileName)} を指定してください。");
            }
        }
    }
}
