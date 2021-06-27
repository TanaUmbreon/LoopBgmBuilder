using System;
using LoopWaveBuilder.Settings;
using NAudio.Wave;

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
            WaveFormat format;
            WaveSampleFrame[] buffer;

            using (var reader = new WaveFileReaderEx(InputFullName))
            {
                format = reader.WaveFormat;
                buffer = reader.ReadToEnd();
            }

            int soundBeginFrames = TrimsBeginingSilence ? GetSoundBeginFrames(buffer) : 0;

            int correctedLoopBeginFrames = Math.Max(LoopBeginFrames, soundBeginFrames);

            int correctedLoopEndFrames = LoopEndFrames == DefaultLoopEnd ? buffer.Length - 1 : LoopEndFrames;
            correctedLoopEndFrames = Math.Min(correctedLoopEndFrames, buffer.Length - 1);
            correctedLoopEndFrames = Math.Max(correctedLoopEndFrames, correctedLoopBeginFrames);

            return new WaveBgm(
                format,
                buffer[soundBeginFrames..correctedLoopBeginFrames],
                buffer[correctedLoopBeginFrames..(correctedLoopEndFrames + 1)]);
        }

        /// <summary>
        /// 指定した音声データから、最初に音が始まるフレーム (音声データの先頭の無音が終了した次のフレーム) 位置を取得します。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static int GetSoundBeginFrames(WaveSampleFrame[] buffer)
        {
            int soundBeginFrames = 0;
            while (soundBeginFrames < buffer.Length && buffer[soundBeginFrames].IsSilence())
            {
                soundBeginFrames++;
            }
            if (soundBeginFrames >= buffer.Length)
            {
                throw new ApplicationException("全て無音の BGM データです。");
            }
            return soundBeginFrames;
        }
    }
}
