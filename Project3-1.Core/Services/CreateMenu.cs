using Project3_1.Core.Menu;

namespace Project3_1.Core.Services
{
    public static class CreateMenu
    {
        public static void MainMenu()
        {
            Menu.Menu mainMenu = new([
                new MenuItem("Ввести данные (консоль/файл)", InputDataMenu),
                /*new MenuItem("Отфильтровать данные", Filter),
                new MenuItem("Отсортировать данные", Sort),
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
    }
}