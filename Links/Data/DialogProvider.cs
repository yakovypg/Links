using System.Windows.Forms;

namespace Links.Data
{
    internal static class DialogProvider
    {
        private static readonly OpenFileDialog _openFileDialog;
        private static readonly SaveFileDialog _saveFileDialog;

        static DialogProvider()
        {
            _openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true,
                Multiselect = false,

                FilterIndex = 0,
                Filter = "all files|*.*|jpg files|*.jpg|jpeg files|*.jpeg|png files|*.png|bmp files|*.bmp|links files|*.groups"
            };

            _saveFileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                RestoreDirectory = true,

                FilterIndex = 0,
                DefaultExt = "json",
                Filter = "links files|*.groups|all files|*.*"
            };
        }

        public static DialogResult GetFilePath(out string path)
        {
            DialogResult dialogResult = _openFileDialog.ShowDialog();

            path = dialogResult == DialogResult.OK
                ? _openFileDialog.FileName
                : null;

            return dialogResult;
        }

        public static DialogResult GetSavingFilePath(out string path)
        {
            DialogResult dialogResult = _saveFileDialog.ShowDialog();

            path = dialogResult == DialogResult.OK
                ? _saveFileDialog.FileName
                : null;

            return dialogResult;
        }
    }
}
