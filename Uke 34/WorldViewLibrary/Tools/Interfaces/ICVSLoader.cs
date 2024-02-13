using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldViewLibrary.Tools.Interfaces
{
    internal interface ICVSLoader
    {
        public interface ICSVLoader<T>        {
            IEnumerable<T> LoadFromCSV(string fileName, Func<string, string[]> splitFunc, Func<string[], T?> parse);
        }
    }
}
