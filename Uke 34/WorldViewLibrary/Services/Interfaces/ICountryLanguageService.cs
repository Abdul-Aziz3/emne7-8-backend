using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldModelLibrary.models;
using WorldViewLibrary.Tools;

namespace WorldViewLibrary.Services.Interfaces
{
    public interface ICountryLanguageService
    {
        IEnumerable<CountryLanguage> LoadCountryLanguages(string fileName);
    }
}
