using System.IO;

namespace AIMPPL_Copy
{
    public class Scan
    {
        public string Directory;
        public string Path;
        public long Size;

        public Scan(string path)
        {
            Path = path;
            Directory = System.IO.Path.GetDirectoryName(path);
            var lastIndex = Directory.LastIndexOf('\\');
            if (lastIndex == -1)
                lastIndex = Directory.LastIndexOf('/');
            Directory = Directory.Substring(lastIndex + 1);

            Size = path == null ? 0 : new FileInfo(path).Length;
        }
    }
}
