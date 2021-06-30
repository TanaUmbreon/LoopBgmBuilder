namespace LoopBgmBuilder.FormModels
{
    /// <summary>
    /// <see cref="MainFormModel"/> の状態を表します。
    /// </summary>
    public enum MainFormModelState
    {
        /// <summary>初期化完了</summary>
        Initialized,
        /// <summary>ループ加工設定ファイルの読み込みに成功</summary>
        LoadSettingsSuccessful,
        /// <summary>ループ加工設定ファイルの読み込みに失敗</summary>
        LoadSettingsFailed,
        /// <summary>実行中</summary>
        Executing,
        /// <summary>実行完了</summary>
        Executed,
    }
}
