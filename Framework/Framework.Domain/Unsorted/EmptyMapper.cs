namespace Framework.Domain
{
    public class EmptyMapper<T> : IViewModelMapper<T, T> where T : class
    {
        public T Map(T entity)
        {
            return entity;
        }
    }
}