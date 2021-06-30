using NAudio.Wave;

namespace LoopBgmBuilder.Models
{
    /// <summary>
    /// ループ再生可能な構造を持つ WAVE 形式の BGM データです。
    /// </summary>
    public class WaveBgm
    {
        /// <summary>
        /// WAVE 形式のフォーマット情報を取得します。
        /// </summary>
        public WaveFormat WaveFormat { get; }

        /// <summary>
        /// 開始部の音声データ サンプルの配列を取得します。
        /// </summary>
        public float[] BeginingPartSamples { get; }

        /// <summary>
        /// ループ部の音声データ サンプルの配列を取得します。
        /// </summary>
        public float[] LoopPartSamples { get; }

        /// <summary>
        /// <see cref="WaveBgm"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="waveFormat">WAVE 形式のフォーマット情報。</param>
        /// <param name="beginingPartSamples">開始部の音声データ サンプルの配列。</param>
        /// <param name="loopPartSamples">ループ部の音声データ サンプルの配列。</param>
        public WaveBgm(WaveFormat waveFormat, float[] beginingPartSamples, float[] loopPartSamples)
        {
            WaveFormat = waveFormat;
            BeginingPartSamples = beginingPartSamples;
            LoopPartSamples = loopPartSamples;
        }
    }
}
