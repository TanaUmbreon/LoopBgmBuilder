namespace LoopWaveBuilder.FormModels
{
    /// <summary>
    /// <see cref="MainFormModel"/> の状態を表します。
    /// </summary>
    public enum MainFormModelState
    {
        /// <summary>初期化完了</summary>
        Initialized,
        /// <summary>設定ファイルの読み込み完了</summary>
        LoadedSettings,
        /// <summary>実行中</summary>
        Executing,
        /// <summary>実行完了</summary>
        Executed,
    }
}
