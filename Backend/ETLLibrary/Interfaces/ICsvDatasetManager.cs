using System.Collections.Generic;
using System.IO;

namespace ETLLibrary.Interfaces
{
    public interface ICsvDatasetManager
    {
        void SaveCsv(Stream stream, string username, string fileName, long fileLength);
        List<string> GetCsvFiles(string username);

        string GetCsvContent(string username, string fileName);
    }
}