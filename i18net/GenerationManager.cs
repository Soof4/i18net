using System.Text.RegularExpressions;

namespace i18net
{
    internal static class GenerationManager
    {
        private static readonly Regex PairRegex = new Regex(@"\s*(\""[^\""]+\"")\s*:\s*(\""[^\""]+\"")\s*");

        internal static void GenerateLocalization()
        {
            FileStream rfs = FileManager.OpenFile(Configuration.Config.LanguageFilesDir + "/" + Configuration.Config.DefaultLang + ".json", FileMode.Open, GenerateDefaultLangFile);
            StreamReader sr = new StreamReader(rfs);

            FileStream wfs = File.Open(Path.Combine(Configuration.Config.LocalizationFileDir, "Localization.cs"), FileMode.Create);
            StreamWriter sw = new StreamWriter(wfs);

            sw.WriteLine($"namespace {Configuration.Config.Namespace} {{");
            sw.WriteLine("    public static class Localization\n    {");

            while (true)
            {
                string? line = sr.ReadLine();

                if (line == null) break;
                if (!PairRegex.IsMatch(line)) continue;

                string[] pair = line.Split(":");
                string key = pair[0].Trim().Trim('"');
                string value = pair[1].Trim().Trim(',').Trim('"');

                sw.WriteLine($"        public const string {key} = \"{value}\";");
            }

            sw.WriteLine("    }\n}");

            sr.Close();
            rfs.Close();
            sw.Close();
            wfs.Close();
        }

        internal static void GenerateLocalizationManager()
        {
            FileStream wfs;
            StreamWriter sw;


        }

        private static void GenerateDefaultLangFile()
        {
            string path = Configuration.Config.LanguageFilesDir;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, Configuration.Config.DefaultLang + ".json");
            FileStream wfs = File.Open(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(wfs);


            sw.WriteLine("{");
            sw.WriteLine("    \"Title\": \"Hello World!\",");
            sw.WriteLine("    \"Description\": \"This is how you should add new texts.\"");
            sw.WriteLine("}");

            sw.Close();
            wfs.Close();
        }
    }
}