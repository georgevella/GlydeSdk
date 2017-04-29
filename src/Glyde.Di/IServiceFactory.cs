namespace Glyde.Di
{
    public interface IServiceFactory<out TContract>
        where TContract : class 
    {
        TContract Build();
    }
}