using System.Collections.Generic;
using System.IO;
using System.Linq;
using ETLLibrary.Database.Models;
using ETLLibrary.Interfaces;

namespace ETLLibrary
{
    public class CsvSerializer : ICsvSerializer
    {
        public List<List<string>> Serialize(Csv csv, string path)
        {
            var raw = File.ReadAllText(path);
            var data = raw.Split(csv.RowDelimiter)
                .Select(row => new List<string>(row.Split(csv.ColDelimiter)))
                .ToList();
            if (csv.HasHeader)
            {
                return data;
            }

            var result = new List<List<string>> {GetDefaultHeaders(GetRowLength(data))};
            result.AddRange(data);
            return result;
        }

        private int GetRowLength(List<List<string>> data)
        {
            return data[0].Count;
        }

        private List<string> GetDefaultHeaders(int length)
        {
            var headers = new List<string>();
            for (int i = 0; i < length; i++)
            {
                headers.Add($"field{i}");
            }

            return headers;
        }
    }
}