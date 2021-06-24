using System;
using System.Collections.Generic;
using LoopWaveBuilder.Settings;
using NAudio.Wave;

namespace LoopWaveBuilder.Models.Extractors
{
    /// <summary>
    /// 末尾まで再生すると先頭からループ再生する構成の WAVE 形式の BGM データを抽出します。
    /// </summary>
    public class WaveBgmExtractor_FullLoop : IWaveBgmExtractor
    {
        /// <summary>読み込む WAV ファイルの名前</summary>
        private readonly string inputFileName;
        /// <summary>BGM 先頭の無音をトリミングすることを示すフラグ</summary>
        private readonly bool trimsBeginingSilence;
        /// <summary>BGM 末尾の無音をトリミングすることを示すフラグ</summary>
        private readonly bool trimsEndingSilence;

        /// <summary>
        /// <see cref="WaveBgmExtractor_FullLoop"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="settings">BGM を抽出する為の設定。</param>
        public WaveBgmExtractor_FullLoop(ExtractionSettings settings)
        {
            inputFileName = settings.InputFileName;
            trimsBeginingSilence = settings.TrimsBeginingSilence ?? false;
            trimsEndingSilence = settings.TrimsEndingSilence ?? false;
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
            int soundEndFrames = trimsEndingSilence ? GetSoundEndFrames(buffer) : buffer.Length - 1;

            return new WaveBgm(format,
                Array.Empty<WaveSampleFrame>(),
                buffer[soundBeginFrames..(soundEndFrames + 1)]);
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

        /// <summary>
        /// 指定した音声データから、最後に音が終わるフレーム (音声データの末尾の無音が始まる前のフレーム) 位置を取得します。
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static int GetSoundEndFrames(WaveSampleFrame[] buffer)
        {
            int soundEndFrames = buffer.Length - 1;
            while (soundEndFrames >= 0 && buffer[soundEndFrames].IsSilence())
            {
                soundEndFrames--;
            }
            if (soundEndFrames < 0)
            {
                throw new ApplicationException("全て無音の BGM データです。");
            }
            return soundEndFrames;
        }
    }
}
