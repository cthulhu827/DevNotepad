namespace Framework.MVC
{
    public interface IReactiveMvcController
    {
        void SetModel(object? model);
        void SetView(object? view);
    }
}