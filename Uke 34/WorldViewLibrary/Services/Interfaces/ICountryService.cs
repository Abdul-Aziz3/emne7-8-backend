using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldModelLibrary.models;
using WorldViewLibrary.Tools;

namespace WorldViewLibrary.Services.Interfaces
{
    public interface ICountryService
    {
        IEnumerable<Country> LoadCountries(string fileName);

    }
}
