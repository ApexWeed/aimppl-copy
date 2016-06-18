using System.IO;

namespace AIMPPL_Copy
{
    public class Cover
    {
        public string Path;
        public long Size;

        public Cover(string Path)
        {
            this.Path = Path;

            Size = Path == null ? 0 : new FileInfo(Path).Length;
        }
    }
}
