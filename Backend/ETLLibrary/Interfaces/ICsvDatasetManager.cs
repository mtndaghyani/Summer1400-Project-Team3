using System.Collections.Generic;
using System.IO;
using ETLLibrary.Database;
using ETLLibrary.Database.Models;

namespace ETLLibrary.Interfaces
{
    public interface ICsvDatasetManager
    {
        void SaveCsv(Stream stream, string username, string fileName, long fileLength);
        List<string> GetCsvFiles(string username);

        string GetCsvContent(string username, string fileName);

        void DeleteCsv(User user, string fileName);
    }
}