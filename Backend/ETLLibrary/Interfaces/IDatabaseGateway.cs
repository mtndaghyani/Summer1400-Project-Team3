using System.Collections.Generic;
using ETLLibrary.Database;
using ETLLibrary.Database.Utils;

namespace ETLLibrary.Interfaces
{
    public interface IDatabaseGateway
    {
        List<string> GetUserDatasets(string username);
        
        void DeleteDataset(string datasetName, int userId);
    }
}