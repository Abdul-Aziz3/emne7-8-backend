using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldModelLibrary.models;
using WorldViewLibrary.Services.Interfaces;
using WorldViewLibrary.Tools;

namespace WorldViewLibrary.Services
{
    public class CountryService : ServiceBase, ICountryService
    {
        private readonly Func<string, string[]> _csvSplitter;
            
        public Action<string>? Log { get; set; }

        public CountryService(Func<string, string[]> csvSplitter, Action<string>? logAction)
            : base(logAction)
        {
            _csvSplitter = csvSplitter;
        }

        public IEnumerable<Country> LoadCountries(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException($"File does not exist: {fileName}");
            }

            return new GAFileObjects<Country>().GetLineObjects(
               fileName,
               line => _csvSplitter(line),
               arr =>
               {
                   // Code,Name,Continent,Region,SurfaceArea,Population,LocalName,Capital,GovernmentForm

                   if (arr is [var code, var name, var continent, var region, var surfaceArea,var population, var localName, 
                       var capital, var GovernmentForm])
                   {
                       if (!float.TryParse(surfaceArea, CultureInfo.InvariantCulture, out float surfArea))
                           LogMessage($"Failed to parse Country.SurfaceArea:{surfaceArea} as float ({string.Join(",", arr)})");                       
                       if (!int.TryParse(population, out int pop))
                           LogMessage($"Failed to parse  Country.Population:{population} as int ({string.Join(",", arr)})");
                       if (!int.TryParse(capital, out int cap))
                           LogMessage($"Failed to parse  Country.Capital:{capital} as int ( {string.Join(",", arr)} )");

                       return new Country()
                       {
                           Code = code,
                           Name = name,
                           Continent = continent,
                           Region = region,
                           SurfaceArea = surfArea,
                           //IndepYear = indYear,
                           Population = pop,
                           //LifeExpectancy = lifeExp,
                           //GNP = gnpf,
                           //GNP_Old = gnpfOld,
                           LocalName = localName,
                           //GovernmentForm = governmentForm,
                           //HeadOfState = headOfState,
                           Capital = cap,
                           //Code2 = code2
                       };

                       //return new Country(code, name, continent, region, surfArea, indYear,
                       //   pop, lifeExp, gnpf, gnpfOld, localName, governmentForm, headOfState, cap, code2);

                   }
                    LogMessage($"FAILED TO PARSE CSV line for Country ({string.Join(",", arr)})");
                    return null;
               });
        }
    }
}
