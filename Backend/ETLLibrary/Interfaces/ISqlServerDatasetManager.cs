using System.Collections.Generic;
using ETLLibrary.Database;

namespace ETLLibrary.Interfaces
{
    public interface ISqlServerDatasetManager
    {
        List<string> GetTables(string dbName, string dbUsername, string dbPassword, string url);
        void CreateDataset(string username, DatasetInfo info);
        void DeleteDataset( string name, User user);
        List<string> GetDatasets(string username);
    }
}