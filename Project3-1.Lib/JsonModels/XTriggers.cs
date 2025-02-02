namespace Project3_1.Lib.JsonModels
{
    public class XTriggers : IJsonObject
    {
        /// <summary>
        /// Содержит список проинициализированных полей.
        /// </summary>
        public HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Поле fatiguing.
        /// </summary>
        public XTrigger? Fatiguing { get; private set; }
        
        /// <summary>
        /// Поле fatiguing.ability.
        /// </summary>
        public XTrigger? FatiguingAbility { get; private set; }
        
        /// <summary>
        /// Поле malady.inflicting.
        /// </summary>
        public XTrigger? MaladyInflicting { get; private set; }
        
        /// <summary>
        /// Поле contamintation.bloodlines.
        /// </summary>
        public XTrigger? ContamintationBloodlines { get; private set; }
        
        /// <summary>
        /// Поле contamination.keeperskin.
        /// </summary>
        public XTrigger? ContaminationKeeperskin { get; private set; }

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
                    return Fatiguing.ToString();
                case "fatiguing.ability":
                    return FatiguingAbility.ToString();
                case "malady.inflicting":
                    return MaladyInflicting.ToString();
                case "contamination.bloodlines":
                    return ContamintationBloodlines.ToString();
                case "contamination.keeperskin":
                    return ContaminationKeeperskin.ToString();
                
            }
            return null;
        }

        // сюда скорее всего надо пихать вообще что угодно что после двоеточия стоит в jsone
        public void SetField(string fieldName, string value) 
        {
            switch (fieldName)
            {
                case "fatiguing":
                    Fatiguing = new XTrigger(value); 
                    InitializedFields.Add("fatiguing");
                    break;
                case "fatiguing.ability":
                    FatiguingAbility = new XTrigger(value); 
                    InitializedFields.Add("fatiguing.ability");
                    break;
                case "malady.inflicting":
                    MaladyInflicting = new XTrigger(value); 
                    InitializedFields.Add("malady.inflicting");
                    break;
                case "contamination.bloodlines":
                    ContamintationBloodlines = new XTrigger(value); 
                    InitializedFields.Add("contamination.bloodlines");
                    break;
                case "contamination.keeperskin":
                    ContaminationKeeperskin = new XTrigger(value); 
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