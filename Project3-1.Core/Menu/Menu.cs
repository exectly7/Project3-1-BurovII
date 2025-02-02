namespace Project3_1.Core.Menu
{
    /// <summary>
    /// 
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MenuItem> MenuItems { get; set; }
        
        private int _currentItem;
        
        public void ShowMenu()
        {
            Console.Clear();
            
            for (int i = 0; i < MenuItems.Count; i++)
            {
                Console.ForegroundColor = i == _currentItem ? ConsoleColor.White : ConsoleColor.White;
                if (i == _currentItem)
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ResetColor();
                }
                Console.WriteLine(MenuItems[i].Title);
            }

            Console.ResetColor();

        }

        public Menu(List<MenuItem> menuItems)
        {
            MenuItems = menuItems;
        }

        public Menu()
        {
            MenuItems = [];
        }
        
        public void Loop()
        {
            while (true)
            {
                ShowMenu();
                ConsoleKey? key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _currentItem = _currentItem - 1 < 0 ? MenuItems.Count - 1 : _currentItem - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        _currentItem = _currentItem + 1 > MenuItems.Count - 1 ? 0 : _currentItem + 1;
                        break;
                    case ConsoleKey.Enter:
                        MenuItem selectedItem = MenuItems[_currentItem];
                        if (selectedItem.Action(selectedItem.Parameter ?? string.Empty))
                        {
                            return;
                        }
                        break;
                    case ConsoleKey.Q:
                        return;
                }
            }
        }
        
    }
}