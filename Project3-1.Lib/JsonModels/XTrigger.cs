namespace Project3_1.Lib.JsonModels
{
    public class XTrigger : IJsonObject
    {
        public HashSet<string> InitializedFields { get; set; }
        
        /// <summary>
        /// Поле id.
        /// </summary>
        public string Id { get; private set; }
        
        /// <summary>
        /// Поле morpheffect.
        /// </summary>
        public string? Morpheffect { get; private set; }
        
        /// <summary>
        /// Поле level.
        /// </summary>
        public int? Level { get; private set; }

        /// <summary>
        /// Конструктор xtrigger.
        /// </summary>
        /// <param name="source">json строка.</param>
        public XTrigger(string source)
        {
            InitializedFields = [];
            try
            {
                Dictionary<string, string> xTrigger = JsonParser.ParseObject(JsonParser.ParseArray(source)[0]);
                foreach (KeyValuePair<string, string> kvp in xTrigger)
                {
                    SetField(kvp.Key, kvp.Value);
                }
            }
            catch (Exception)
            {
                SetField("id", source);
            }
        }

        /// <summary>
        /// Возвращает все возвожные поля.
        /// </summary>
        /// <returns> Возвращает все возвожные поля.</returns>
        public IEnumerable<string> GetAllFields()
        {
            return InitializedFields.ToArray();
        }

        /// <summary>
        /// Возвращает значение поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение.</returns>
        public string GetField(string fieldName)
        {
            if (!InitializedFields.Contains(fieldName))
            {
                return null;
            }
            
            switch (fieldName) 
            {
                case "id":
                    return JsonParser.StringToQuotedString(Id);
                case "morpheffect":
                    return JsonParser.StringToQuotedString(Morpheffect);
                case "level":
                    return Level.ToString();
            }
            return null;
        }

        /// <summary>
        /// Ставит значение поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Возвращает значение.</param>
        public void SetField(string fieldName, string value)
        {
            switch (fieldName)
            {
                case "id":
                    Id = value[1..^1]; 
                    InitializedFields.Add("id");
                    break;
                case "morpheffect":
                    Morpheffect = value[1..^1];
                    InitializedFields.Add("morpheffect");
                    break;
                case "level":
                    Level = JsonParser.StringToInt(value);
                    InitializedFields.Add("level");
                    break;
            }
            return;
        }
        
        /// <summary>
        /// Возвращает строку.
        /// </summary>
        /// <returns>Возвращает строку.</returns>
        public override string ToString()
        {
            Dictionary<string, string> xTrigger = new();
            if (InitializedFields.Count == 3)
            {
                foreach (string field in GetAllFields())
                {
                    xTrigger[field] = GetField(field); 
                }
                return "[" + JsonParser.CreateJson(xTrigger, false) + "]";
            }
            return JsonParser.StringToQuotedString(Id);
        }
    }
}