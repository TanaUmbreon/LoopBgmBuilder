using System.ComponentModel.DataAnnotations;

namespace LoopWaveBuilder.Settings
{
    /// <summary>
    /// BGM 出力時のフォーマット設定を格納します。
    /// </summary>
    public record OutputFormatSettings(
        int BeginingSilenceMiliseconds,
        int LoopCount,
        int FadeoutDelayMiliseconds,
        int FadeoutMiliseconds,
        int EndingSilenceMiliseconds)
    {
        /// <summary>
        /// インスタンスの状態を検証し、検証に失敗した場合は例外をスローします。
        /// </summary>
        public void ThrowIfValidationFailed()
        {
            if (BeginingSilenceMiliseconds < 0)
            {
                throw new ValidationException(
                    $"{nameof(BeginingSilenceMiliseconds)} は 0 以上を指定してください。");
            }

            if (LoopCount < 0)
            {
                throw new ValidationException(
                    $"{nameof(LoopCount)} は 0 以上を指定してください。");
            }

            if (FadeoutDelayMiliseconds < 0)
            {
                throw new ValidationException(
                    $"{nameof(FadeoutDelayMiliseconds)} は 0 以上を指定してください。");
            }

            if (FadeoutMiliseconds < 0)
            {
                throw new ValidationException(
                    $"{nameof(FadeoutMiliseconds)} は 0 以上を指定してください。");
            }

            if (EndingSilenceMiliseconds < 0)
            {
                throw new ValidationException(
                    $"{nameof(EndingSilenceMiliseconds)} は 0 以上を指定してください。");
            }
        }
    }
}
