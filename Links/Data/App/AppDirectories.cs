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
    }
}
