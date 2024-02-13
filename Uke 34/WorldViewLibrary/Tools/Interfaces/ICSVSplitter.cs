using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldViewLibrary.Tools.Interfaces
{
    public interface ICSVSplitter
    {
        string[] Split(string input);
        string[] Split(string input, char forceReadChar = '\"');
    }
}
