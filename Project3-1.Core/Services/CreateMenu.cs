using Microsoft.VisualBasic;
using Project3_1.Core.Menu;
using Project3_1.Lib.JsonModels;
using System.Collections;

namespace Project3_1.Core.Services
{
    public static class CreateMenu
    {
        public static void MainMenu()
        {
            Menu.Menu mainMenu = new([
                new MenuItem("Ввести данные (консоль/файл)", InputDataMenu),
                new MenuItem("Отфильтровать данные", FilterMenu),
                /*new MenuItem("Отсортировать данные", Sort),
                new MenuItem("Обозреватель XTriggers", ShowTriggers),
                new MenuItem("Показать способности", ShowAbilities), */
                new MenuItem("Вывести данные (консоль/файл)", OutputDataMenu),
                new MenuItem("Выход", Program.Exit)
            ]);
            mainMenu.Loop();
        }
        
        private static bool InputDataMenu(string parameter)
        {
            Menu.Menu inputMenu = new([
                new MenuItem("Ввод в консоль", DataService.ImportData, "console"),
                new MenuItem("Импорт из файла", DataService.ImportData, "file")
            ]);
            inputMenu.Loop();
            return false;
        }
        
        private static bool OutputDataMenu(string parameter)
        {
            Menu.Menu outputMenu = new([
                new MenuItem("Вывод в консоль", DataService.ExportData, "console"),
                new MenuItem("Экспорт в файл", DataService.ExportData, "file")
            ]);
            outputMenu.Loop();
            return false;
        }
        
        private static bool FilterMenu(string parameter)
        {
            if (!DataService.CheckDataImported())
            {
                return false;
            }
            
            List<MenuItem> filterMenuItems = new();
            foreach (string field in DataService.FilterSettings.Keys)
            {
                filterMenuItems.Add(new MenuItem($"{field}", SubFilterMenu, field));
            }
            Menu.Menu filterMenu = new(filterMenuItems);
            filterMenu.Loop();
            return false;
        }
        
        private static bool SubFilterMenu(string field)
        {
            Menu.Menu filterMenu = new();
            foreach (KeyValuePair<string, bool> kvp in DataService.FilterSettings[field])
            {
                if (kvp.Value)
                {
                    MenuItem item = new MenuItem($"{kvp.Key[1..^1]} +", parameter: $"{field}\u2600{kvp.Key}");
                    ((IList)filterMenu.MenuItems).Add(item);
                    item.Action += item.Switch;
                }
                else
                {
                    MenuItem item = new MenuItem($"{kvp.Key[1..^1]}", parameter: $"{field}\u2600{kvp.Key}");
                    ((IList)filterMenu.MenuItems).Add(item);
                    item.Action += item.Switch;
                }
            }
            filterMenu.Loop();
            return false;
        }
    }
}