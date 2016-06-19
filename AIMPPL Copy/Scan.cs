using System.IO;

namespace AIMPPL_Copy
{
    public class Scan
    {
        public string Path;
        public string Directory;
        public long Size;

        public Scan(string Path)
        {
            this.Path = Path;
            Directory = System.IO.Path.GetDirectoryName(Path);
            var lastIndex = Directory.LastIndexOf('\\');
            if (lastIndex == -1)
            {
                lastIndex = Directory.LastIndexOf('/');
            }
            Directory = Directory.Substring(lastIndex + 1);

            Size = Path == null ? 0 : new FileInfo(Path).Length;
        }
    }
}
