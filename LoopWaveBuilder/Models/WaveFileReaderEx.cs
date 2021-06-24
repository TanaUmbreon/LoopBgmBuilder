using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace LoopWaveBuilder.Models
{
    /// <summary>
    /// WAV ファイルの読み込みをサポートします。
    /// </summary>
    public class WaveFileReaderEx : WaveFileReader
    {
        /// <summary>
        /// <see cref="WaveFileReaderEx"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="waveFile"></param>
        public WaveFileReaderEx(string waveFile) : base(waveFile) { }

        /// <summary>
        /// 現在の位置から最後まで WAVE ファイルを読み込み、<see cref="WaveSampleFrame"/> の配列に変換して返します。
        /// </summary>
        /// <returns></returns>
        public WaveSampleFrame[] ReadToEnd()
        {
            int frames = (int)(Length / WaveFormat.Channels / WaveFormat.BitsPerSample / 8L);
            var buffer = new List<WaveSampleFrame>(frames);

            while (Position < Length)
            {
                buffer.Add(new WaveSampleFrame(ReadNextSampleFrame()));
            }

            return buffer.ToArray();
        }
    }
}
