namespace Project3_1.Core.Services
{
    public static class Sorter
    {
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