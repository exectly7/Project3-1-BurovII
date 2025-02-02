/*
 *
 *
 *
 */

using Project3_1.Core.IOHandlers;
using Project3_1.Core.Services;
using Project3_1.Lib;
using Project3_1.Lib.JsonModels;
using System.Text;

namespace Project3_1.Core
{
    /// <summary>
    ///
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///
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

        public static bool Exit(string parameter)
        {
            return true;
        }
    }
}
