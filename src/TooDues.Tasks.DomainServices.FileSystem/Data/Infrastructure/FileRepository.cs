using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace TooDues.Tasks.DomainServices.FileSystem.Data.Infrastructure
{
    internal class FileRepository<TData>
        where TData : class, new()
    {
        private static string _filePath = "";

        private static TData? _data;

        public TData Data
        {
            get
            {
                #pragma warning disable 8603
                return _data;
                #pragma warning restore 8603
            }
        }

        private static readonly object DataWriteLock = new object();

        public FileRepository(
            IOptions<FileSystemSettings> options, 
            string tableName)
        {
            if (_data != null)
                return;

            lock (DataWriteLock)
            {
                if (_data != null)
                    return;

                _filePath = Path.Combine(options.Value.Path, $"{tableName}.json");

                if (TryReadFromDisk(out var json))
                {
                    _data = JsonConvert.DeserializeObject<TData>(json);
                }
                else
                {
                    _data = new TData();
                }
            }
        }

        public void FlushToDisk()
        {
            lock (DataWriteLock)
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(_data));
            }
        }

        private static bool TryReadFromDisk(out string json)
        {
            json = string.Empty;
            try
            {
                json = File.ReadAllText(_filePath);
                return true;
            }
            catch
            {
                // ignored
            }

            return false;
        }
    }
}