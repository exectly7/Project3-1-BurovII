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
        /// Поле contamination.curse.fifth.eye.
        /// </summary>
        public XTrigger? ContaminationCurseFifthEye { get; private set; }
        
        /// <summary>
        /// Поле contamination.keeperskin.
        /// </summary>
        public XTrigger? ContaminationKeeperskin { get; private set; }
        
        /// <summary>
        /// Поле contamination.winkwell.
        /// </summary>
        public XTrigger? ContaminationWinkwell { get; private set; }
        
        /// <summary>
        /// Поле contamination.chionic.
        /// </summary>
        public XTrigger? ContaminationChionic { get; private set; }
        
        /// <summary>
        /// Поле contamination.sthenic.taint.
        /// </summary>
        public XTrigger? ContaminationSthenicTaint { get; private set; }
        
        /// <summary>
        /// Поле drying.
        /// </summary>
        public XTrigger? Drying { get; private set; }
        
        /// <summary>
        /// Поле contamination.actinic.
        /// </summary>
        public XTrigger? ContaminationActinic { get; private set; }
        
        /// <summary>
        /// Поле contamination.witchworms.
        /// </summary>
        public XTrigger? ContaminationWitchworms { get; private set; }
        
        /// <summary>
        /// Поле recovering.
        /// </summary>
        public XTrigger? Recovering { get; private set; }
        
        /// <summary>
        /// Поле recovering.ability.
        /// </summary>
        public XTrigger? RecoveringAbility { get; private set; }
        
        /// <summary>
        /// Поле malady.curing.
        /// </summary>
        public XTrigger? MaladyCuring { get; private set; }

        /// <summary>
        /// Конструктор xtriggers.
        /// </summary>
        /// <param name="source"></param>
        public XTriggers(string source)
        {
            Dictionary<string, string> ability = JsonParser.ParseObject(source);
            InitializedFields = new HashSet<string>();
            foreach (KeyValuePair<string, string> field in ability)
            {
                SetField(field.Key, field.Value);    
            }
        }

        /// <summary>
        /// Возвращает названия всех полей.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllFields()
        {
            return InitializedFields.ToArray(); // Выбрать массив или лист.
        }

        /// <summary>
        /// Возвращает значение поля.
        /// </summary>
        /// <param name="fieldName">Название поля.</param>
        /// <returns>Возвращает значение поля.</returns>
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
                case "contamination.curse.fifth.eye":
                    return ContaminationCurseFifthEye.ToString();
                case "contamination.winkwell":
                    return ContaminationWinkwell.ToString();
                case "contamination.chionic":
                    return ContaminationChionic.ToString();
                case "contamination.sthenic.taint":
                    return ContaminationSthenicTaint.ToString();
                case "drying":
                    return Drying.ToString();
                case "contamination.actinic":
                    return ContaminationActinic.ToString();
                case "contamination.witchworms":
                    return ContaminationWitchworms.ToString();
                case "recovering":
                    return Recovering.ToString();
                case "recovering.ability":
                    return RecoveringAbility.ToString();
                case "malady.curing":
                    return MaladyCuring.ToString();
            }
            Console.WriteLine($"Unknown field: {fieldName}");
            Console.ReadLine();
            return null;
        }

        // Видит Бог я не хотел хранить это в полях.
        /// <summary>
        /// Устанавливает значение полю.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значение поля.</param>
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
                case "contamination.curse.fifth.eye":
                    ContaminationCurseFifthEye = new XTrigger(value); 
                    InitializedFields.Add("contamination.curse.fifth.eye");
                    break;
                case "contamination.winkwell":
                    ContaminationWinkwell = new XTrigger(value); 
                    InitializedFields.Add("contamination.winkwell");
                    break;
                case "contamination.chionic":
                    ContaminationChionic = new XTrigger(value); 
                    InitializedFields.Add("contamination.chionic");
                    break;
                case "contamination.sthenic.taint":
                    ContaminationSthenicTaint = new XTrigger(value); 
                    InitializedFields.Add("contamination.sthenic.taint");
                    break;
                case "drying":
                    Drying = new XTrigger(value); 
                    InitializedFields.Add("drying");
                    break;
                case "contamination.actinic":
                    ContaminationActinic = new XTrigger(value); 
                    InitializedFields.Add("contamination.actinic");
                    break;
                case "contamination.witchworms":
                    ContaminationWitchworms = new XTrigger(value); 
                    InitializedFields.Add("contamination.witchworms");
                    break;
                case "recovering":
                    Recovering = new XTrigger(value); 
                    InitializedFields.Add("recovering");
                    break;
                case "recovering.ability":
                    RecoveringAbility = new XTrigger(value); 
                    InitializedFields.Add("recovering.ability");
                    break;
                case "malady.curing":
                    MaladyCuring = new XTrigger(value); 
                    InitializedFields.Add("malady.curing");
                    break;
                default:
                    Console.WriteLine($"Unknown field: {fieldName}");
                    Environment.Exit(-1);
                    break;
            }
        }

        /// <summary>
        /// Возваращает xtriggers в виде строки.
        /// </summary>
        /// <returns>Возваращает xtriggers в виде строки.</returns>
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