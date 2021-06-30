using System;
using LoopWaveBuilder.Settings;

namespace LoopWaveBuilder.Models.Extractors
{
    /// <summary>
    /// 特定の位置まで再生すると特定の位置からループ再生する構成の WAVE 形式の BGM データを抽出します。
    /// </summary>
    public class WaveBgmExtractor_PartialLoop : WaveBgmExtractorBase
    {
        private const int DefaultLoopEnd = -1;

        /// <summary>
        /// BGM 先頭の無音をトリミングすることを示す値を取得します。
        /// </summary>
        public bool TrimsBeginingSilence { get; }

        /// <summary>
        /// ループ再生の開始フレーム位置を取得します。
        /// </summary>
        public int LoopBeginFrames { get; }

        /// <summary>
        /// ループ再生の終了フレーム位置を取得します。
        /// </summary>
        public int LoopEndFrames { get; }

        /// <summary>
        /// <see cref="WaveBgmExtractor_PartialLoop"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="settings">BGM データを抽出するための設定。</param>
        public WaveBgmExtractor_PartialLoop(ExtractionSettings settings)
            : base(settings)
        {
            TrimsBeginingSilence = settings.TrimsBeginingSilence ?? false;
            LoopBeginFrames = settings.LoopBeginSamples ?? 0;
            LoopEndFrames = settings.LoopEndSamples ?? DefaultLoopEnd;
        }

        public override WaveBgm Extract()
        {
            var buffer = new FloatWaveSampleBuffer(InputFullName);

            int soundBeginSamples = TrimsBeginingSilence ? GetSoundBeginSamples(buffer) : 0;

            int correctedLoopBeginSamples = Math.Max(LoopBeginFrames * buffer.WaveFormat.Channels, soundBeginSamples);

            int correctedLoopEndSamples = LoopEndFrames == DefaultLoopEnd ? buffer.SampleCount : LoopEndFrames * buffer.WaveFormat.Channels;
            correctedLoopEndSamples = Math.Min(correctedLoopEndSamples, buffer.SampleCount);
            correctedLoopEndSamples = Math.Max(correctedLoopEndSamples, correctedLoopBeginSamples);

            return new WaveBgm(
                buffer.WaveFormat,
                buffer.Samples[soundBeginSamples..correctedLoopBeginSamples],
                buffer.Samples[correctedLoopBeginSamples..correctedLoopEndSamples]);
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
    }
}
