using System;
using LoopWaveBuilder.Settings;
using NAudio.Wave;

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
            WaveFormat format;
            WaveSampleFrame[] buffer;

            using (var reader = new WaveFileReaderEx(InputFullName))
            {
                format = reader.WaveFormat;
                buffer = reader.ReadToEnd();
            }

            int soundBeginFrames = TrimsBeginingSilence ? GetSoundBeginFrames(buffer) : 0;
            int soundEndFrames = TrimsEndingSilence ? GetSoundEndFrames(buffer) : buffer.Length - 1;

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
