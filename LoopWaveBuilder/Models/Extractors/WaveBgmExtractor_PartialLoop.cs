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
        private readonly string inputFileName;
        private readonly bool trimsBeginingSilence;

        /// <summary>
        /// <see cref="WaveBgmExtractor_PartialLoop"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="settings">BGM を抽出する為の設定。</param>
        public WaveBgmExtractor_PartialLoop(ExtractionSettings settings)
        {
            inputFileName = settings.InputFileName;
            trimsBeginingSilence = settings.TrimsBeginingSilence ?? false;
        }

        public WaveBgm Extract()
        {
            byte[] buffer;
            WaveFormat format;

            using (var reader = new WaveFileReader(inputFileName))
            {
                buffer = new byte[reader.Length];
                reader.Read(buffer, 0, buffer.Length);
                format = reader.WaveFormat;
            }

            if (trimsBeginingSilence)
            {

            }

            throw new NotImplementedException();
            //return new WaveBgm(format, Array.Empty<byte>(), buffer);
        }
    }
}
