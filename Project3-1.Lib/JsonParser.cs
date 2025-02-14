﻿using System.Text;

namespace Project3_1.Lib
{
    /// <summary>
    /// Содержит набор методов для работы с JSON.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Записывает строку в основной поток вывода.
        /// </summary>
        /// <param name="json">Строка для записи.</param>
        public static void WriteJson(string json)
        {
            Console.WriteLine(json);
        }
        
        /// <summary>
        /// Переводит JSON строку в словарь.
        /// </summary>
        /// <param name="json">JSON в виде строки</param>
        /// <returns>JSON объект в виде словаря.</returns>
        /// <exception cref="FormatException">Если JSON невалидный.</exception>
        public static Dictionary<string, string> ParseObject(string json)
        {
            Dictionary<string, string> result = new();
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
                index++; // Пропускает ":".
                SkipWhitespace(json, ref index);
                
                string value = ParseValue(json, ref index).Trim();
                
                SkipWhitespace(json, ref index);
                if (json[index] == ',')
                {
                    index++;
                }

                try
                {
                    result.Add(key, value); // В JSON не может быть двух одинаковых ключей.
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
        
        /// <summary>
        /// Создает строку формата JSON из словаря.
        /// </summary>
        /// <param name="jsonObject">JSON в виде словаря.</param>
        /// /// <param name="newLines">Делать ли отступы или писать в одну строку.</param>
        /// <returns>JSON строку.</returns>
        public static string CreateJson(Dictionary<string, string> jsonObject, bool newLines = true)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            if (newLines)
            {
                sb.Append("\n ");
            }
                
            foreach (KeyValuePair<string, string> kvp in jsonObject)
            {
                if (newLines)
                {
                    sb.Append($"\t \t \t \"{kvp.Key}\"").Append(": ").Append(kvp.Value).Append(",\n");
                }
                else
                {
                    sb.Append($"\"{kvp.Key}\"").Append(": ").Append(kvp.Value).Append(", ");
                }
            }

            if (jsonObject.Count != 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            if (newLines)
            {
                sb.Append("\n \t ");
            }
            sb.Append("}");
            return sb.ToString();
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

        /// <summary>
        /// Пропускает пробельные символы.
        /// </summary>
        /// <param name="json">Строка JSON.</param>
        /// <param name="index">Местоположение парсера по ссылке.</param>
        private static void SkipWhitespace(string json, ref int index)
        {
            while (index < json.Length && char.IsWhiteSpace(json[index]))
            {
                index++;
            }
        }

        /// <summary>
        /// Вычленяет строку, оставляя ее в кавычках.
        /// </summary>
        /// <param name="json">JSON строка.</param>
        /// <param name="index">Индекс автомата по ссылке.</param>
        /// <returns>Строку в кавычках.</returns>
        /// <exception cref="FormatException">Если JSON невалидный.</exception>
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

        /// <summary>
        /// Превращает массив в виде строки в массив строк.
        /// </summary>
        /// <param name="array">Массив в виде строки.</param>
        /// <returns>Массив строк элементов.</returns>
        /// <exception cref="FormatException">Если массив невалидный.</exception>
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

        /// <summary>
        /// Возвращает value для пары key value в json.
        /// Учитывает открытые-закрытые скобки и кавычки.
        /// </summary>
        /// <param name="json">Строка json.</param>
        /// <param name="index">Индекс автомата.</param>
        /// <returns></returns>
        /// <exception cref="FormatException">Если JSON невалидный.</exception>
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
        
        /// <summary>
        /// Возвращает элемент массива.
        /// Учитывает открытые-закрытые скобки и кавычки.
        /// </summary>
        /// <param name="json">Строка json.</param>
        /// <param name="index">Индекс автомата.</param>
        /// <returns></returns>
        /// <exception cref="FormatException">Если массив невалидный.</exception>
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

        
        /// <summary>
        /// Возвращает строку обернутую в кавычки.
        /// </summary>
        /// <param name="json">Строка.</param>
        /// <returns>Строка в кавычках.</returns>
        public static string StringToQuotedString(string json)
        {
            return "\"" + json + "\"";
        }


        /// <summary>
        /// Пытается превратить строку в число.
        /// </summary>
        /// <param name="json">Строка.</param>
        /// <returns>Число.</returns>
        /// <exception cref="FormatException">Если в JSON здесь должно быть число, а появилось что-то другое.</exception>
        public static int StringToInt(string json)
        {
            try
            {
                return int.Parse(json);
            }
            catch (Exception e) when (e is FormatException or OverflowException or ArgumentNullException)
            {
                throw new FormatException("Invalid JSON");
            }
        }
        
        /// <summary>
        /// Пытается превратить строку в bool.
        /// </summary>
        /// <param name="json">Строка.</param>
        /// <returns>true/false.</returns>
        /// <exception cref="FormatException">Если в JSON здесь должен быть bool, а появилось что-то другое.</exception>
        public static bool StringToBool(string json)
        {
            return json switch
            {
                "true" => true,
                "false" => false,
                _ => throw new FormatException("Invalid JSON")
            };
        }
    }
}