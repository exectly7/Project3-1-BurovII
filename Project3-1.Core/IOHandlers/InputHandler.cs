/*namespace Project3_1.Core.IOHandlers
{
    public static class InputHandler
    {
        private static TextReader? OriginalInputStream;
        public static StreamReader? CurrentReader;

        public static void SwitchInputStreamToFile()
        {
            Console.Write("Введите путь к файлу: ");
            
            try
            {
                string path = Console.ReadLine() ?? string.Empty;
                StreamReader streamReader = new StreamReader(path);
                OriginalInputStream = Console.In;
                Console.SetIn(streamReader);
            }
            catch (Exception e)
            {
                OutputHandler.Message("Произошла ошибка при импорте файла.");
                throw new IOException(e.Message);
            }
            
            return;
        }

        public static void SwitchInputStreamToConsole()
        {
            if (CurrentReader != null)
            {
                CurrentReader.Close();
            }
            Console.SetIn(OriginalInputStream);
            OriginalInputStream = null;
        }
    }
}*/
using System;
using System.IO;

namespace Project3_1.Core.IOHandlers
{
    public static class InputHandler
    {
        private static TextReader? _originalInputStream;
        public static StreamReader? CurrentReader;

        public static void SwitchInputStreamToFile()
        {
            Console.Write("Введите путь к файлу: ");
            Console.CursorVisible = true;
            try
            {
                string path = Console.ReadLine() ?? string.Empty;
                Console.CursorVisible = false;
                CurrentReader = new StreamReader(path);
                if (_originalInputStream == null)
                {
                    _originalInputStream = Console.In;
                }
                Console.SetIn(CurrentReader);
            }
            catch (Exception e)
            {
                OutputHandler.Message("Произошла ошибка при импорте файла.");
                throw new IOException(e.Message, e);
            }
        }

        public static void SwitchInputStreamToConsole()
        {
            if (CurrentReader != null)
            {
                CurrentReader.Close();
                CurrentReader = null;
            }
            if (_originalInputStream != null)
            {
                Console.SetIn(_originalInputStream);
                _originalInputStream = null;
            }
        }
    }
}
