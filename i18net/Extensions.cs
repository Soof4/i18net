namespace i18net
{
    public static class Extensions
    {
        public static StreamWriter PrintLine(this StreamWriter writer, int indent, string text)
        {
            string spaces = "";

            for (int i = 0; i < indent; i++)
            {
                spaces += "    ";
            }

            writer.WriteLine(spaces + text);
            return writer;
        }

        public static StreamWriter PrintLine(this StreamWriter writer, string text)
        {
            writer.WriteLine(text);
            return writer;
        }
    }
}