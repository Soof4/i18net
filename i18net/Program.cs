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
            catch (Exception e)
            {
                Print($"i18net could not read the default language file.\n{e.Message}", ConsoleColor.Red);
                return;
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