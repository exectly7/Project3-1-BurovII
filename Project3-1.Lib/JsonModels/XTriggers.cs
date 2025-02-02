namespace Project3_1.Lib.JsonModels
{
    public class XTriggers : IJSONObject
    {
        /// <summary>
        /// Содержит список проинициализированных полей.
        /// </summary>
        public HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Поле fatiguing.
        /// </summary>
        public string? Fatiguing { get; private set; }
        
        /// <summary>
        /// Поле fatiguing.ability.
        /// </summary>
        public string? FatiguingAbility { get; private set; }
        
        /// <summary>
        /// Поле malady.inflicting.
        /// </summary>
        public string? MaladyInflicting { get; private set; }
        
        /// <summary>
        /// Поле contamintation.bloodlines.
        /// </summary>
        public string? ContamintationBloodlines { get; private set; }
        
        /// <summary>
        /// Поле contamination.keeperskin.
        /// </summary>
        public string? ContaminationKeeperskin { get; private set; }

        public XTriggers(string source)
        {
            Dictionary<string, string> ability = JsonParser.ParseObject(source);
            InitializedFields = new HashSet<string>();
            foreach (KeyValuePair<string, string> field in ability)
            {
                SetField(field.Key, field.Value);    
            }
        }

        public IEnumerable<string> GetAllFields()
        {
            return InitializedFields.ToArray(); // Выбрать массив или лист.
        }

        public string? GetField(string fieldName)
        {
            
            if (!InitializedFields.Contains(fieldName))
            {
                return null;
            }
            
            switch (fieldName)
            {
                case "fatiguing":
                    return JsonParser.StringToQuotedString(Fatiguing);
                case "fatiguing.ability":
                    return JsonParser.StringToQuotedString(FatiguingAbility);
                case "malady.inflicting":
                    return JsonParser.StringToQuotedString(MaladyInflicting);
                case "contamination.bloodlines":
                    return JsonParser.StringToQuotedString(ContamintationBloodlines);
                case "contamination.keeperskin":
                    return JsonParser.StringToQuotedString(ContaminationKeeperskin);
                
            }
            return null;
        }

        // сюда скорее всего надо пихать вообще что угодно что после двоеточия стоит в jsone
        public void SetField(string fieldName, string value) 
        {
            switch (fieldName)
            {
                case "fatiguing":
                    Fatiguing = value[1..^1]; 
                    InitializedFields.Add("fatiguing");
                    break;
                case "fatiguing.ability":
                    FatiguingAbility = value[1..^1]; 
                    InitializedFields.Add("fatiguing.ability");
                    break;
                case "malady.inflicting":
                    MaladyInflicting = value[1..^1]; 
                    InitializedFields.Add("malady.inflicting");
                    break;
                case "contamination.bloodlines":
                    ContamintationBloodlines = value[1..^1]; 
                    InitializedFields.Add("contamination.bloodlines");
                    break;
                case "contamination.keeperskin":
                    ContaminationKeeperskin = value[1..^1]; 
                    InitializedFields.Add("contamination.keeperskin");
                    break;
                
            }
            return;
        }

        public override string ToString()
        {
            Dictionary<string, string> xTriggers = new();
            foreach (string field in GetAllFields())
            {
                xTriggers[field] = GetField(field);
            }
            return JsonParser.CreateJson(xTriggers, false);
        }
    }
}