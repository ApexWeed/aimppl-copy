using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AIMPPL_Copy
{
    public class FormatChange
    {
        public Song Song;
        public string NewExtension;

        public FormatChange(Song Song, string Extension)
        {
            this.Song = Song;
            this.NewExtension = Extension;
        }

        public override string ToString()
        {
            return $"{Song.ToString()} ({Path.GetExtension(Song.Path).Substring(1)} -> {NewExtension})";
        }
    }
}
