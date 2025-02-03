namespace Project3_1.Core.Services
{
    /// <summary>
    /// Класс для сортировки данных.
    /// </summary>
    public static class Sorter
    {
        /// <summary>
        /// Сортирует данные по возрастанию по указанному полю.
        /// </summary>
        /// <param name="field">Имя поля для сортировки.</param>
        /// <returns>Всегда возвращает true.</returns>
        public static bool DoSortUp(string field)
        {
            DataService.DisplayData.Sort((a, b) =>
            {
                object valueA = a.GetField(field);
                object valueB = b.GetField(field);

                return Comparer<object>.Default.Compare(valueA, valueB);
            });
            return true;
        }

        /// <summary>
        /// Сортирует данные по убыванию по указанному полю.
        /// </summary>
        /// <param name="field">Имя поля для сортировки.</param>
        /// <returns>Всегда возвращает true.</returns>
        public static bool DoSortDown(string field)
        {
            DataService.DisplayData.Sort((a, b) =>
            {
                object valueA = a.GetField(field);
                object valueB = b.GetField(field);

                return Comparer<object>.Default.Compare(valueB, valueA); 
            });
            return true;
        }
    }
}