using System;
using LoopWaveBuilder.Settings;
using NAudio.Wave;

namespace LoopWaveBuilder.Models.Extractors
{
    /// <summary>
    /// 末尾まで再生すると先頭からループ再生する構成の WAVE 形式の BGM データを抽出します。
    /// </summary>
    public class WaveBgmExtractor_FullLoop : IWaveBgmExtractor
    {
        private readonly string inputFileName;
        private readonly bool trimsBeginingSilence;
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

            if (trimsEndingSilence)
            {

            }

            throw new NotImplementedException();
            //return new WaveBgm(format, Array.Empty<byte>(), buffer);
        }
    }
}
