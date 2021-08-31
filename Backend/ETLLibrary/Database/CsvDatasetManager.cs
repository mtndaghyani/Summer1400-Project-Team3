using System;
using System.IO;
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
                _mapper.Save();

                string content = "";
                int c = 0;
                byte[] bytes = new byte[fileLength];
                stream.Read(bytes);
                foreach (var b in bytes)
                {
                    content += Convert.ToChar(b);
                }

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
    }
}