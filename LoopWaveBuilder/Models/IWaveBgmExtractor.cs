namespace LoopWaveBuilder.Models
{
    /// <summary>
    ///  WAVE 形式の BGM データの抽出をサポートします。
    /// </summary>
    public interface IWaveBgmExtractor
    {
        /// <summary>
        /// 入力ファイルの完全パスを取得します。
        /// </summary>
        string InputFullName { get; }

        /// <summary>
        /// 入力ファイルが格納されているフォルダーのパスを取得します。
        /// </summary>
        string InputFolderPath { get; }

        /// <summary>
        /// 入力ファイルの名前を取得します。
        /// </summary>
        string InputFileName { get; }

        /// <summary>
        /// BGM 抽出パターン名を取得します。
        /// </summary>
        string ExtractorName { get; }

        /// <summary>
        /// WAVE 形式の BGM データを抽出します。
        /// </summary>
        /// <returns></returns>
        WaveBgm Extract();
    }
}
