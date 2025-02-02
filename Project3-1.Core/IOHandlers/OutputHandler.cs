namespace Project3_1.Core.IOHandlers
{
    public class OutputHandler
    {
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
    }
}