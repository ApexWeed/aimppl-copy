using System.IO;

namespace AIMPPL_Copy
{
    public class Cover
    {
        public string Path;
        public long Size;

        public Cover(string path)
        {
            Path = path;

            Size = path == null ? 0 : new FileInfo(path).Length;
        }
    }
}
