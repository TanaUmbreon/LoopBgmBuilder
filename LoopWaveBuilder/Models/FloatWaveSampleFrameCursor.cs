using System;
using System.Linq;

namespace LoopBgmBuilder.Models
{
    /// <summary>
    /// <see cref="FloatWaveSampleBuffer"/> の音声データ サンプルをフレーム単位で参照、参照位置の移動および操作するカーソルです。
    /// </summary>
    public class FloatWaveSampleFrameCursor
    {
        /// <summary>先頭を指し示すサンプル位置</summary>
        private const int BeginSamplePosition = -1;

        /// <summary>カーソルを指し示す対象となる、音声データ サンプルのバッファー配列</summary>
        private readonly float[] targetSamples;
        /// <summary>末尾を指し示すサンプル位置</summary>
        private readonly int endSamplePosition;
        /// <summary>音声データのチャネル数</summary>
        private readonly int channels;
        /// <summary>現在のサンプル位置</summary>
        private int samplePosition;

        /// <summary>
        /// 現在のサンプル位置を取得します。
        /// </summary>
        public int SamplePosition => samplePosition;

        /// <summary>
        /// 現在のフレーム位置を取得します。
        /// </summary>
        public int FramePosition => IsBegin() ? BeginSamplePosition : samplePosition / channels;

        /// <summary>
        /// 指定した音声データ サンプルのバッファーに対して、指定した位置から参照する
        /// <see cref="FloatWaveSampleFrameCursor"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="target">カーソルを指し示す対象となる、音声データ サンプルのバッファー。</param>
        /// <param name="initialPosition">カーソルの初期位置。</param>
        public FloatWaveSampleFrameCursor(FloatWaveSampleBuffer target, CursorPosition initialPosition)
        {
            targetSamples = target.Samples;
            endSamplePosition = targetSamples.Length;
            channels = target.WaveFormat.Channels;

            samplePosition = initialPosition == CursorPosition.Begin ? BeginSamplePosition : endSamplePosition;
        }

        /// <summary>
        /// カーソルのフレーム位置を 1 フレーム次に移動します。
        /// </summary>
        /// <returns>移動先のフレーム位置に音声データがある場合は true、末尾に到達して移動先のフレーム位置に音声データがない場合は false。</returns>
        public bool MoveNext()
        {
            samplePosition = IsBegin() ? 0 : samplePosition + channels;
            if (IsEnd())
            {
                samplePosition = endSamplePosition;
                return false;
            }
            return true;
        }

        /// <summary>
        /// カーソルのフレーム位置を 1 フレーム前に移動します。
        /// </summary>
        /// <returns>移動先のフレーム位置に音声データがある場合は true、先頭に到達して移動先のフレーム位置に音声データがない場合は false。</returns>
        public bool MovePrevious()
        {
            samplePosition -= channels;
            if (IsBegin())
            {
                samplePosition = BeginSamplePosition;
                return false;
            }
            return true;
        }

        /// <summary>
        /// カーソルのフレーム位置が先頭 (音声データより 1 フレーム前の何もない位置) を指している事を判定します。
        /// </summary>
        /// <returns>カーソルのフレーム位置が先頭を指している場合は true、そうでない場合は false。</returns>
        public bool IsBegin()
            => samplePosition <= BeginSamplePosition;

        /// <summary>
        /// カーソルのフレーム位置が末尾 (音声データより 1 フレーム次の何もない位置) を指している事を判定します。
        /// </summary>
        /// <returns>カーソルのフレーム位置が末尾を指している場合は true、そうでない場合は false。</returns>
        public bool IsEnd()
            => samplePosition >= endSamplePosition;

        /// <summary>
        /// カーソルが指し示すフレームが無音のフレームである事を判定します。
        /// </summary>
        /// <returns></returns>
        public bool IsSilence()
        {
            if (IsBegin() || IsEnd())
            {
                throw new InvalidOperationException("カーソルの位置が音声データの先頭または末尾の範囲外を指しています。");
            }

            return targetSamples[samplePosition..(samplePosition + channels)].All(f => f == 0f);
        }

        /// <summary>
        /// カーソルが指し示すフレームのサンプルに対して指定した倍率で増幅します。
        /// </summary>
        /// <param name="rate">増幅率。</param>
        public void Amplify(float rate)
        {
            if (rate < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(rate), "増幅率がマイナスです。");
            }
            
            for (int offset = 0; offset < channels; offset++)
            {
                targetSamples[samplePosition + offset] *= rate;
            }
        }
    }
}
