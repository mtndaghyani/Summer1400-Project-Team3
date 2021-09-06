using System.Collections.Generic;
using System.IO;
using System.Linq;
using ETLLibrary.Database.Models;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;

namespace ETLLibrary
{
    public class CsvSerializer: ICsvSerializer
    {
        public List<List<string>> Serialize(Csv csv, string path)
        {
            var raw = File.ReadAllText(path);
            return raw.Split(csv.RowDelimiter)
                .Select(row => new List<string>(row.Split(csv.ColDelimiter)))
                .ToList();
        }
    }
}