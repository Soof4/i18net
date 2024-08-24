namespace i18net
{
    internal static class FileManager
    {
        internal static FileStream? OpenFile(string dir, FileMode mode)
        {
            if (File.Exists(dir))
            {
                return File.Open(dir, mode);
            }

            return null;
        }

        internal static FileStream OpenFile(string dir, FileMode mode, Action fileGenerator)
        {
            if (File.Exists(dir))
            {
                return File.Open(dir, mode);
            }

            fileGenerator.Invoke();

            return File.Open(dir, mode);
        }
    }
}