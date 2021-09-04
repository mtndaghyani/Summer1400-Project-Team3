using System.Collections.Generic;
using System.Linq;
using ETLLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETLLibrary.Database
{
    public class CsvGateway : IDatabaseGateway
    {
        public readonly EtlContext Context;

        public CsvGateway(EtlContext context)
        {
            Context = context;
        }


        public List<string> GetUserDatasets(string username)
        {
            var user = Context.Users.Where(w => w.Username == username).Include(w => w.CsvFiles).Single();
            return user.CsvFiles.Select(document => document.Name).ToList();
        }

        public void AddDataset(string username, DatasetInfo info)
        {
            var user = Context.Users.Include(x => x.CsvFiles).Single(u => u.Username == username);
            var csvFile = new Csv() { Name = info.Name, User = user };
            user.CsvFiles.Add(csvFile);
            Context.SaveChanges();
        }

        public void DeleteDataset(string fileName, int userId)
        {
            var csv = Context.CsvFiles.SingleOrDefault(x => x.Name == fileName && x.UserId == userId);
            if (csv == null) return;
            Context.CsvFiles.Remove(csv);
            Context.SaveChanges();
        }
        
    }
}