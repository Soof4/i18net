namespace i18net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Configuration.Reload();
                GenerationManager.GenerateLocalization();
                GenerationManager.GenerateLocalizationManager();
            }
            catch
            {
                GenerationManager.GenerateDefaultLangFile();
                GenerationManager.GenerateLocalization();
                GenerationManager.GenerateLocalizationManager();
                Print("i18net could not find the default language file. Thus, generated the example language file.", ConsoleColor.Yellow);
            }

            Print("i18net successfully generated the Localization.cs file.", ConsoleColor.Green);
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}