namespace Project3_1.Core.IOHandlers
{
    public static class OutputHandler
    {
        private static TextWriter? _originalOutputStream;
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