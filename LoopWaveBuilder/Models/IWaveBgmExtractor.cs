namespace LoopWaveBuilder.Models
{
    /// <summary>
    ///  WAVE 形式の BGM データの抽出をサポートします。
    /// </summary>
    public interface IWaveBgmExtractor
    {
        /// <summary>
        /// WAVE 形式の BGM データを抽出します。
        /// </summary>
        /// <returns></returns>
        WaveBgm Extract();
    }
}
