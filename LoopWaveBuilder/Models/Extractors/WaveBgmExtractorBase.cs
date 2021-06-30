using System.IO;
using LoopBgmBuilder.Settings;

namespace LoopBgmBuilder.Models.Extractors
{
    public abstract class WaveBgmExtractorBase : IWaveBgmExtractor
    {
        public string InputFullName { get; }
        public string InputFolderPath { get; }
        public string InputFileName { get; }
        public string ExtractorName { get; }

        /// <summary>
        /// <see cref="WaveBgmExtractorBase"/> のコンストラクタ。
        /// </summary>
        /// <param name="settings">BGM データを抽出するための設定。</param>
        public WaveBgmExtractorBase(ExtractionSettings settings)
        {
            var file = new FileInfo(settings.InputFileName);
            InputFullName = file.FullName;
            InputFolderPath = file.DirectoryName ?? "";
            InputFileName = file.Name;
            ExtractorName = settings.ExtractorName;
        }

        public abstract WaveBgm Extract();
    }
}
