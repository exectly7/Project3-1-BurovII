using System.Text;

namespace Project3_1.Lib
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonParser
    {
        public static Dictionary<string, string> ParseObject(string json)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            json = json.Trim();
            int index = 1;      
            
            if (!json.StartsWith("{") || !json.EndsWith("}"))
            {
                throw new FormatException("Invalid JSON");
            }

            while (json[index] != '}')
            {
                SkipWhitespace(json, ref index);
                
                string key = ParseString(json, ref index)[1..^1].ToLower(); // Обрезаем кавычки
                
                SkipWhitespace(json, ref index);
                index++;
                SkipWhitespace(json, ref index);
                
                string value = ParseValue(json, ref index).Trim();
                
                SkipWhitespace(json, ref index);
                if (json[index] == ',')
                {
                    index++;
                }

                try
                {
                    result.Add(key, value);
                }
                catch (Exception e) when (e is ArgumentException)
                {
                    throw new FormatException("Invalid JSON");
                }
                
            }

            if (result.Count == 0)
            {
                throw new FormatException("Invalid JSON");
            }
            return result;
        }
        
        public static void WriteJson()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод считывает текст из стандартного потока ввода.
        /// Текст будет считан до null или строки END.
        /// </summary>
        /// <returns>Строку считанную из потока ввода.</returns>
        public static string ReadJson()
        {
            StringBuilder json = new StringBuilder();

            while (Console.ReadLine() is { } line && line != "END")
            {
                json.Append(line);
            }
            
            return json.ToString();
        }

        private static void SkipWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }
        }

        // сюда подается строка начинающаяся на " и возвращается заканч на кавычки
        public static string ParseString(string json, ref int index)
        {
            StringBuilder result = new();
            
            if (json[index] != '"')
            {
                throw new FormatException("Invalid JSON");
            }
            
            result.Append(json[index]);
            index++;
            
            while (index < json.Length && json[index] != '"')
            {
                result.Append(json[index]);
                index++;
            }

            result.Append(json[index]);
            index++;

            if (result.ToString().Split('"').Length < 3 || result.ToString().Length == 2) 
            {
                throw new FormatException("Invalid JSON");
            }
            
            return result.ToString();
        }

        public static string[] ParseArray(string array)
        {
            array = array.Trim();
            int index = 0;
            List<string> result = new();
            
            if (!array.StartsWith("[") || !array.EndsWith("]"))
            {
                throw new FormatException("Invalid JSON");
            }
            
            index++;

            while (array[index] != ']')
            {
                SkipWhitespace(array, ref index);
                result.Add(ParseElement(array, ref index));
                SkipWhitespace(array, ref index);
                if (array[index] == ',')
                {
                    index++;
                }
            }

            if (result.Count == 0)
            {
                throw new FormatException("Invalid JSON");
            }
            return result.ToArray();
        }

        private static string ParseValue(string json, ref int index)
        {
            StringBuilder result = new();
            int openedFigureBrackets = 0;
            int openedSquareBrackets = 0;
            bool isString = false;
            while (isString || (index < json.Length - 1 &&
                   (json[index] != ',' || openedFigureBrackets + openedSquareBrackets != 0) &&
                   (json[index] != '}' || openedFigureBrackets + openedSquareBrackets != 0)))
            {
                if (json[index] == '"')
                {
                    isString = !isString;
                }

                if (!isString)
                {
                    if (json[index] == '{')
                    {
                        openedFigureBrackets++;
                    }

                    if (json[index] == '}')
                    {
                        openedFigureBrackets--;
                    }
                
                    if (json[index] == '[')
                    {
                        openedFigureBrackets++;
                    }

                    if (json[index] == ']')
                    {
                        openedFigureBrackets--;
                    }
                }
                
                result.Append(json[index]);
                index++;
            }
            
            if (isString || openedFigureBrackets + openedSquareBrackets != 0 || (json[index] != ',' && json[index] != '}'))
            {
                throw new FormatException("Invalid JSON");
            }
            
            return result.ToString();
        }
        private static string ParseElement(string json, ref int index)
        {
            StringBuilder result = new();
            int openedFigureBrackets = 0;
            int openedSquareBrackets = 0;
            bool isString = false;
            while (isString || (index < json.Length - 1 &&
                                (json[index] != ',' || openedFigureBrackets + openedSquareBrackets != 0) &&
                                (json[index] != '}' || openedFigureBrackets + openedSquareBrackets != 0)))
            {
                if (json[index] == '"')
                {
                    isString = !isString;
                }

                if (!isString)
                {
                    if (json[index] == '{')
                    {
                        openedFigureBrackets++;
                    }

                    if (json[index] == '}')
                    {
                        openedFigureBrackets--;
                    }
                
                    if (json[index] == '[')
                    {
                        openedFigureBrackets++;
                    }

                    if (json[index] == ']')
                    {
                        openedFigureBrackets--;
                    }
                }
                
                result.Append(json[index]);
                index++;
            }
            
            if (isString || openedFigureBrackets + openedSquareBrackets != 0 || (json[index] != ',' && json[index] != ']'))
            {
                throw new FormatException("Invalid JSON");
            }
            
            return result.ToString();
        }
    }
}