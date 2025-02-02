using Project3_1.Core.Services;

namespace Project3_1.Core.Menu
{
    public class MenuItem
    {
        public string Title { get; private set; }
        public Func<string, bool> Action { get; set; }
        public string? Parameter { get; }
        

        public MenuItem(string title, Func<string, bool> action = null, string? parameter = null)
        {
            Title = title;
            Action = action;
            Parameter = parameter;
        }

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