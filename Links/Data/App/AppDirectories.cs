using System.IO;

namespace Links.Data.App
{
    internal class AppDirectories
    {
        public string Program { get; }
        public string Links { get; }
        public string Themes { get; }
        public string Locales { get; }
        public string Data { get; }

        public bool IsDirectoriesInOrder => Directory.Exists(Links) && Directory.Exists(Themes) &&
            Directory.Exists(Locales) && Directory.Exists(Data);

        public bool IsDirectoriesDefaultFilesInOrder
        {
            get
            {
                try
                {
                    var themesCount = new DirectoryInfo(Themes).GetFiles().Length;
                    var localesCount = new DirectoryInfo(Locales).GetFiles().Length;

                    return themesCount > 0 && localesCount > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public AppDirectories()
        {
            string executablePath = System.Windows.Forms.Application.ExecutablePath;
            string workingDirectory = executablePath.Remove(executablePath.LastIndexOf(@"\"));

            Program = workingDirectory;

            Links = Path.Combine(Program, "Links");
            Themes = Path.Combine(Program, "Themes");
            Locales = Path.Combine(Program, "Locales");
            Data = Path.Combine(Program, "Data");
        }

        public bool TryRestoreDirectories()
        {
            try
            {
                if (!Directory.Exists(Links))
                    Directory.CreateDirectory(Links);

                if (!Directory.Exists(Themes))
                    Directory.CreateDirectory(Themes);

                if (!Directory.Exists(Locales))
                    Directory.CreateDirectory(Locales);

                if (!Directory.Exists(Data))
                    Directory.CreateDirectory(Data);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
