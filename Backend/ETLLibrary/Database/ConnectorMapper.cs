using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ETLLibrary.Database
{
    public class ConnectorMapper
    {
        public readonly EtlContext Context;

        public ConnectorMapper(EtlContext context)
        {
            Context = context;
        }


        public List<string> GetUserFiles(string username)
        {
            var user = Context.Users.Where(w => w.Username == username).Include(w => w.CsvFiles).Single();
            return user.CsvFiles.Select(document => document.Name).ToList();
        }

        public void AddNewFile(string username, string fileName)
        {
            var user = Context.Users.Include(x => x.CsvFiles).Single(u => u.Username == username);
            var csvFile = new Csv() { Name = fileName, User = user };
            user.CsvFiles.Add(csvFile);
            Context.SaveChanges();
        }

        public void DeleteFile(string fileName, int userId)
        {
            var csv = Context.CsvFiles.SingleOrDefault(x => x.Name == fileName && x.UserId == userId);
            if (csv == null) return;
            Context.CsvFiles.Remove(csv);
            Context.SaveChanges();
        }
        
    }
}