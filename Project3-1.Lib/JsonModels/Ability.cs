namespace Project3_1.Lib.JsonModels
{
    /// <summary>
    /// Представление JSON объекта ability.
    /// </summary>
    public class Ability : IJSONObject
    {
        /// <summary>
        /// Содержит список проинициализированных полей.
        /// </summary>
        public HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Поле id.
        /// </summary>
        public string Id { get; private set; }


        public Ability(string source)
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

        public string GetField(string fieldName)
        {
            
            if (!InitializedFields.Contains(fieldName))
            {
                return null;
            }
            
            switch (fieldName)
            {
                case "id":
                    return Id;
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
            }
            return;
        }

        public override string ToString()
        {
            return $"Id: {Id}";
        }
    }
}