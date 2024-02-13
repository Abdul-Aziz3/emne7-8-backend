
using WorldModelLibrary.models;
using WorldViewLibrary.Services.Interfaces;
using WorldViewLibrary.Tools;

namespace WorldViewLibrary.Services;

public class CityService : ServiceBase, ICityService
{
    private readonly Func<string, string[]> _csvSplitter;

    public CityService(Func<string, string[]> csvSplitter, Action<string>? logAction)
        : base(logAction)
    {
        _csvSplitter = csvSplitter;
    }
    public IEnumerable<City> LoadCities(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"File does not exist: {fileName}");
        }

        // 3 parameter 1: filenavn, 2: delegate (hvordan splitte linjene) 3: hvordan tolke arrayen?
        return new GAFileObjects<City>().GetLineObjects(
            fileName,
            line => _csvSplitter(line),
            arr =>
            {                    
                if (arr is [var idStr, var name, var countryCode, var district, var populationStr])
                {
                    if (!int.TryParse(idStr, out int id))
                    {
                        LogMessage($"Failed to parse City.Id {idStr} as int ({string.Join(",", arr)})");
                    }

                    if (!int.TryParse(populationStr, out int population))
                    {
                        LogMessage($"Failed to parse City.Population:{populationStr} as int ({string.Join(",", arr)})");
                    }

                    //return new City(id, district, countryCode, name, population);
                    return new City() { Id = id, District = district, CountryCode = countryCode, Name = name, Population = population };


                }
                LogMessage($"FAILED TO PARSE CSV line for City ({string.Join(",", arr)})");
                return null;
            });
    }
}
