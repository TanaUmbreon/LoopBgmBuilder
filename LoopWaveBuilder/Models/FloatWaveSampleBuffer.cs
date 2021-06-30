using NAudio.Wave;

namespace LoopWaveBuilder.Models
{
    /// <summary>
    /// 32 ビット浮動小数点数方式で WAVE 形式の音声データ サンプルを扱うバッファーです。
    /// </summary>
    public class FloatWaveSampleBuffer
    {
        /// <summary>
        /// 音声データのサンプル配列を取得します。
        /// </summary>
        public float[] Samples { get; }

        /// <summary>
        /// WAVE 形式のフォーマット情報を取得します。
        /// </summary>
        public WaveFormat WaveFormat { get; }

        /// <summary>
        /// 音声データのサンプル数を取得します。
        /// </summary>
        public int SampleCount => Samples.Length;

        /// <summary>
        /// 指定した WAVE ファイルを読み込み、
        /// <see cref="FloatWaveSampleBuffer"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="waveFile">読み込む WAVE ファイルのパス。</param>
        public FloatWaveSampleBuffer(string waveFile)
        {
            using var reader = new WaveFileReader(waveFile);

            Samples = new float[reader.Length / (reader.WaveFormat.BitsPerSample / 8L)];
            WaveFormat = reader.WaveFormat;

            int length = Samples.Length;
            int channels = WaveFormat.Channels;
            for (int i = 0; i < length; i += channels)
            {
                float[] frame = reader.ReadNextSampleFrame();
                for (int offset = 0; offset < channels; offset++)
                {
                    Samples[i + offset] = frame[offset];
                }
            }
        }

        /// <summary>
        /// 音声データのサンプル配列およびフォーマット情報を指定して
        /// <see cref="FloatWaveSampleBuffer"/> の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="samples">音声データのサンプル配列。</param>
        /// <param name="format">WAVE 形式のフォーマット情報。</param>
        public FloatWaveSampleBuffer(float[] samples, WaveFormat format)
        {
            Samples = samples;
            WaveFormat = format;
        }

        /// <summary>
        /// 指定した初期位置からサンプルをフレーム単位で参照するカーソルを生成します。
        /// </summary>
        /// <param name="initialPosition">カーソルの初期位置。</param>
        /// <returns>サンプルをフレーム単位で参照するカーソル。</returns>
        public FloatWaveSampleFrameCursor CreateFrameCursor(CursorPosition initialPosition)
            => new(this, initialPosition);
    }
}
