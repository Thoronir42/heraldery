using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heraldery.Blazon.IO
{
    abstract class CsvLoader<T>
    {
        private string fileName;

        public CsvLoader(String fileName)
        {
            this.fileName = fileName;
        }

        public List<T> LoadItems()
        {
            List<T> list = new List<T>();

            var lines = File.ReadLines(this.fileName);
            foreach (string line in lines)
            {
                list.Add(ParseLine(line.Split(';')));
            }

            return list;
        }

        abstract protected T ParseLine(string[] line);
    }
}
