using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldModelLibrary.models;

namespace WorldViewLibrary.Services.Interfaces
{
    public interface IWorldDataService
    {
        void LoadData(string countryFile, string cityFile, string countryLanguageFile);

        bool IsLoaded { get;  }

        IEnumerable<Country> Countries { get; }
        IEnumerable<City> Cities { get; }
        IEnumerable<CountryLanguage> CountryLanguages { get; }

        IEnumerable<Country> GetCountriesByName(string countryName);

        Country GetCountryByCode(string countryCode);
        IEnumerable<City> GetCitiesByCountryCode(string countryCode);
        IEnumerable<City> GetCitiesByCountryName(string countryName);

        IEnumerable<CountryLanguage> GetCountryLanguageByCountryName(string countryName);
        IEnumerable<CountryLanguage> GetCountryLanguageByCountryCode(string countryCode);

        City GetCityById(int cityId);

        
        

        
    }
}
