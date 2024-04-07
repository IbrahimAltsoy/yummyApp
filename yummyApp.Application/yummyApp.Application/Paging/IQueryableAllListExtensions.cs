namespace yummyApp.Application.Paging
{
    public static class IQueryableAllListExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> source)
        {
            List<T> items = await source.ToListAsync();

            return items;
        }

        public static List<T> ToList<T>(this IQueryable<T> source)
        {
            List<T> items = source.ToList();

            return items;
        }
    }
}
