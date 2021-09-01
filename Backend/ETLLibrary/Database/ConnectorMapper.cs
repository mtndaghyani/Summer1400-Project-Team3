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
            var user = Context.Users.Where(w => w.Username == username).Include(w => w.csvFiles).Single();
            return user.csvFiles.Select(document => document.Name).ToList();
        }

        public void AddNewFile(string username, string fileName)
        {
            var user = Context.Users.Include(x => x.csvFiles).Single(u => u.Username == username);
            var csvFile = new Csv() { Name = fileName, User = user };
            user.csvFiles.Add(csvFile);
            Context.SaveChanges();
        }

        
    }
}