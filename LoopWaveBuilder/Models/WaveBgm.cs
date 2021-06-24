using System.Collections.Generic;
using NAudio.Wave;

namespace LoopWaveBuilder.Models
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
        /// 開始部の波形データを取得します。
        /// </summary>
        public WaveSampleFrame[] BeginingPart { get; }

        /// <summary>
        /// ループ部の波形データを取得します。
        /// </summary>
        public WaveSampleFrame[] LoopPart { get; }

        /// <summary>
        /// <see cref="WaveBgm"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="waveFormat">WAVE 形式のフォーマット情報。</param>
        /// <param name="beginingPart">開始部の波形データ。</param>
        /// <param name="loopPart">ループ部の波形データ。</param>
        public WaveBgm(WaveFormat waveFormat, WaveSampleFrame[] beginingPart, WaveSampleFrame[] loopPart)
        {
            WaveFormat = waveFormat;
            BeginingPart = beginingPart;
            LoopPart = loopPart;
        }
    }
}
