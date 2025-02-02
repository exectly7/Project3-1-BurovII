using Project3_1.Core.IOHandlers;
using Project3_1.Lib;
using Project3_1.Lib.JsonModels;

namespace Project3_1.Core.Services
{
    public static class DataService
    {
        public static string? FilterField { get; set; }
        public static Dictionary<string, Ability> SourceData { get; set; } = new();
        public static List<Ability> DisplayData { get; set; } = new();
        public static Dictionary<string, Dictionary<string, bool>?> SortSettings { get; set; } = new();
        
        public static bool DataImported { get; set; } = false;

        public static bool ImportData(string source)
        {
            bool file = source == "file";
            if (file)
            {
                try
                {
                    InputHandler.SwitchInputStreamToFile();
                }
                catch (IOException ex)
                {
                    return true;
                }
            }

            string json = JsonParser.ReadJson();
            
            try
            {
                Dictionary<string, string> elements = JsonParser.ParseObject(json);
                string[] abilities = JsonParser.ParseArray(elements["elements"]);
                foreach (string ability in abilities)
                {
                    Ability temp = new(ability);
                    SourceData.Add(temp.Id, temp);
                    DisplayData.Add(temp);
                }
                InitializeSorter(DisplayData);
                DataImported = true;
            }
            catch (FormatException e)
            {
                OutputHandler.Message(e.Message);
            }

            if (!file)
            {
                return true;
            }

            {
                try
                {
                    InputHandler.SwitchInputStreamToConsole();
                    return true;
                }
                catch (IOException ex)
                {
                    return true;
                }
            }
        }

        private static void InitializeSorter(List<Ability> abilities)
        {
            try
            {
                foreach (Ability ability in abilities)
                {
                    foreach (string fieldName in ability.GetFieldsToFilter())
                    {
                        SortSettings.TryAdd(fieldName, new Dictionary<string, bool>());
                        SortSettings[fieldName]?.TryAdd(ability.GetField(fieldName) ?? throw new InvalidOperationException(), true);
                    }
                }
            }
            catch (InvalidOperationException)
            {
                throw new FormatException("Invalid JSON");
            }
        }
    }
}