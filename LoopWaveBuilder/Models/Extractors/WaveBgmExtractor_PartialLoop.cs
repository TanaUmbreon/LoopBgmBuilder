using System;
using LoopWaveBuilder.Settings;
using NAudio.Wave;

namespace LoopWaveBuilder.Models.Extractors
{
    /// <summary>
    /// 特定の位置まで再生すると特定の位置からループ再生する構成の WAVE 形式の BGM データを抽出します。
    /// </summary>
    public class WaveBgmExtractor_PartialLoop : IWaveBgmExtractor
    {
        private const int DefaultLoopEnd = -1;

        /// <summary>読み込む WAV ファイルの名前</summary>
        private readonly string inputFileName;
        /// <summary>BGM 先頭の無音をトリミングすることを示すフラグ</summary>
        private readonly bool trimsBeginingSilence;
        /// <summary>ループ再生の開始フレーム位置</summary>
        private readonly int loopBeginFrames;
        /// <summary>ループ再生の終了フレーム位置</summary>
        private readonly int loopEndFrames;

        /// <summary>
        /// <see cref="WaveBgmExtractor_PartialLoop"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="settings">BGM を抽出する為の設定。</param>
        public WaveBgmExtractor_PartialLoop(ExtractionSettings settings)
        {
            inputFileName = settings.InputFileName;
            trimsBeginingSilence = settings.TrimsBeginingSilence ?? false;
            loopBeginFrames = settings.LoopBeginSamples ?? 0;
            loopEndFrames = settings.LoopEndSamples ?? DefaultLoopEnd;
        }

        public WaveBgm Extract()
        {
            WaveFormat format;
            WaveSampleFrame[] buffer;

            using (var reader = new WaveFileReaderEx(inputFileName))
            {
                format = reader.WaveFormat;
                buffer = reader.ReadToEnd();
            }

            int soundBeginFrames = trimsBeginingSilence ? GetSoundBeginFrames(buffer) : 0;

            int correctedLoopBeginFrames = Math.Max(loopBeginFrames, soundBeginFrames);

            int correctedLoopEndFrames = loopEndFrames == DefaultLoopEnd ? buffer.Length - 1 : loopEndFrames;
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
