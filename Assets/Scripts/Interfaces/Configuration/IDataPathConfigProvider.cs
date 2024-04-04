namespace Interfaces.Configuration
{
    public interface IDataPathConfigProvider
    {
        string GetJsonDataPath();
        void SetJsonDataPath(string path);
    }

}