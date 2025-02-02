/*
 *
 *
 *
 */

using Project3_1.Core.IOHandlers;
using Project3_1.Core.Services;
using Project3_1.Lib;
using Project3_1.Lib.JsonModels;

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
