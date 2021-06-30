using System;
using System.Windows.Forms;
using LoopBgmBuilder.Forms;

namespace LoopBgmBuilder
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                TaskDialog.ShowDialog(new TaskDialogPage()
                {
                    Caption = AssemblyInfo.Title,
                    Icon = TaskDialogIcon.Error,
                    Heading = $"{AssemblyInfo.Title} Ç≈èdëÂÇ»ñ‚ëËÇ™î≠ê∂ÇµÇΩà◊ÅAíºÇøÇ…èIóπÇµÇ‹Ç∑",
                    Text = ex.Message,
                    Buttons = { TaskDialogButton.Close }
                });
            }
        }
    }
}
