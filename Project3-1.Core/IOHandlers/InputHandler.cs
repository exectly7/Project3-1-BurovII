using System;
using System.IO;

namespace Project3_1.Core.IOHandlers
{
    /// <summary>
    /// Класс занимающийся вводом.
    /// </summary>
    public static class InputHandler
    {
        private static TextReader? _originalInputStream;
        /// <summary>
        /// Хранит текущий поток ввода.
        /// </summary>
        public static StreamReader? CurrentReader;

        /// <summary>
        /// Меняет поток ввода в файл.
        /// </summary>
        /// <exception cref="IOException">Не удалось перенаправить поток.</exception>
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

        /// <summary>
        /// Возвращает поток в консоль.
        /// </summary>
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
