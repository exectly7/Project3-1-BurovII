using Project3_1.Lib.JsonModels;
using System.ComponentModel.DataAnnotations;

namespace Project3_1.Core.IOHandlers
{
    /// <summary>
    /// Класс для обозревания триггеров.
    /// </summary>
    public static class TriggerExplorer
    {
        public static void Display(Ability ability)
        {
            Console.Clear();
            int width = 20 + ability.Id.Length + ability.Label.Length;
            
            // Первая строка.
            Console.Write("\u250C");
            for (int i = 0; i < width + 2; i++)
            {
                Console.Write("\u2500");
            }
            Console.WriteLine("\u2510");
            
            // Вторая строка.
            Console.Write("\u2502" + " " + "\u2554");
            for (int i = 0; i < ability.Id.Length + 6; i++)
            {
                Console.Write("\u2550");
            }
            Console.Write("\u2557" + " ");
            
            Console.Write("\u2554");
            for (int i = 0; i < ability.Label.Length + 9; i++)
            {
                Console.Write("\u2550");
            }
            Console.WriteLine("\u2557" + " " + "\u2502" + "\u2591");
            
            // Третья строка.
            Console.WriteLine("\u2502" + " " + "\u2551" + " ID: " + ability.Id + " " + "\u2551" + " " + "\u2551" + " Label: " + ability.Label + " " + "\u2551" + " " + "\u2502" + "\u2591");
            
            // Четвертая строка.
            Console.Write("\u2502" + " " + "\u2560");
            for (int i = 0; i < ability.Id.Length + 6; i++)
            {
                Console.Write("\u2550");
            }
            Console.Write("\u255D" + " ");
            
            Console.Write("\u255A");
            for (int i = 0; i < ability.Label.Length + 9; i++)
            {
                Console.Write("\u2550");
            }
            Console.WriteLine("\u2563" + " " + "\u2502" + "\u2591");
            
            // Пятая строка.
            Console.Write("\u255E" + "\u2550" + "\u2569");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("\u2550");
            }
            Console.WriteLine("\u2569" + "\u2550" + "\u2561" + "\u2591");
            
            // Description.
            List<string> description = ["Description:"];
            for (int i = 0; i < width - 12; i++)
            {
                description[0] += " ";
            }
            description.AddRange(Hyphenate(ability.Desc, width));
            foreach (string line in description)
            {
                Console.WriteLine("\u2502" + " " + line + " " + "\u2502" + "\u2591");
            }
            
            // Строка после Description.
            Console.Write("\u255E");
            for (int i = 0; i < width + 2; i++)
            {
                Console.Write("\u2550");
            }
            Console.WriteLine("\u2561" + "\u2591");
            
            // Строка Triggers.
            Console.Write("\u2502" + " ");
            string triggers = "Triggers:";
            for (int i = 0; i < width - 9; i++)
            {
                triggers += " ";
            }
            Console.Write(triggers);
            Console.WriteLine(" " + "\u2502" + "\u2591");
            
            // Крышечка триггеров. 
            Console.Write("\u251C");
            for (int i = 0; i < width + 2; i++)
            {
                Console.Write("\u2500");
            }
            Console.WriteLine("\u2524" + "\u2591");
            
            
            int counter = 0;
            string finalString = "\u251C";
            for (int i = 0; i < width + 2; i++)
            {
                finalString += "\u2500";
            }
            finalString += "\u2524" + "\u2591";
            foreach (string trigger in ability.XTriggers.GetAllFields())
            {
                if (trigger is string)
                {
                    
                }
                if (counter == ability.XTriggers.Triggers.Count - 1)
                {
                    finalString = "\u2514";
                    for (int i = 0; i < width + 2; i++)
                    {
                        finalString += "\u2500";
                    }
                    finalString += "\u2518" + "\u2591";
                }
                
                List<string> key = Hyphenate((string)kvp.Key, width);
                foreach (string line in key)
                {
                    Console.WriteLine("\u2502" + " " + line + " " + "\u2502" + "\u2591");
                }
                Console.Write("\u2502" + " ");
                string arrows = "";
                for (int i = 0; i < width; i++)
                {
                    arrows += "\u2193";
                }
                Console.Write(arrows);
                Console.WriteLine(" " + "\u2502" + "\u2591");

                if (kvp.Value is string)
                {
                    List<string> lines = Hyphenate((string)kvp.Value, width);
                    foreach (string line in lines)
                    {
                        Console.WriteLine("\u2502" + " " + line + " " + "\u2502" + "\u2591");
                    }
                }

                if (kvp.Value is List<object>)
                {
                    List<object> temp = (List<object>)kvp.Value;
                    Dictionary<string, object> inTrigger = (Dictionary<string, object>)temp[0];
                    foreach (KeyValuePair<string, object> value in inTrigger)
                    {
                        string val = "";
                        if (value.Value is string)
                        {
                            val = (string)value.Value;
                        }
                        else if (value.Value is int)
                        {
                            val = ((int)value.Value).ToString();
                        }
                        
                        List<string> lines = Hyphenate(value.Key + ": " + val, width);
                        foreach (string line in lines)
                        {
                            Console.WriteLine("\u2502" + " " + line + " " + "\u2502" + "\u2591");
                        }
                    }
                }
                Console.WriteLine(finalString);
                counter++;
            }

            for (int i = 0; i < width + 5; i++)
            {
                Console.Write("\u2591");
            }
            Console.WriteLine();
            
        }
        
        /// <summary>
        /// Метод для переноса слов по ширине строки.
        /// </summary>
        /// <param name="input">Слова.</param>
        /// <param name="width">Ширина строки.</param>
        /// <returns></returns>
        public static List<string> Hyphenate(string? input, int width)
        {
            if (input == null)
            {
                return new List<string> { "No description".PadRight(width) };
            }
    
            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new();
            string line = "";
    
            foreach (string word in words)
            {
                if (word.Length > width)
                {
                    string longword = word;
                    while (longword.Length > width)
                    {
                        result.Add(longword.Substring(0, width - 1) + "-".PadRight(width - (width - 1)));
                        longword = longword.Substring(width - 1);
                    }
                    line = longword;
                }
                else if (line.Length + word.Length + 1 <= width)
                {
                    line += (line.Length > 0 ? " " : "") + word;
                }
                else
                {
                    result.Add(line.PadRight(width));
                    line = word;
                }
            }
    
            if (line.Length > 0)
            {
                result.Add(line.PadRight(width));
            }
    
            return result;
        }
    }
}