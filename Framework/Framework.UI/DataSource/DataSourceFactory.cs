using Framework.UI;

namespace Framework.MVC
{
    public static class DataSourceFactory
    {
        public static IDataSource<T> Create<T>() where T : ViewModel
        {
            return new DataSource<T>();
        }

        public static IDataSource<T> CreateNull<T>() where T : ViewModel
        {
            return new NullDataSource<T>();
        }
    }
}
