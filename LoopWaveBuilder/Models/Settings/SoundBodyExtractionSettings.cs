using System;
using System.ComponentModel.DataAnnotations;

namespace LoopWaveBuilder.Models.Settings
{
    public record SoundBodyExtractionSettings(
        string FileName,
        string ExtractionScriptName,
        int LoopBeginSamples,
        int LoopEndSamples,
        bool TrimsBeginingSilence,
        bool TrimsEndingSilence)
    {
        /// <summary>
        /// インスタンスの状態を検証し、検証に失敗した場合は例外をスローします。
        /// </summary>
        public void ThrowIfValidationFailed()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new ValidationException(
                    $"{nameof(FileName)} を指定してください。");
            }

            if (string.IsNullOrEmpty(ExtractionScriptName))
            {
                throw new ValidationException(
                    $"{nameof(ExtractionScriptName)} を指定してください。");
            }
        }
    }
}
