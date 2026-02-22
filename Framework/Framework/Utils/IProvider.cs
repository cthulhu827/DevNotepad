namespace Framework
{
    public interface IProvider<out T>
    {
        T Get();
    }
}