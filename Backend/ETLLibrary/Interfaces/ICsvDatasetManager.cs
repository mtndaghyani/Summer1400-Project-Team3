using System.IO;

namespace ETLLibrary.Interfaces
{
    public interface ICsvDatasetManager
    {
        void SaveCsv(Stream stream, string username, string fileName, long fileLength);
        
    }
}