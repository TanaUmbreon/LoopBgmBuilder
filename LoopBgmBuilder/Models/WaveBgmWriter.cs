using System;
using System.Collections.Generic;
using System.Linq;
using LoopBgmBuilder.Settings;
using NAudio.Wave;

namespace LoopBgmBuilder.Models
{
    /// <summary>
    /// WAVE 形式の BGM データを書き出します。
    /// </summary>
    public class WaveBgmWriter : IDisposable
    {
        private readonly WaveFileWriter writer;
        private readonly WaveBgm bgm;
        private readonly DefaultOutputFormatSettings outputFormat;

        /// <summary>
        /// <see cref="WaveBgmWriter"/> の新しいインスタンスを生成します。
        /// </summary>
        public WaveBgmWriter(string fileName, WaveBgm bgm, DefaultOutputFormatSettings defaultFormat) 
        {
            writer = new WaveFileWriter(fileName, bgm.WaveFormat);
            this.bgm = bgm;
            outputFormat = defaultFormat;
        }

        #region Dispose パターン

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    writer.Dispose();
                }
                disposedValue = true;
            }
        }

        // // TODO: 'Dispose(bool disposing)' にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします
        // ~WaveBgmWriter()
        // {
        //     // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// WAVE 形式で BGM データを書き出します。
        /// </summary>
        public void Write()
        {
            WriteBeginingSilencePart();
            WriteBeginingPart();
            WriteLoopPart();

            int offsetSamples = 0;
            offsetSamples = WriteFadeoutDelayPart(offsetSamples);
            WriteFadeoutPart(offsetSamples);

            WriteEndingSilencePart();
        }

        private void WriteBeginingSilencePart()
        {
            if (outputFormat.BeginingSilenceMiliseconds <= 0) { return; }

            // チャネル数の倍数となるように計算
            int samples = (outputFormat.BeginingSilenceMiliseconds * bgm.WaveFormat.SampleRate / 1000) * bgm.WaveFormat.Channels;
            writer.WriteSamples(new float[samples], 0, samples);
        }

        private void WriteBeginingPart()
        {
            if (bgm.BeginingPartSamples.Length <= 0) { return; }

            writer.WriteSamples(bgm.BeginingPartSamples, 0, bgm.BeginingPartSamples.Length);
        }

        private void WriteLoopPart()
        {
            if (bgm.LoopPartSamples.Length <= 0 || outputFormat.LoopCount <= 0) { return; }

            for (int loopCount = 0; loopCount < outputFormat.LoopCount; loopCount++)
            {
                writer.WriteSamples(bgm.LoopPartSamples, 0, bgm.LoopPartSamples.Length);
            }
        }

        private int WriteFadeoutDelayPart(int offsetSamples)
        {
            if (bgm.LoopPartSamples.Length <= 0 || outputFormat.FadeoutDelayMiliseconds <= 0) { return offsetSamples; }

            int samples = (outputFormat.FadeoutDelayMiliseconds * bgm.WaveFormat.SampleRate / 1000) * bgm.WaveFormat.Channels;
            while (samples > 0)
            {
                int length = Math.Min(samples, bgm.LoopPartSamples.Length);
                writer.WriteSamples(bgm.LoopPartSamples, offsetSamples, length);
                samples -= length;
                offsetSamples = (offsetSamples + length) % bgm.LoopPartSamples.Length;
            }

            return offsetSamples;
        }

        private void WriteFadeoutPart(int offsetSamples)
        {
            if (bgm.LoopPartSamples.Length <= 0 || outputFormat.FadeoutMiliseconds <= 0) { return; }

            int samples = (outputFormat.FadeoutMiliseconds * bgm.WaveFormat.SampleRate / 1000) * bgm.WaveFormat.Channels;
            float denominator = samples;
            while (samples > 0)
            {
                int bufferLength = Math.Min(samples, bgm.LoopPartSamples.Length);
                var buffer = new FloatWaveSampleBuffer(
                    bgm.LoopPartSamples[offsetSamples..(offsetSamples + bufferLength)],
                    bgm.WaveFormat);
                var cursor = new FloatWaveSampleFrameCursor(buffer, CursorPosition.Begin);

                while (cursor.MoveNext())
                {
                    // フェードアウト二重掛け
                    cursor.Amplify(samples / denominator);
                    cursor.Amplify(samples / denominator);
                    samples -= bgm.WaveFormat.Channels;
                }

                writer.WriteSamples(buffer.Samples, 0, buffer.Samples.Length);

                offsetSamples = (offsetSamples + bufferLength) % bgm.LoopPartSamples.Length;
            }
        }

        private void WriteEndingSilencePart()
        {
            if (outputFormat.EndingSilenceMiliseconds <= 0) { return; }

            int samples = (outputFormat.EndingSilenceMiliseconds * bgm.WaveFormat.SampleRate / 1000) * bgm.WaveFormat.Channels;
            writer.WriteSamples(new float[samples], 0, samples);
        }
    }
}
