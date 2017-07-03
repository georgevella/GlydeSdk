namespace GlydeSdk.Sample.SimpleConsoleApp.Services
{
    public interface IService
    {
        string GetValue();
    }


    class Service : IService
    {
        public string GetValue()
        {
            return "Hello World from a service";
        }
    }
}