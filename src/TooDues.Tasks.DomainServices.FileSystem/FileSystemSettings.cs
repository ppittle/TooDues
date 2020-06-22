using System;

namespace TooDues.Tasks.DomainServices.FileSystem
{
    public class FileSystemSettings
    {
        public string Path { get; set; } = Environment.CurrentDirectory;
    }
}