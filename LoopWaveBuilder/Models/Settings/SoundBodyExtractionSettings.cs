namespace LoopWaveBuilder.Models.Settings
{
    public record SoundBodyExtractionSettings(
        string FileName,
        string ExtractionScriptName,
        int LoopBeginSamples,
        int LoopEndSamples,
        bool TrimsBeginingSilence,
        bool TrimsEndingSilence);
}
