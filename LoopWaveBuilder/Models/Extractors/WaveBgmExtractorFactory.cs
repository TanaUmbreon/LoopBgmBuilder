using System;
using LoopBgmBuilder.Settings;

namespace LoopBgmBuilder.Models.Extractors
{
    /// <summary>
    /// <see cref="IWaveBgmExtractor"/> を実装するオブジェクトを動的に生成します。
    /// </summary>
    public static class WaveBgmExtractorFactory
    {
        /// <summary></summary>
        private const string ExtractorPrefixName = "LoopBgmBuilder.Models.Extractors.WaveBgmExtractor_";

        /// <summary>
        /// 指定した名前を持つ BGM データ抽出オブジェクトのインスタンスを生成します。
        /// </summary>
        /// <param name="name">BGM データ抽出パターンの名前。</param>
        /// <returns></returns>
        public static IWaveBgmExtractor Create(ExtractionSettings settings)
        {
            Type extractorType = Type.GetType($"{ExtractorPrefixName}{settings.ExtractorName}")
                ?? throw new ArgumentException($"設定された BGM データの抽出パターン名 '{settings.ExtractorName}' は定義されていません。");

            return Activator.CreateInstance(extractorType, settings) as IWaveBgmExtractor
                ?? throw new InvalidCastException();
        }
    }
}
