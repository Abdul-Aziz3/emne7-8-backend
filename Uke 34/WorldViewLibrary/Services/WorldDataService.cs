using System.Linq;
using WorldModelLibrary.models;
using WorldViewLibrary.Services.Interfaces;
using WorldViewLibrary.Tools;

namespace WorldViewLibrary.Services
{
    public class WorldDataService : IWorldDataService
    {
        private readonly CountryService _countryService;
        private readonly CityService _cityService;
        private readonly CountryLanguageService _countryLanguageService;

        private List<Country>? _countries;        
        private List<CountryLanguage>? _countryLanguages;
        private List<City>? _cities;

        private readonly Action<string>? _logAction;

        public WorldDataService()
            : this(null)
        { }

        public WorldDataService( Action<string>? logAction)
        {
            _countryService = new CountryService( new CSVSplitter().Split, logAction);
            _cityService = new CityService(new CSVSplitter().Split, logAction);
            _countryLanguageService = new CountryLanguageService(new CSVSplitter().Split, logAction);
            _logAction = logAction;
        }

        protected void LogMessage(string message)
        {
            if (_logAction == null)
            {
                throw new Exception(message);                
            }
            _logAction?.Invoke(message);

        }

        public IEnumerable<City> Cities => _cities ?? Enumerable.Empty<City>();

        public IEnumerable<Country> Countries => _countries ?? Enumerable.Empty<Country>();

        public IEnumerable<CountryLanguage> CountryLanguages => _countryLanguages ?? Enumerable.Empty<CountryLanguage>();

        public bool IsLoaded => (_cities is null || _countries is null || _countryLanguages is null) ? false : true;

        public IEnumerable<Country> GetCountriesByName(string countryName)
        {
            Func<Country, bool> filter = countryName switch
            {
                "" => cntr => true,
                _ => cntr => cntr.Name.ToLower().Contains(countryName.ToLower()),
            };


            return Countries.Where(filter);

        }
        public IEnumerable<City> GetCitiesByCountryCode(string countryCode)
        {
            Func<City, bool> filter = countryCode.Equals(string.Empty)
                ? cty => true
                : cty => cty.CountryCode.ToLower().Contains(countryCode.ToLower());


            return Cities.Where(filter);
        }
        public IEnumerable<City> GetCitiesByCountryName(string countryName)
        {
            var country = Countries.FirstOrDefault( c => c.Name == countryName);
            if (country == null)
                return Enumerable.Empty<City>();

            return Cities.Where(city => String.Equals(city.CountryCode, country.Code, StringComparison.CurrentCultureIgnoreCase));
        }

        public City GetCityById(int cityId)
        {
            if (!Cities.Any(c => c.Id == cityId))
                return new City();

            return Cities.First(c => c.Id == cityId);
        }

        public Country GetCountryByCode(string countryCode)
        {
            return Countries.First( c => c.Code == countryCode);
        }

        public IEnumerable<CountryLanguage> GetCountryLanguageByCountryCode(string countryCode)
        {
            Func<CountryLanguage, bool> filter = countryCode.Equals(string.Empty)
                    ? cntr => true
                    : cntr => cntr.CountryCode.ToLower().Contains(countryCode.ToLower());

            return CountryLanguages.Where(filter);
        }

        public IEnumerable<CountryLanguage> GetCountryLanguageByCountryName(string countryName)
        {
            var country = Countries.FirstOrDefault(c => c.Name == countryName);
            if (country == null)
                return Enumerable.Empty<CountryLanguage>();

            

            return CountryLanguages.Where( l => l.CountryCode == country.Code);
        }

        public void LoadData(string countryFile, string cityFile, string countryLanguageFile)
        {
            _countries = _countryService.LoadCountries(countryFile).ToList();
            _countryLanguages = _countryLanguageService.LoadCountryLanguages(countryLanguageFile).ToList();
            _cities = _cityService.LoadCities(cityFile).ToList();
        }


    }
}
