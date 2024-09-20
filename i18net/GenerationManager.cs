using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace i18net
{
    internal static class GenerationManager
    {
        private static readonly Regex PairRegex = new Regex(@"\s*(\""[^\""]+\"")\s*:\s*(\""[^\""]+\"")\s*");

        internal static void GenerateLocalization()
        {
            string path = Path.Join(Configuration.Config.LanguageFilesDir, Configuration.Config.DefaultLang + ".json");

            Dictionary<string, string>? kvps = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));

            // If kvps is null then make it empty dictionary so we can generate empty Localization class
            if (kvps == null)
            {
                kvps = new Dictionary<string, string>();
            }

            FileStream wfs = File.Open(Path.Combine(Configuration.Config.BaseDir, "Localization.cs"), FileMode.Create);
            StreamWriter sw = new StreamWriter(wfs);

            sw.WriteLine($"namespace {Configuration.Config.Namespace} \n{{");
            sw.WriteLine("    public static class Localization\n    {");

            foreach (var kvp in kvps)
            {
                sw.WriteLine($"        public static string {kvp.Key} = \"{kvp.Value}\";");
            }

            sw.WriteLine("    }\n}");

            sw.Close();
            wfs.Close();

        }

        internal static void GenerateLocalizationManager()
        {
            FileStream wfs = File.Open(Path.Combine(Configuration.Config.BaseDir, "LocalizationManager.cs"), FileMode.Create);
            StreamWriter sw = new StreamWriter(wfs);

            sw = sw
                .PrintLine("using System.Reflection;")
                .PrintLine("using System.Text.RegularExpressions;")
                .PrintLine($"namespace {Configuration.Config.Namespace}")
                .PrintLine("{")
                .PrintLine(1, "public static class LocalizationManager")
                .PrintLine(1, "{")
                .PrintLine(2, "private static readonly Regex PairRegex = new Regex(@\"\\s*(\\\"\"[^\\\"\"]+\\\"\")\\s*:\\s*(\\\"\"[^\\\"\"]+\\\"\")\\s*\");")
                .PrintLine(2, $"private static string _curLang = \"{Configuration.Config.DefaultLang}\";")
                .PrintLine(2, "public static void LoadLanguage(string langCode)")
                .PrintLine(2, "{")
                .PrintLine(3, "FileStream rfs;")
                .PrintLine(3, "StreamReader sr;")
                .PrintLine("")
                .PrintLine(3, "try")
                .PrintLine(3, "{")
                .PrintLine(4, $"rfs = File.Open(\"{Configuration.Config.LanguageFilesDir}/\" + langCode + \".json\", FileMode.Open);")
                .PrintLine(4, "sr = new StreamReader(rfs);")
                .PrintLine(3, "}")
                .PrintLine(3, "catch")
                .PrintLine(3, "{")
                .PrintLine(4, "return;")
                .PrintLine(3, "}")
                .PrintLine("")
                .PrintLine(3, "Assembly assembly = Assembly.GetExecutingAssembly();")
                .PrintLine(3, $"var type = assembly.GetType(\"{Configuration.Config.Namespace}.Localization\");")
                .PrintLine("")
                .PrintLine(3, "if (type == null) return;")
                .PrintLine("")
                .PrintLine(3, "while (true)")
                .PrintLine(3, "{")
                .PrintLine(4, "string? line = sr.ReadLine();")
                .PrintLine("")
                .PrintLine(4, "if (line == null) break;")
                .PrintLine(4, "if (!PairRegex.IsMatch(line)) continue;")
                .PrintLine("")
                .PrintLine(4, "string[] pair = line.Split(\":\");")
                .PrintLine(4, "string key = pair[0].Trim().Trim('\"');")
                .PrintLine(4, "string value = pair[1].Trim().Trim(',').Trim('\"');")
                .PrintLine("")
                .PrintLine(4, "FieldInfo? field = type.GetField(key, BindingFlags.Static | BindingFlags.Public);")
                .PrintLine("")
                .PrintLine(4, "if (field == null) continue;")
                .PrintLine(4, "field.SetValue(null, value);")
                .PrintLine(3, "}")
                .PrintLine(2, "}")
                .PrintLine(1, "}")
                .PrintLine("}");

            sw.Close();
            wfs.Close();
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