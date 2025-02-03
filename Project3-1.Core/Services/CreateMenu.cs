using Microsoft.VisualBasic;
using Project3_1.Core.IOHandlers;
using Project3_1.Core.Menu;
using Project3_1.Lib.JsonModels;
using System.Collections;

namespace Project3_1.Core.Services
{
    /// <summary>
    /// Методы для создания меню (основное ветвление программы).
    /// </summary>
    public static class CreateMenu
    {
        /// <summary>
        /// Создает главное меню.
        /// </summary>
        public static void MainMenu()
        {
            Menu.Menu mainMenu = new([
                new MenuItem("Ввести данные (консоль/файл)", InputDataMenu),
                new MenuItem("Отфильтровать данные", FilterMenu),
                new MenuItem("Отсортировать данные", SortMenu),
                new MenuItem("Обозреватель XTriggers", ShowTriggersMenu),
                new MenuItem("Показать способности", AdditionalTask.Task),
                new MenuItem("Вывести данные (консоль/файл)", OutputDataMenu),
                new MenuItem("Выход", Program.Exit)
            ]);
            mainMenu.Loop();
        }
        
        /// <summary>
        /// Меню для ввода данных (консоль/файл).
        /// </summary>
        /// <param name="parameter">Параметр для метода.</param>
        /// <returns>Возвращает false.</returns>
        private static bool InputDataMenu(string parameter)
        {
            Menu.Menu inputMenu = new([
                new MenuItem("Ввод в консоль", DataService.ImportData, "console"),
                new MenuItem("Импорт из файла", DataService.ImportData, "file")
            ]);
            inputMenu.Loop();
            return false;
        }
        
        /// <summary>
        /// Меню для вывода данных (консоль/файл).
        /// </summary>
        /// <param name="parameter">Параметр для метода.</param>
        /// <returns>Возвращает false.</returns>
        private static bool OutputDataMenu(string parameter)
        {
            Menu.Menu outputMenu = new([
                new MenuItem("Вывод в консоль", DataService.ExportData, "console"),
                new MenuItem("Экспорт в файл", DataService.ExportData, "file")
            ]);
            outputMenu.Loop();
            return false;
        }
        
        /// <summary>
        /// Меню для фильтрации данных.
        /// </summary>
        /// <param name="parameter">Параметр для метода.</param>
        /// <returns>Возвращает false.</returns>
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
        
        /// <summary>
        /// Подменю для фильтрации данных по полям.
        /// </summary>
        /// <param name="field">Поле для фильтрации.</param>
        /// <returns>Возвращает false.</returns>
        private static bool SubFilterMenu(string field)
        {
            Menu.Menu filterMenu = new();
            foreach (KeyValuePair<string, bool> kvp in DataService.FilterSettings[field])
            {
                MenuItem item = kvp.Value
                    ? new MenuItem($"{kvp.Key[1..^1]} +", parameter: $"{field}\u2600{kvp.Key}")
                    : new MenuItem($"{kvp.Key[1..^1]}", parameter: $"{field}\u2600{kvp.Key}");

                ((IList)filterMenu.MenuItems).Add(item);
                item.Action += item.Switch;
            }
            filterMenu.Loop();
            return false;
        }

        /// <summary>
        /// Меню для сортировки данных.
        /// </summary>
        /// <param name="parameter">Параметр для метода.</param>
        /// <returns>Возвращает false.</returns>
        private static bool SortMenu(string parameter)
        {
            if (!DataService.CheckDataImported())
            {
                return false;
            }

            List<MenuItem> sortMenuItems = new();
            foreach (string field in DataService.FilterSettings.Keys)
            {
                sortMenuItems.Add(new MenuItem($"{field}", SortOptionsMenu, field));
            }
            Menu.Menu sortMenu = new(sortMenuItems);
            sortMenu.Loop();
            return false;
        }
        
        /// <summary>
        /// Подменю для выбора способа сортировки.
        /// </summary>
        /// <param name="field">Поле для сортировки.</param>
        /// <returns>Возвращает false.</returns>
        private static bool SortOptionsMenu(string field)
        {
            List<MenuItem> menuItems = new()
            {
                new MenuItem("По возрастанию", Sorter.DoSortUp, field),
                new MenuItem("По убыванию", Sorter.DoSortDown, field)
            };
            Menu.Menu inputMenu = new(menuItems);
            inputMenu.Loop();
            return false;
        }
        
        /// <summary>
        /// Меню для отображения всех триггеров.
        /// </summary>
        /// <param name="parameter">Параметр для метода.</param>
        /// <returns>Возвращает false.</returns>
        private static bool ShowTriggersMenu(string parameter)
        {
            List<MenuItem> menuItems = new();
            foreach (Ability ability in DataService.SourceData.Values)
            {
                menuItems.Add(new MenuItem($"Id: {ability.Id} Label: {ability.Label}", DisplayTrigger, ability.Id));
            }

            Menu.Menu triggerMenu = new(menuItems);
            triggerMenu.Loop();
            return false;
        }

        /// <summary>
        /// Отображает информацию о триггере по его Id.
        /// </summary>
        /// <param name="id">Идентификатор триггера.</param>
        /// <returns>Возвращает false.</returns>
        private static bool DisplayTrigger(string id)
        {
            TriggerExplorer.Display(DataService.SourceData[id]);
            Console.WriteLine();
            Console.WriteLine("Нажмите Enter для выхода");
            Console.ReadLine();
            return false;
        }
    }
}
