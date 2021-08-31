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


        public List<string> Get(string key)
        {
            var user = Context.Users.Where(w => w.Username == key).Include(w => w.csvFiles).FirstOrDefault();
            if (user == null)
            {
                return new List<string>();
            }

            return user.csvFiles.Select(document => document.Name).ToList();
        }

        public void Add(string key, string value)
        {
            var user = Context.Users.Find(key);
            if (user == null)
            {
                user = new User { Username = key };
                Context.Users.Add(user);
            }

            var csvFile = Context.CsvFiles.Find(value);
            if (csvFile == null)
            {
                csvFile = new Csv() { Name = value };
                Context.CsvFiles.Add(csvFile);
            }

            if (user.csvFiles.Contains(csvFile))
            {
                return;
            }

            user.csvFiles.Add(csvFile);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}