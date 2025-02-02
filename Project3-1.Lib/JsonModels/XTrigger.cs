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

        public IEnumerable<string> GetAllFields()
        {
            return InitializedFields.ToArray();
        }

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