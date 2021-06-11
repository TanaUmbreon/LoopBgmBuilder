namespace LoopWaveBuilder.Models.Settings
{
    public record OutputFormatSettings(
        int BeginingSilenceMiliseconds,
        int LoopCount,
        int FadeoutDelayMiliseconds,
        int FadeoutMiliseconds,
        int EndingSilenceMiliseconds);
}
