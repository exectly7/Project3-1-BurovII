/*
 * Буров Иван Юрьевич.
 * Вариант 4(.
 * БПИ249-1.
 */

using Project3_1.Core.IOHandlers;
using Project3_1.Core.Services;
using Project3_1.Lib;
using Project3_1.Lib.JsonModels;
using System.Text;

namespace Project3_1.Core
{
    /// <summary>
    /// Класс содержащий точку входа в программу.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            OutputHandler.MenuGuide();
            Console.Clear();
            CreateMenu.MainMenu();
        }

        /// <summary>
        /// Метод для завершения работы.
        /// </summary>
        /// <param name="parameter">Нужен чтобы можно было положить в делегат.</param>
        /// <returns></returns>
        public static bool Exit(string parameter)
        {
            return true;
        }
    }
}
