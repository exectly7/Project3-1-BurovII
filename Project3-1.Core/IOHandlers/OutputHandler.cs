namespace Project3_1.Core.IOHandlers
{
    /// <summary>
    /// Класс для вывода данных.
    /// </summary>
    public static class OutputHandler
    {
        private static TextWriter? _originalOutputStream;
        
        /// <summary>
        /// Хранит текущий поток вывода.
        /// </summary>
        public static StreamWriter? CurrentWriter;
        
        /// <summary>
        /// Выводит гайд по меню.
        /// </summary>
        public static void MenuGuide()
        {
            Console.Clear();
            Console.WriteLine(@"
 /\_/\  
( o.o ) 
 > ^ <
 ");
 
            Console.WriteLine("Управление:");
            Console.WriteLine("↑ ↓ - Навигация по меню");
            Console.WriteLine("Enter - Выбор");
            Console.WriteLine("Backspace - Назад");
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы выйти...");
            Console.ReadLine();
        }

        /// <summary>
        /// Выводит message в консоль и ждет нажатия enter.
        /// </summary>
        /// <param name="message">Сообщение для вывода.</param>
        /// <param name="wait">Отключает ожидание (опционально).</param>
        public static void Message(string message, bool wait = true)
        {
            Console.Clear();
            Console.WriteLine(message);
            if (wait)
            {
                Console.CursorVisible = false;
                Console.WriteLine("Нажмите enter для продолжения...");
                Console.ReadLine();
                Console.CursorVisible = true;
            }
        }

        /// <summary>
        /// Меняет поток вывода в файл.
        /// </summary>
        /// <exception cref="IOException">Не удалось перенаправить поток выхода.</exception>
        public static void SwitchOutputStreamToFile()
        {
            Console.Write("Введите путь к файлу: ");
            Console.CursorVisible = true;
            try
            {
                string path = Console.ReadLine() ?? string.Empty;
                Console.CursorVisible = false;
                CurrentWriter = new StreamWriter(path);
                if (_originalOutputStream == null)
                {
                    _originalOutputStream = Console.Out;
                }
                Console.SetOut(CurrentWriter);
            }
            catch (Exception e)
            {
                OutputHandler.Message("Произошла ошибка при экспорте файла.");
                throw new IOException(e.Message, e);
            }
        }

        /// <summary>
        /// Возвращает поток вывода в консоль.
        /// </summary>
        public static void SwitchOutputStreamToConsole()
        {
            if (CurrentWriter != null)
            {
                CurrentWriter.Close();
                CurrentWriter = null;
            }
            if (_originalOutputStream != null)
            {
                Console.SetOut(_originalOutputStream);
                _originalOutputStream = null;
            }
        }
    }
}