namespace Repository.Core.Extensions
{

    public static class QueryableExtension
    {
        public static IQueryable<T> ToQueryable<T>(this T obj) where T : ModelBase
        {
            return new List<T> { obj }.AsQueryable();
        }
    }
}
