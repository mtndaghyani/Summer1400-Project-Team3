using System.Linq;

namespace ETLLibrary.Database.Utils
{
    public static class PipelineConfigurator
    {
        
        
        public static string GetCsvPath(string username, string name)
        {
            using var context = new EtlContext();
            var user = context.Users.SingleOrDefault(u => u.Username == username);
            var csv = context.CsvFiles.SingleOrDefault(x => x.Name == name && x.UserId == user.Id);
            return CsvConfigurator.GetFilePath(username, csv.FileName);
        }

        public static ConnectionInfo GetConnectionString(string username, string name)
        {
            using var context = new EtlContext();
            var user = context.Users.SingleOrDefault(u => u.Username == username);
            var dbConnection = context.DbConnections.SingleOrDefault(x => x.Name == name && x.UserId == user.Id);
            var connectionString = DatabaseConfigurator.GetConnectionString(dbConnection.DbName,
                dbConnection.DbUsername, dbConnection.DbPassword, dbConnection.Url);
            return new ConnectionInfo()
            {
                ConnectionString = connectionString,
                TableName = dbConnection.Table
            };
        }
    }

    public class ConnectionInfo
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
    }
}