namespace Project3_1.Lib.JsonModels
{
    public class Aspects : IJsonObject
    {
        /// <summary>
        /// Словарь, содержащий все аспекты и их значения.
        /// </summary>
        public Dictionary<string, int> AspectsDictionary { get; private set; }

        /// <summary>
        /// Конструктор aspects.
        /// </summary>
        /// <param name="source">строка аспектов.</param>
        public Aspects(string source)
        {
            AspectsDictionary = new Dictionary<string, int>();
            Dictionary<string, string> parsedAspects = JsonParser.ParseObject(source);

            foreach (KeyValuePair<string, string> field in parsedAspects)
            {
                SetField(field.Key, field.Value);
            }
        }

        /// <summary>
        /// Множество проинициализированных полей.
        /// </summary>
        public HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Возвращает список всех инициализированных аспектов.
        /// </summary>
        public IEnumerable<string> GetAllFields()
        {
            return AspectsDictionary.Keys;
        }

        /// <summary>
        /// Возвращает значение указанного аспекта, если он существует.
        /// </summary>
        public string? GetField(string fieldName)
        {
            return AspectsDictionary.TryGetValue(fieldName, out int value) ? value.ToString() : null;
        }

        /// <summary>
        /// Устанавливает значение для указанного аспекта.
        /// Если значение не является числом, программа завершится с ошибкой.
        /// </summary>
        public void SetField(string fieldName, string value)
        {
            if (int.TryParse(value, out int intValue))
            {
                AspectsDictionary[fieldName] = intValue;
            }
            else
            {
                Console.WriteLine($"Ошибка: Некорректное значение для {fieldName} = {value}");
                Environment.Exit(-1);
            }
        }

        /// <summary>
        /// Преобразует объект в JSON-строку, включая только инициализированные аспекты.
        /// </summary>
        public override string ToString()
        {
            Dictionary<string, string> aspectsString = AspectsDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            return JsonParser.CreateJson(aspectsString, false);
        }
    }
}
