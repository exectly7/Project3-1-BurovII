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
        // Сохраняем оригинальный поток ввода консоли
        private static TextReader? _originalInputStream;
        // Текущий поток, если выбран файл
        public static StreamReader? CurrentReader;

        public static void SwitchInputStreamToFile()
        {
            Console.Write("Введите путь к файлу: ");
            Console.CursorVisible = true;
            try
            {
                string path = Console.ReadLine() ?? string.Empty;
                Console.CursorVisible = false;
                // Создаём и сохраняем поток для файла
                CurrentReader = new StreamReader(path);
                // Сохраняем оригинальный поток, если он ещё не сохранён
                if (_originalInputStream == null)
                {
                    _originalInputStream = Console.In;
                }
                // Перенаправляем ввод консоли на файл
                Console.SetIn(CurrentReader);
            }
            catch (Exception e)
            {
                // Предполагается, что OutputHandler.Message выводит сообщение об ошибке
                OutputHandler.Message("Произошла ошибка при импорте файла.");
                throw new IOException(e.Message, e);
            }
        }

        public static void SwitchInputStreamToConsole()
        {
            // Закрываем файловый поток, если он открыт
            if (CurrentReader != null)
            {
                CurrentReader.Close();
                CurrentReader = null;
            }
            // Возвращаем оригинальный поток ввода, если он был сохранён
            if (_originalInputStream != null)
            {
                Console.SetIn(_originalInputStream);
                _originalInputStream = null;
            }
        }
    }
}
