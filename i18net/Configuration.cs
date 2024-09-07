using Newtonsoft.Json;

namespace i18net
{
    internal class Configuration
    {
        internal static string ConfigPath = Path.Combine("i18net", "i18net.config.json");
        internal static Configuration Config = new Configuration();

        public string Namespace = "my_namespace";
        public string DefaultLang = "en_US";
        public string BaseDir = "i18net";
        public string LanguageFilesDir = "lang";

        public static void Reload()
        {
            Config = Read();
        }

        public static Configuration Read()
        {
            Configuration? c = null;

            if (File.Exists(ConfigPath))
            {
                c = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(ConfigPath));
            }

            if (c == null)
            {
                c = new Configuration();
                File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(c, Formatting.Indented));
            }

            return c;
        }

        public void Write()
        {
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}