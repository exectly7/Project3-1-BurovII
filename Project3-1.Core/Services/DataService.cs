using Project3_1.Core.IOHandlers;
using Project3_1.Lib;
using Project3_1.Lib.JsonModels;
using System.Text;

namespace Project3_1.Core.Services
{
    /// <summary>
    /// Класс для обработки данных.
    /// </summary>
    public static class DataService
    {
        /// <summary>
        /// 
        /// </summary>
        public static string? FilterField { get; set; }
        public static Dictionary<string, Ability> SourceData { get; set; } = new();
        public static List<Ability> DisplayData { get; set; } = new();
        public static Dictionary<string, Dictionary<string, bool>> FilterSettings { get; set; } = new();
        
        public static bool DataImported { get; set; } = false;

        public static bool ImportData(string source)
        {
            if (DataImported)
            {
                SourceData = new();
                DisplayData = new();
                FilterSettings = new();
            }
            bool file = source == "file";
            if (file)
            {
                try
                {
                    InputHandler.SwitchInputStreamToFile();
                }
                catch (IOException ex)
                {
                    OutputHandler.Message(ex.Message);
                    return true;
                }
            }

            if (!file)
            {
                Console.Clear();
                Console.WriteLine("Введите json, по окончанию ввода введите END с новой строки.");
            }
            
            string json = JsonParser.ReadJson();
            
            try
            {
                Dictionary<string, string> elements = JsonParser.ParseObject(json);
                string[] abilities = JsonParser.ParseArray(elements["elements"]);
                foreach (string ability in abilities)
                {
                    Ability temp = new(ability);
                    SourceData.TryAdd(temp.Id, temp);
                    DisplayData.Add(temp);
                }
                InitializeSorter(DisplayData);
                DataImported = true;
            }
            catch (FormatException e)
            {
                InputHandler.SwitchInputStreamToConsole();
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
                    OutputHandler.Message(ex.Message);
                    return true;
                }
            }
        }

        public static bool CheckDataImported()
        {
            if (!DataImported)
            {
                OutputHandler.Message($"Для начала работы импортируйте данные.");
                return false;
            }

            return true;
        }
        
        public static bool ExportData(string source)
        {
            if (!CheckDataImported())
            {
                return true;
            }
            bool file = source == "file";
            if (file)
            {
                try
                {
                    OutputHandler.SwitchOutputStreamToFile();
                }
                catch (IOException ex)
                {
                    return true;
                }
            }
            StringBuilder sb = new();
            sb.Append("{\n \"elements\": [");
            List<Ability> filtredDisplayData = FilterDisplayData();
            int counter = 0;
            foreach (Ability ability in filtredDisplayData)
            {
                if (counter < DisplayData.Count - 1)
                {
                    sb.Append("\t" + ability + ",");
                }
                else
                {
                    sb.Append("\t" + ability);
                }
                counter++;
            }
            sb.Append("     ] \n}");
            JsonParser.WriteJson(sb.ToString());

            if (!file)
            {
                Console.WriteLine("Нажмите enter для выхода...");
                Console.ReadLine();
                return true;
            }

            {
                try
                {
                    OutputHandler.SwitchOutputStreamToConsole();
                    return true;
                }
                catch (IOException ex)
                {
                    return true;
                }
            }
        }

        private static List<Ability> FilterDisplayData()
        {
            bool flag = true;
            List<Ability> filtredDisplayData = new();
            foreach (Ability ability in DisplayData)
            {
                foreach (string field in ability.GetFieldsToFilter())
                {
                    flag = FilterSettings[field][ability.GetField(field)];
                    if (!flag)
                    {
                        break;
                    }
                }

                if (flag)
                {
                    filtredDisplayData.Add(ability);
                }
                
                
                flag = true;
            }
            return filtredDisplayData;
        }
        

        private static void InitializeSorter(List<Ability> abilities)
        {
            try
            {
                foreach (Ability ability in abilities)
                {
                    foreach (string fieldName in ability.GetFieldsToFilter())
                    {
                        FilterSettings.TryAdd(fieldName, new Dictionary<string, bool>());
                        FilterSettings[fieldName]?.TryAdd(ability.GetField(fieldName) ?? throw new InvalidOperationException(), true);
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