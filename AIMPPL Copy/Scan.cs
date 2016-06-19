using System.IO;

namespace AIMPPL_Copy
{
    public class Scan
    {
        public string Directory;
        public string File;
        public long Size;

        public Scan(string Path)
        {
            Directory = System.IO.Path.GetDirectoryName(Path);
            File = System.IO.Path.GetFileName(Path);

            Size = Path == null ? 0 : new FileInfo(Path).Length;
        }
    }
}
