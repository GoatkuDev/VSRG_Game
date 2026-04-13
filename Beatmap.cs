using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhythm
{
    class Beatmap
    {
        public string? Title;
        public string? Artist;
        public string? Version;
        public string? FilePath;

        public override string ToString()
        {
            return $"{Artist} - {Title} [{Version}]";
        }
    }
}
