using System;
using System.Linq;

namespace ETLLibrary.Database.Utils
{
    public static class Dataset
    {
        public static DatasetType TypeOf(string username, string datasetName)
        {
            using var context = new EtlContext();
            var user = context.Users.Single(u => u.Username == username);
            var csv = user.CsvFiles.SingleOrDefault(x => x.Name == datasetName);
            if (csv != null) return DatasetType.Csv;
            var dbConnection = user.DbConnections.SingleOrDefault(x => x.Name == datasetName);
            if (dbConnection != null) return DatasetType.SqlServer;
            throw new Exception("Dataset not found");
        }
    }
}