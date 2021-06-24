using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LoopWaveBuilder.Models
{
    /// <summary>
    /// WAVE 形式の音声データ 1 フレーム分のサンプルを格納します。
    /// </summary>
    [DebuggerDisplay("{GetSamplesInFrameValues()}")]
    public class WaveSampleFrame : IReadOnlyList<float>
    {
        private readonly float[] samplesInFrame;

        /// <summary>
        /// フレーム内の指定したチャネル インデックスにある音声データ サンプルを取得します。
        /// </summary>
        /// <param name="channelIndex">取得する音声データ サンプルを指し示す、0 から始まるチャネル インデックス。</param>
        /// <returns>音声データ サンプル。</returns>
        public float this[int channelIndex] => samplesInFrame[channelIndex];

        /// <summary>
        /// フレーム内の音声データ サンプル数 (チャネル数) を取得します。
        /// </summary>
        public int Count => samplesInFrame.Length;

        /// <summary>
        /// <see cref="WaveSampleFrame"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="samplesInFrame">1 フレームに含まれる音声データのサンプル。例えば、2 チャネル (ステレオ) 形式の場合は 2 サンプル、1 チャネル (モノラル) 形式の場合は 1 サンプル。</param>
        public WaveSampleFrame(float[] samplesInFrame)
        {
            this.samplesInFrame = samplesInFrame;
        }

        /// <summary>
        /// フレーム内の音声データ サンプルを反復処理する列挙子を返します。
        /// </summary>
        /// <returns></returns>
        public IEnumerator<float> GetEnumerator() 
            => (IEnumerator<float>)samplesInFrame.GetEnumerator();

        /// <summary>
        /// 無音のフレームである事を判定します。
        /// </summary>
        /// <returns></returns>
        public bool IsSilence()
        {
            return samplesInFrame.All(f => f == 0f);
        }

        IEnumerator IEnumerable.GetEnumerator()
            => samplesInFrame.GetEnumerator();

        private string GetSamplesInFrameValues()
            => string.Join(", ", samplesInFrame.Select(f => f.ToString("0.00000")));
    }
}
