namespace Project3_1.Lib.JsonModels
{
    /// <summary>
    /// Содержит методы, нужные для работы с представлениями JSON объектов.
    /// </summary>
    public interface IJSONObject
    {
        /// <summary>
        /// Содержит множество проинициализированных полей.
        /// </summary>
        HashSet<string> InitializedFields { get; set; }

        /// <summary>
        /// Возвращает названия всех полей JSON объекта.
        /// Информацию берёт из InitializedFields.
        /// </summary>
        /// <returns>Массив строк с названиями полей.</returns>
        IEnumerable<string> GetAllFields();
        
        /// <summary>
        /// Возвращает значение поля по его названию.
        /// Если поля не существует, то возвращает null.
        /// </summary>
        /// <param name="fieldName">Название поля.</param>
        /// <returns>Значение поля в виде строки или null.</returns>
        string GetField(string fieldName);
        
        /// <summary>
        /// Устанавливает полю нужное значение.
        /// В качестве side effect добавляет поле в свойство InitializedFields.
        /// </summary>
        /// <param name="fieldName">Название поля.</param>
        /// <param name="value">Значение поля в виде строки.</param>
        void SetField(string fieldName, string value);
    }
}