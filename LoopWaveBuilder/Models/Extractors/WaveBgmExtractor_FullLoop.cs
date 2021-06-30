using System;
using LoopWaveBuilder.Settings;

namespace LoopWaveBuilder.Models.Extractors
{
    /// <summary>
    /// 末尾まで再生すると先頭からループ再生する構成の WAVE 形式の BGM データを抽出します。
    /// </summary>
    public class WaveBgmExtractor_FullLoop : WaveBgmExtractorBase
    {
        /// <summary>
        /// BGM 先頭の無音をトリミングすることを示す値を取得します。
        /// </summary>
        public bool TrimsBeginingSilence { get; }
        
        /// <summary>
        /// BGM 末尾の無音をトリミングすることを示す値を取得します。
        /// </summary>
        public bool TrimsEndingSilence { get; }

        /// <summary>
        /// <see cref="WaveBgmExtractor_FullLoop"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="settings">BGM データを抽出するための設定。</param>
        public WaveBgmExtractor_FullLoop(ExtractionSettings settings)
            : base(settings)
        {
            TrimsBeginingSilence = settings.TrimsBeginingSilence ?? false;
            TrimsEndingSilence = settings.TrimsEndingSilence ?? false;
        }

        public override WaveBgm Extract()
        {
            var buffer = new FloatWaveSampleBuffer(InputFullName);

            int soundBeginSamples = TrimsBeginingSilence ? GetSoundBeginSamples(buffer) : 0;
            int soundEndSamples = TrimsEndingSilence ? GetSoundEndSamples(buffer) : buffer.SampleCount;

            return new WaveBgm(buffer.WaveFormat,
            Array.Empty<float>(),
            buffer.Samples[soundBeginSamples..soundEndSamples]);
        }

        /// <summary>
        /// 指定したバッファーから、最初に音が始まる包括的なサンプル位置を取得します。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static int GetSoundBeginSamples(FloatWaveSampleBuffer buffer)
        {
            FloatWaveSampleFrameCursor cursor = buffer.CreateFrameCursor(CursorPosition.Begin);
            while (cursor.MoveNext() && cursor.IsSilence()) { }
            if (cursor.IsEnd())
            {
                throw new ApplicationException("全て無音の BGM データです。");
            }
            return cursor.SamplePosition;
        }

        /// <summary>
        /// 指定したバッファーから、最後に音が終わる排他的なサンプル位置を取得します。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static int GetSoundEndSamples(FloatWaveSampleBuffer buffer)
        {
            FloatWaveSampleFrameCursor cursor = buffer.CreateFrameCursor(CursorPosition.End);
            while (cursor.MovePrevious() && cursor.IsSilence()) { }
            if (cursor.IsBegin())
            {
                throw new ApplicationException("全て無音の BGM データです。");
            }
            cursor.MoveNext();
            return cursor.SamplePosition;
        }
    }
}
