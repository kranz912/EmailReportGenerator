using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailReportGenerator.Helpers
{
    class CSVHelper
    {
        public static async Task sortCSV(string path) {
            string[] lines = File.ReadAllLines(path);
            var data = lines.Skip(1);
            var sorted = data.Select(line => new
            {
                Sortkey = Int32.Parse(line.Split(',')[7]),
                Line = line
            })
            .OrderByDescending(x => x.Sortkey)
            .Select(x => x.Line);
            File.WriteAllLines(path, lines.Take(1).Concat(sorted));
        }
    }
}
