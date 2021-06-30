using System.Diagnostics;
using System.Reflection;

namespace LoopBgmBuilder
{
    /// <summary>
    /// このアプリケーションに関するアセンブリ情報を参照するユーティリティ クラスです。
    /// </summary>
    internal static class AssemblyInfo
    {
        /// <summary>現在実行中のコードを格納しているアセンブリのファイル バージョン情報</summary>
        private static readonly FileVersionInfo versionInfo;

        /// <summary>
        /// アセンブリのタイトル情報を取得します。
        /// </summary>
        public static string Title => versionInfo.FileDescription ?? "";

        static AssemblyInfo()
        {
            versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        }
    }
}
