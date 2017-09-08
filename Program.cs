using System;
using System.Windows.Forms;
using HtmlElementsDBEditor.Properties;

namespace HtmlElementsDBEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new FrameWindow());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.UnhandledExceptionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
