using System.Collections;
using System.Text;

namespace Project3_1.Lib.JsonModels
{
    /// <summary>
    /// Представление JSON объекта ability.
    /// </summary>
    public class Ability : IJsonObject
    {
        /// <summary>
        /// Хранит имена полей, по которым можно фильтровать/сортировать.
        /// </summary>
        public static readonly List<string> FieldsToFilter = ["id"];

        /// <summary>
        /// Содержит список проинициализированных полей.
        /// </summary>
        public HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Поле id.
        /// </summary>
        public string Id { get; private set; }
        
        /// <summary>
        /// Поле label.
        /// </summary>
        public string? Label { get; private set; }
        
        /// <summary>
        /// Поле desc.
        /// </summary>
        public string? Description { get; private set; }
        
        /// <summary>
        /// Поле icon.
        /// </summary>
        public string? Icon { get; private set; }
        
        /// <summary>
        /// Поле inherits.
        /// </summary>
        public string? Inherits { get; private set; }
        
        /// <summary>
        /// Поле decayto.
        /// </summary>
        public string? DecayTo { get; private set; }
        
        /// <summary>
        /// Поле lifetime.
        /// </summary>
        public int? Lifetime { get; private set; }
        
        /// <summary>
        /// Поле noartneeded.
        /// </summary>
        public bool? NoArtNeeded { get; private set; }
        
        /// <summary>
        /// Поле resasturate.
        /// </summary>
        public bool? Resaturate { get; private set; }
        
        /// <summary>
        /// Поле XTriggers.
        /// </summary>
        public XTriggers? XTriggers { get; private set; }


        public Ability(string source)
        {
            Dictionary<string, string> ability = JsonParser.ParseObject(source);
            InitializedFields = new HashSet<string>();
            foreach (KeyValuePair<string, string> field in ability)
            {
                SetField(field.Key, field.Value);    
            }

            if (!InitializedFields.Contains("id"))
            {
                throw new FormatException("Invalid JSON");
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
                case "id":
                    return JsonParser.StringToQuotedString(Id);
                case "label":
                    return JsonParser.StringToQuotedString(Label);
                case "desc":
                    return JsonParser.StringToQuotedString(Description);
                case "icon":
                    return JsonParser.StringToQuotedString(Icon);
                case "inherits":
                    return JsonParser.StringToQuotedString(Inherits);
                case "decayto":
                    return JsonParser.StringToQuotedString(DecayTo);
                case "lifetime":
                    return Lifetime.ToString();
                case "noartneeded":
                    return NoArtNeeded.ToString().ToLower();
                case "resaturate":
                    return Resaturate.ToString().ToLower();
                case "xtriggers":
                    return XTriggers.ToString();
            }
            return null;
        }

        // сюда скорее всего надо пихать вообще что угодно что после двоеточия стоит в jsone
        public void SetField(string fieldName, string value) 
        {
            switch (fieldName)
            {
                case "id":
                    Id = value[1..^1]; 
                    InitializedFields.Add("id");
                    break;
                case "label":
                    Label = value[1..^1];
                    InitializedFields.Add("label");
                    break;
                case "desc":
                    Description = value[1..^1];
                    InitializedFields.Add("desc");
                    break;
                case "icon":
                    Icon = value[1..^1];
                    InitializedFields.Add("icon");
                    break;
                case "inherits":
                    Inherits = value[1..^1];
                    InitializedFields.Add("inherits");
                    break;
                case "decayto":
                    DecayTo = value[1..^1];
                    InitializedFields.Add("decayto");
                    break;
                case "lifetime":
                    Lifetime = JsonParser.StringToInt(value);
                    InitializedFields.Add("lifetime");
                    break;
                case "noartneeded":
                    NoArtNeeded = JsonParser.StringToBool(value);
                    InitializedFields.Add("noartneeded");
                    break;
                case "resaturate":
                    Resaturate = JsonParser.StringToBool(value);
                    InitializedFields.Add("resaturate");
                    break;
                case "xtriggers":
                    XTriggers = new XTriggers(value);
                    InitializedFields.Add("xtriggers");
                    break;
            }
            return;
        }

        public override string ToString()
        {
            Dictionary<string, string> ability = new();
            foreach (string field in GetAllFields())
            {
                ability[field] = GetField(field);
            }
            return JsonParser.CreateJson(ability);
        }

        public string[] GetFieldsToFilter()
        {
            List<string> fieldsToFilter = new();
            foreach (string field in GetAllFields())
            {
                if (FieldsToFilter.Contains(field))
                {
                    fieldsToFilter.Add(field);
                }
            }
            
            return fieldsToFilter.ToArray();
        }
    }
}