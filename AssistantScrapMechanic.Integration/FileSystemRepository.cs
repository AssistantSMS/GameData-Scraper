using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AssistantScrapMechanic.Integration
{
    public class FileSystemRepository
    {
        private readonly string _jsonDirectory;
        private readonly JsonSerializerSettings _jsonSettings;

        public FileSystemRepository(string jsonDirectory)
        {
            _jsonDirectory = jsonDirectory;
            _jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public Dictionary<string, dynamic> LoadJsonDict(string fileName)
        {
            string jsonFilePath = Path.Combine(_jsonDirectory, fileName);
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                Dictionary<string, dynamic> result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
                return result;
            }
            catch
            {
                //unused
            }

            return new Dictionary<string, dynamic>();
        }

        public Dictionary<string, T> LoadJsonDictOfType<T>(string fileName)
        {
            string jsonFilePath = Path.Combine(_jsonDirectory, fileName);
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                Dictionary<string, T> result = JsonConvert.DeserializeObject<Dictionary<string, T>>(json);
                return result;
            }
            catch
            {
                //unused
            }

            return new Dictionary<string, T>();
        }

        public List<T> LoadListJsonFile<T>(string fileName)
        {
            string jsonFilePath = Path.Combine(_jsonDirectory, fileName);
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                List<T> result = JsonConvert.DeserializeObject<List<T>>(json);
                return result;
            }
            catch
            {
                //unused
            }

            return new List<T>();
        }

        public T LoadJsonFile<T>(string fileName)
        {
            string jsonFilePath = Path.Combine(_jsonDirectory, fileName);
            try
            {
                string json = File.ReadAllText(jsonFilePath);
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            catch(Exception unused)
            {
                //unused
            }

            return default;
        }

        public void WriteBackToJsonFile(object jsonObj, string fileName)
        {
            string jsonFilePath = Path.Combine(_jsonDirectory, fileName);
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }

            int directorySeparatorIndex = jsonFilePath.LastIndexOf(Path.DirectorySeparatorChar);
            if (directorySeparatorIndex > 0)
            {
                string dir = jsonFilePath.Remove(directorySeparatorIndex, jsonFilePath.Length - directorySeparatorIndex);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }

            string json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented, _jsonSettings);
            File.WriteAllText(jsonFilePath, json);
        }
        public void WriteJsonFile(object jsonObj, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            int directorySeparatorIndex = fileName.LastIndexOf(Path.DirectorySeparatorChar);
            if (directorySeparatorIndex > 0)
            {
                string dir = fileName.Remove(directorySeparatorIndex, fileName.Length - directorySeparatorIndex);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            }

            string json = JsonConvert.SerializeObject(jsonObj, Formatting.Indented, _jsonSettings);
            File.WriteAllText(fileName, json);
        }

        public string GetRelPath(string folder, string partial)
        {
            return Path.Combine(folder, partial);
        }

        public bool FileExists(string folder, string appId)
        {
            string path = Path.Combine(_jsonDirectory, folder, $"{appId}.png");
            return File.Exists(path);
        }

    }
}
