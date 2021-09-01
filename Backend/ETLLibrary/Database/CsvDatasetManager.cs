using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ETLLibrary.Interfaces;

namespace ETLLibrary.Database
{
    public class CsvDatasetManager : ICsvDatasetManager
    {
        private ConnectorMapper _mapper;
        private const string Path = "csvFiles";

        public CsvDatasetManager(EtlContext context)
        {
            _mapper = new ConnectorMapper(context);
        }
        
        public void SaveCsv(Stream stream, string username, string fileName, long fileLength)
        {
            EnsureDirectoryCreated(Path);
            EnsureUserDirectoryCreated(Path, username);

            if (!File.Exists(Path + "/" + username + "/" + fileName))
            {
                _mapper.Add(username, fileName);

                var bytes = new byte[fileLength];
                stream.Read(bytes);
                var content = bytes.Aggregate("", (current, b) => current + Convert.ToChar(b));

                File.WriteAllText(Path + "/" + username + "/" + fileName, content);
            }
        }

        private void EnsureDirectoryCreated(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        
        private void EnsureUserDirectoryCreated(string path, string username)
        {
            if (!Directory.Exists(path+"/"+username))
            {
                Directory.CreateDirectory(path+"/"+username);
            }
        }

        public List<string> GetCsvFiles(string username)
        {
            return _mapper.Get(username);
        }

        public string GetCsvContent(string username, string fileName)
        {
            if (FileExists(username, fileName))
            {
                string content = File.ReadAllText(Path + "/" + username + "/" + fileName);
                return content;
            }
            else
            {
                return null;
            }
        }

        private bool FileExists(string username, string fileName)
        {
            return Directory.Exists(Path) && Directory.Exists(Path+"/"+username) && File.Exists(Path+"/" + username + "/" + fileName);
        }
    }
}