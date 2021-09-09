using System.IO;

namespace ETLLibrary.Interfaces
{
    public interface IYmlManager
    {
        void SaveYml(Stream openReadStream, string modelName, string username, long fileLength);
    }
}