namespace Project3_1.Lib.JsonModels
{
    public class Xexts : IJsonObject
    {
        /// <summary>
        /// Содержит список проинициализированных полей.
        /// </summary>
        public HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Поле malady.inflicting.
        /// </summary>
        public string? MaladyInflicting { get; private set; }
        
        /// <summary>
        /// Поле contamination.bloodlines.
        /// </summary>
        public string? ContaminationBloodlines { get; private set; }
        
        /// <summary>
        /// Поле contamination.keeperskin.
        /// </summary>
        public string? ContaminationKeeperskin { get; private set; }
        
        /// <summary>
        /// Поле contamination.curse.fifth.eye.
        /// </summary>
        public string? ContaminationCurseFifthEye { get; private set; }
        
        /// <summary>
        /// Поле contamination.winkwell.
        /// </summary>
        public string? ContaminationWinkwell { get; private set; }
        
        /// <summary>
        /// Поле contamination.chionic.
        /// </summary>
        public string? ContaminationChionic { get; private set; }
        
        /// <summary>
        /// Поле contamination.sthenic.taint.
        /// </summary>
        public string? ContaminationSthenicTaint { get; private set; }
        
        /// <summary>
        /// Поле contamination.actinic.
        /// </summary>
        public string? ContaminationActinic { get; private set; }
        
        /// <summary>
        /// Поле contamination.witchworms.
        /// </summary>
        public string? ContaminationWitchworms { get; private set; }

        
        /// <summary>
        /// Конструктор xexts.
        /// </summary>
        /// <param name="source">Строка xexts.</param>
        public Xexts(string source)
        {
            Dictionary<string, string> xexts = JsonParser.ParseObject(source);
            InitializedFields = new HashSet<string>();
            foreach (KeyValuePair<string, string> field in xexts)
            {
                SetField(field.Key, field.Value);
            }
        }

        /// <summary>
        /// Возвращает все проинициализированные поля.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllFields()
        {
            return InitializedFields.ToArray();
        }

        /// <summary>
        /// Возвращает значение поля по названию.
        /// </summary>
        /// <param name="fieldName">Название поля.</param>
        /// <returns>Значение поля.</returns>
        public string? GetField(string fieldName)
        {
            if (!InitializedFields.Contains(fieldName))
            {
                return null;
            }
            
            switch (fieldName)
            {
                case "malady.inflicting":
                    return JsonParser.StringToQuotedString(MaladyInflicting);
                case "contamination.bloodlines":
                    return JsonParser.StringToQuotedString(ContaminationBloodlines);
                case "contamination.keeperskin":
                    return JsonParser.StringToQuotedString(ContaminationKeeperskin);
                case "contamination.curse.fifth.eye":
                    return JsonParser.StringToQuotedString(ContaminationCurseFifthEye);
                case "contamination.winkwell":
                    return JsonParser.StringToQuotedString(ContaminationWinkwell);
                case "contamination.chionic":
                    return JsonParser.StringToQuotedString(ContaminationChionic);
                case "contamination.sthenic.taint":
                    return JsonParser.StringToQuotedString(ContaminationSthenicTaint);
                case "contamination.actinic":
                    return JsonParser.StringToQuotedString(ContaminationActinic);
                case "contamination.witchworms":
                    return JsonParser.StringToQuotedString(ContaminationWitchworms);
            }
            Console.WriteLine($"Unknown field: {fieldName}");
            return null;
        }

        /// <summary>
        /// Устанавливает значение поля.
        /// </summary>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="value">Значение.</param>
        public void SetField(string fieldName, string value) 
        {
            switch (fieldName)
            {
                case "malady.inflicting":
                    MaladyInflicting = value[1..^1]; 
                    InitializedFields.Add("malady.inflicting");
                    break;
                case "contamination.bloodlines":
                    ContaminationBloodlines = value[1..^1]; 
                    InitializedFields.Add("contamination.bloodlines");
                    break;
                case "contamination.keeperskin":
                    ContaminationKeeperskin = value[1..^1]; 
                    InitializedFields.Add("contamination.keeperskin");
                    break;
                case "contamination.curse.fifth.eye":
                    ContaminationCurseFifthEye = value[1..^1]; 
                    InitializedFields.Add("contamination.curse.fifth.eye");
                    break;
                case "contamination.winkwell":
                    ContaminationWinkwell = value[1..^1]; 
                    InitializedFields.Add("contamination.winkwell");
                    break;
                case "contamination.chionic":
                    ContaminationChionic = value[1..^1]; 
                    InitializedFields.Add("contamination.chionic");
                    break;
                case "contamination.sthenic.taint":
                    ContaminationSthenicTaint = value[1..^1]; 
                    InitializedFields.Add("contamination.sthenic.taint");
                    break;
                case "contamination.actinic":
                    ContaminationActinic = value[1..^1]; 
                    InitializedFields.Add("contamination.actinic");
                    break;
                case "contamination.witchworms":
                    ContaminationWitchworms = value[1..^1]; 
                    InitializedFields.Add("contamination.witchworms");
                    break;
                default:
                    Console.WriteLine($"Unknown field: {fieldName}");
                    Environment.Exit(-1);
                    break;
            }
        }

        public override string ToString()
        {
            Dictionary<string, string> xExts = new();
            foreach (string field in GetAllFields())
            {
                xExts[field] = GetField(field);
            }
            return JsonParser.CreateJson(xExts, false);
        }
    }
}
