using Project3_1.Core.Services;

namespace Project3_1.Core.Menu
{
    /// <summary>
    /// Класс для пункта меню.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Название пункта меню.
        /// </summary>
        public string Title { get; private set; }
        
        /// <summary>
        /// Хранит метод с действием.
        /// </summary>
        public Func<string, bool> Action { get; set; }
        
        /// <summary>
        /// Параметр для передачи в Action (опционально).
        /// </summary>
        public string? Parameter { get; }
        
        /// <summary>
        /// Конструктор айтема.
        /// </summary>
        /// <param name="title">Название.</param>
        /// <param name="action">Метод.</param>
        /// <param name="parameter">Параметр.</param>
        public MenuItem(string title, Func<string, bool> action = null, string? parameter = null)
        {
            Title = title;
            Action = action;
            Parameter = parameter;
        }

        /// <summary>
        /// Метод для выбора пукта меню в меню фильтрации.
        /// </summary>
        /// <param name="parameter">Передается нужный ключ для изменения настроек фильтрации.</param>
        /// <returns>false чтобы не возвращаться в предыдущее меню.</returns>
        public bool Switch(string parameter)
        {
            string field = parameter.Split("\u2600")[0];
            string value = parameter.Split("\u2600")[1];
            
            if (DataService.FilterSettings[field][value])
            {
                Title = Title.Substring(0, Title.Length - 2);
            }
            else
            {
                Title += " +";
            }
            DataService.FilterSettings[field][value] = !DataService.FilterSettings[field][value];
            return false;
        }
    }
}