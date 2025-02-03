using Project3_1.Lib;
using Project3_1.Lib.JsonModels;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text.Json;

namespace Project3_1.Core.Services
{
    /// <summary>
    /// Содержит методы для генерации изображения способности.
    /// </summary>
    public static class AdditionalTask
    {
        private static readonly SKColor CopperColor = new SKColor(0xB8, 0x73, 0x33);
        private const int SquareImageSize = 200;

        /// <summary>
        /// Генерирует изображение способности и сохраняет его в файл "output.png".
        /// В начале запрашивается путь до JSON-файла с конфигурацией аспектов.
        /// </summary>
        /// <param name="parameter">Не используется.</param>
        /// <returns>Всегда возвращает false.</returns>
        public static bool Task(string parameter)
        {
            Console.Clear();
            Console.Write("Введите путь к директории с изображениями: ");
            string imageDir = Console.ReadLine() ?? "";
            if (!Directory.Exists(imageDir))
            {
                Console.WriteLine("Ошибка: директория с изображениями не найдена.");
                return true;
            }

            Dictionary<string, bool> aspectsConfig = LoadAspectsConfiguration();

            Console.Write("Введите ID способности: ");
            string abilityId = Console.ReadLine() ?? "";
            if (!DataService.SourceData.ContainsKey(abilityId))
            {
                Console.WriteLine("Ошибка: способность не найдена.");
                return false;
            }

            Ability ability = DataService.SourceData[abilityId];
            string label = ability.GetField("label") ?? "Неизвестная способность";
            string description = ability.GetField("desc") ?? "Описание отсутствует";
            List<KeyValuePair<string, string>> aspectsWithValues = (ability.Aspects?.AspectsDictionary?
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString()))
                .ToList()) ?? new List<KeyValuePair<string, string>>();

            int width = 800;
            int mainContentHeight = 250;
            int totalHeight = mainContentHeight + 140;

            using (SKBitmap bitmap = new SKBitmap(width, totalHeight))
            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                DrawRoundedBackground(canvas, width, totalHeight);
                DrawAbilityImage(canvas, imageDir, abilityId);
                DrawTextBlock(canvas, label, description, SquareImageSize + 50, width - (SquareImageSize + 50) - 30);
                if (aspectsWithValues.Any())
                {
                    DrawAspectsBlock(canvas, imageDir, aspectsWithValues, aspectsConfig, width, mainContentHeight);
                }
                DrawOuterBorder(canvas, width, totalHeight);

                using (SKImage image = SKImage.FromBitmap(bitmap))
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    File.WriteAllBytes($"../../../Output/output{abilityId}.png", data.ToArray());
                }
            }

            return false;
        }

        /// <summary>
        /// Заготовка метода для загрузки конфигурации аспектов из JSON.
        /// Метод запрашивает путь к JSON-файлу и возвращает словарь,
        /// где ключ – название аспекта, а значение – нужно ли отрисовывать для него картинку.
        /// </summary>
        /// <returns>Словарь конфигурации аспектов.</returns>
        private static Dictionary<string, bool> LoadAspectsConfiguration()
        {
            Console.Write("Введите путь к JSON файлу с аспектами: ");
            string jsonPath = Console.ReadLine() ?? "";
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine("Файл не найден. Используется пустая конфигурация.");
                return new Dictionary<string, bool>();
            }

            try
            {
                Dictionary<string, bool> aspectsConfig = new();
                string json = File.ReadAllText(jsonPath);
                foreach (string aspect in JsonParser.ParseArray(JsonParser.ParseObject(json)["elements"]))
                {
                    Dictionary<string, string> aspectObject = JsonParser.ParseObject(aspect); // Один раз парсим JSON
                    if (aspectObject.TryGetValue("id", out string aspectId)) // Получаем ID
                    {
                        bool noArtNeeded = aspectObject.TryGetValue("noartneeded", out string noartneededStr) &&
                                           JsonParser.StringToBool(noartneededStr);
                        aspectsConfig[aspectId] = noArtNeeded;
                    }
                }
                return aspectsConfig;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки конфигурации: {ex.Message}");
                return new Dictionary<string, bool>();
            }
        }

        /// <summary>
        /// Рисует скругленный фон с темным заливочным цветом.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="width">Ширина изображения.</param>
        /// <param name="totalHeight">Высота изображения.</param>
        private static void DrawRoundedBackground(SKCanvas canvas, int width, int totalHeight)
        {
            using (SKPath clipPath = new SKPath())
            {
                float cornerRadius = 20f;
                clipPath.AddRoundRect(new SKRect(0, 0, width, totalHeight), cornerRadius, cornerRadius);
                canvas.ClipPath(clipPath);
            }
            canvas.Clear(SKColors.White);
            using (SKPaint bgPaint = new SKPaint { Color = new SKColor(30, 30, 30) })
            {
                canvas.DrawRect(new SKRect(0, 0, width, totalHeight), bgPaint);
            }
            canvas.Clear(new SKColor(30, 30, 30));
        }

        /// <summary>
        /// Рисует основное изображение способности.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="imageDir">Директория с изображениями.</param>
        /// <param name="abilityId">ID способности.</param>
        private static void DrawAbilityImage(SKCanvas canvas, string imageDir, string abilityId)
        {
            string imagePath = Path.Combine(imageDir, abilityId + ".png");
            if (File.Exists(imagePath))
            {
                using (SKBitmap image = SKBitmap.Decode(imagePath))
                {
                    SKRect destRect = new SKRect(20, 20, 20 + SquareImageSize, 20 + SquareImageSize);
                    SKRect sourceRect = new SKRect(0, 0, image.Width, image.Height);
                    using (SKPaint paint = new SKPaint { FilterQuality = SKFilterQuality.High })
                    {
                        canvas.DrawBitmap(image, sourceRect, destRect, paint);
                    }
                }
            }
        }

        /// <summary>
        /// Рисует текстовый блок с заголовком и описанием.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="label">Заголовок способности.</param>
        /// <param name="description">Описание способности.</param>
        /// <param name="textX">Начальная координата по оси X для текста.</param>
        /// <param name="maxTextWidth">Максимальная ширина для текста.</param>
        private static void DrawTextBlock(SKCanvas canvas, string label, string description, int textX, int maxTextWidth)
        {
            using (SKPaint titlePaint = new SKPaint
            {
                Color = CopperColor,
                Typeface = SKTypeface.FromFamilyName("Times New Roman"),
                TextSize = 28
            })
            {
                canvas.DrawText(label, textX, 60, titlePaint);
            }
            using (SKPaint linePaint = new SKPaint
            {
                Color = CopperColor,
                StrokeWidth = 2,
                IsStroke = true
            })
            {
                canvas.DrawLine(textX, 80, textX + maxTextWidth, 80, linePaint);
            }
            using (SKPaint descPaint = new SKPaint
            {
                Color = CopperColor,
                Typeface = SKTypeface.FromFamilyName("Times New Roman", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal, SKFontStyleSlant.Italic),
                TextSize = 18
            })
            {
                float yPos = 110;
                List<string> wrappedLines = WrapText(description, descPaint, maxTextWidth, 4);
                foreach (string line in wrappedLines)
                {
                    canvas.DrawText(line, textX, yPos, descPaint);
                    yPos += 28;
                }
            }
        }

        /// <summary>
        /// Рисует блок аспектов с изображениями и числовыми значениями.
        /// Если в конфигурации аспектов указано, что для определённого аспекта не нужно рисовать картинку,
        /// вместо него выводится знак вопроса с отступом от рамки.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="imageDir">Директория с изображениями.</param>
        /// <param name="aspectsWithValues">Список аспектов и их значений.</param>
        /// <param name="aspectsConfig">Конфигурация аспектов, где для каждого аспекта указано, нужно ли рисовать картинку.</param>
        /// <param name="width">Ширина изображения.</param>
        /// <param name="mainContentHeight">Высота основной области содержимого.</param>
        private static void DrawAspectsBlock(SKCanvas canvas, string imageDir, List<KeyValuePair<string, string>> aspectsWithValues, Dictionary<string, bool> aspectsConfig, int width, int mainContentHeight)
        {
            int aspectY = mainContentHeight + 10;
            int aspectSize = 60;
            int aspectSpacing = 25;
            int aspectsBlockWidth = aspectsWithValues.Count * aspectSize + (aspectsWithValues.Count - 1) * aspectSpacing;
            int borderMargin = 10;
            int extraHeight = 15;
            int sideMargin = 20;
            int borderX = sideMargin;
            int borderWidth = width - (2 * sideMargin);
            int borderY = aspectY - borderMargin;
            int borderHeight = aspectSize + extraHeight + (borderMargin * 2);
            int startX = borderX + borderWidth - aspectsBlockWidth - 20;
            float diamondSize = 5f;
            float diamondCenterY = aspectY + (aspectSize / 2f);
            float questionMargin = 5f; 

            using (SKPaint diamondPaint = new SKPaint
            {
                Color = CopperColor,
                StrokeWidth = 2,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            })
            using (SKPaint valuePaint = new SKPaint
            {
                Color = SKColors.White,
                TextSize = 20,
                Typeface = SKTypeface.FromFamilyName("Times New Roman"),
                IsAntialias = true,
                TextAlign = SKTextAlign.Center
            })
            using (SKPaint missingAspectPaint = new SKPaint
            {
                Color = SKColors.White,
                TextSize = 36,
                Typeface = SKTypeface.FromFamilyName("Times New Roman"),
                IsAntialias = true,
                TextAlign = SKTextAlign.Center
            })
            {
                for (int i = 0; i < aspectsWithValues.Count; i++)
                {
                    if (i > 0)
                    {
                        float diamondX = startX - (aspectSpacing / 2f);
                        DrawDiamond(canvas, diamondX, diamondCenterY, diamondSize, diamondPaint);
                    }

                    string aspectKey = aspectsWithValues[i].Key;
                    bool needImage = true;
                    if (aspectsConfig.TryGetValue(aspectKey, out bool configValue))
                    {
                        needImage = !configValue;
                    }

                    if (needImage)
                    {
                        string aspectPath = Path.Combine(imageDir, aspectKey + ".png");
                        if (File.Exists(aspectPath))
                        {
                            SKRect aspectRect = new SKRect(startX, aspectY, startX + aspectSize, aspectY + aspectSize);
                            float cornerRadius = 10f;
                            using (SKPaint bgPaint = new SKPaint { Color = SKColors.White, IsAntialias = true })
                            {
                                canvas.DrawRoundRect(aspectRect, cornerRadius, cornerRadius, bgPaint);
                            }
                            using (SKBitmap aspectImage = SKBitmap.Decode(aspectPath))
                            {
                                SKBitmap resized = aspectImage.Resize(new SKImageInfo(aspectSize, aspectSize), SKFilterQuality.High);
                                if (resized != null)
                                {
                                    using (SKPaint imagePaint = new SKPaint { IsAntialias = true })
                                    {
                                        using (SKPath clipPath = new SKPath())
                                        {
                                            clipPath.AddRoundRect(aspectRect, cornerRadius, cornerRadius);
                                            canvas.Save();
                                            canvas.ClipPath(clipPath, SKClipOperation.Intersect, true);
                                            canvas.DrawBitmap(resized, startX, aspectY, imagePaint);
                                            canvas.Restore();
                                        }
                                    }
                                    resized.Dispose();
                                }
                            }
                        }
                        else
                        {
                            DrawQuestionMark(canvas, startX, aspectY, aspectSize, missingAspectPaint, questionMargin);
                        }
                    }
                    else
                    {
                        DrawQuestionMark(canvas, startX, aspectY, aspectSize, missingAspectPaint, questionMargin);
                    }

                    float valueX = startX + (aspectSize / 2f);
                    float valueY = aspectY + aspectSize + valuePaint.TextSize + 5;
                    canvas.DrawText(aspectsWithValues[i].Value, valueX, valueY, valuePaint);
                    startX += aspectSize + aspectSpacing;
                }
                using (SKPaint overallBorderPaint = new SKPaint
                {
                    Color = CopperColor,
                    StrokeWidth = 2,
                    IsStroke = true,
                    IsAntialias = true
                })
                {
                    float overallCornerRadius = 15;
                    SKRect overallBorderRect = new SKRect(borderX, borderY, borderX + borderWidth, borderY + borderHeight);
                    canvas.DrawRoundRect(overallBorderRect, overallCornerRadius, overallCornerRadius, overallBorderPaint);
                }
            }
        }

        /// <summary>
        /// Рисует внешний белый контур по краям всего изображения.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="width">Ширина изображения.</param>
        /// <param name="totalHeight">Высота изображения.</param>
        private static void DrawOuterBorder(SKCanvas canvas, int width, int totalHeight)
        {
            using (SKPaint borderPaint = new SKPaint
            {
                Color = SKColors.White,
                StrokeWidth = 8,
                IsStroke = true,
                IsAntialias = true
            })
            {
                canvas.DrawRoundRect(new SKRect(4, 4, width - 4, totalHeight - 4), 20, 20, borderPaint);
            }
        }

        /// <summary>
        /// Рисует ромбовидную фигуру.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="centerX">Координата X центра ромба.</param>
        /// <param name="centerY">Координата Y центра ромба.</param>
        /// <param name="size">Полуразмер ромба.</param>
        /// <param name="paint">Инструмент для рисования.</param>
        private static void DrawDiamond(SKCanvas canvas, float centerX, float centerY, float size, SKPaint paint)
        {
            using (SKPath path = new SKPath())
            {
                path.MoveTo(centerX - size, centerY);
                path.LineTo(centerX, centerY - size);
                path.LineTo(centerX + size, centerY);
                path.LineTo(centerX, centerY + size);
                path.Close();
                canvas.DrawPath(path, paint);
            }
        }

        /// <summary>
        /// Рисует знак вопроса внутри квадрата аспекта с заданным отступом от рамки.
        /// </summary>
        /// <param name="canvas">Холст для рисования.</param>
        /// <param name="startX">Левая координата квадрата аспекта.</param>
        /// <param name="aspectY">Верхняя координата квадрата аспекта.</param>
        /// <param name="aspectSize">Размер квадрата аспекта.</param>
        /// <param name="paint">Инструмент для рисования знака вопроса.</param>
        /// <param name="margin">Отступ от рамки квадрата, чтобы знак не пересекался с краем.</param>
        private static void DrawQuestionMark(SKCanvas canvas, float startX, float aspectY, int aspectSize, SKPaint paint, float margin)
        {
            SKRect missingRect = new SKRect(startX, aspectY, startX + aspectSize, aspectY + aspectSize);
            using (SKPaint fillPaint = new SKPaint { Color = SKColors.White, IsAntialias = true })
            {
                canvas.DrawRect(missingRect, fillPaint);
            }
            float centerX = startX + (aspectSize / 2f) - margin;
            float centerY = aspectY + (aspectSize / 2f) + margin + (paint.TextSize / 2f) - 10;
            canvas.DrawText("?", centerX, centerY, paint);
            using (SKPaint fillPaint = new SKPaint { Color = SKColors.White, IsAntialias = true })
            {
                canvas.DrawRoundRect(missingRect, 20, 20, fillPaint);
            }
            using (SKPaint questionPaint = new SKPaint
            {
                Color = CopperColor,
                TextSize = 36,
                Typeface = SKTypeface.FromFamilyName("Arial"),
                TextAlign = SKTextAlign.Center,
                IsAntialias = true
            })
            {
                canvas.DrawText("?", centerX, centerY + 10, questionPaint);
            }
        }

        /// <summary>
        /// Переносит текст на несколько строк, чтобы он не превышал заданную ширину.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <param name="paint">Инструмент для измерения текста.</param>
        /// <param name="maxWidth">Максимальная ширина строки.</param>
        /// <param name="maxLines">Максимальное число строк.</param>
        /// <returns>Список строк с перенесенным текстом.</returns>
        private static List<string> WrapText(string text, SKPaint paint, float maxWidth, int maxLines)
        {
            List<string> lines = new List<string>();
            string[] words = text.Split(' ');
            string currentLine = "";
            foreach (string word in words)
            {
                string testLine = string.IsNullOrEmpty(currentLine) ? word : currentLine + " " + word;
                if (paint.MeasureText(testLine) > maxWidth)
                {
                    lines.Add(currentLine);
                    currentLine = word;
                    if (lines.Count >= maxLines)
                    {
                        break;
                    }
                }
                else
                {
                    currentLine = testLine;
                }
            }
            if (!string.IsNullOrEmpty(currentLine) && lines.Count < maxLines)
            {
                lines.Add(currentLine);
            }
            return lines;
        }
    }
}
