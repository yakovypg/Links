using System.Windows.Forms;

namespace Links.Data
{
    internal static class DialogProvider
    {
        private static readonly OpenFileDialog _openFileDialog;

        static DialogProvider()
        {
            _openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                Multiselect = false,

                FilterIndex = 0,
                Filter = "all files|*.*|jpg files|*.jpg|jpeg files|*.jpeg|png files|*.png|bmp files|*.bmp"
            };
        }

        public static string GetFilePath()
        {
            return _openFileDialog.ShowDialog() == DialogResult.OK
                ? _openFileDialog.FileName
                : null;
        }
    }
}
