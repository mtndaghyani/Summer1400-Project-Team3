using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ETLLibrary.Database.Gataways;
using ETLLibrary.Database.Models;
using ETLLibrary.Database.Utils;
using ETLLibrary.Interfaces;

namespace ETLLibrary.Database.Managers
{
    public class CsvDatasetManager : ICsvDatasetManager
    {
        private const string Path = "csvFiles";
        private CsvGateway _gateway;
        private ICsvSerializer _serializer;

        public CsvDatasetManager(EtlContext context, ICsvSerializer serializer)
        {
            _serializer = serializer;
            _gateway = new CsvGateway(context);
        }

        public void SaveCsv(Stream stream, string username, string fileName, CsvInfo info, long fileLength)
        {
            EnsureDirectoryCreated(Path);
            EnsureUserDirectoryCreated(Path, username);

            if (!File.Exists(GetFilePath(username, fileName)))
            {
                _gateway.AddDataset(username, fileName, info);

                var bytes = new byte[fileLength];
                stream.Read(bytes);
                var content = bytes.Aggregate("", (current, b) => current + Convert.ToChar((byte) b));

                File.WriteAllText(GetFilePath(username, fileName), content);
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
            if (!Directory.Exists(path + "/" + username))
            {
                Directory.CreateDirectory(path + "/" + username);
            }
        }

        public List<string> GetCsvFiles(string username)
        {
            return _gateway.GetUserDatasets(username);
        }

        public List<List<string>> GetCsvContent(User user, string fileName)
        {
            if (FileExists(user.Username, fileName))
            {
                var csv = _gateway.GetDataset(fileName, user.Id);
                return _serializer.Serialize(csv, GetFilePath(user.Username, fileName));
            }

            return null;
        }

        private static string GetFilePath(string username, string fileName)
        {
            return Path + "/" + username + "/" + fileName;
        }

        private bool FileExists(string username, string fileName)
        {
            return Directory.Exists(Path) && Directory.Exists(GetUserDirectoryPath(username)) &&
                   File.Exists(Path + "/" + username + "/" + fileName);
        }

        private static string GetUserDirectoryPath(string username)
        {
            return Path + "/" + username;
        }

        public void DeleteCsv(User user, string fileName)
        {
            File.Delete(GetFilePath(user.Username, fileName));
            _gateway.DeleteDataset(fileName, user.Id);
        }
    }
}