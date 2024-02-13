using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldModelLibrary.models;
using WorldViewLibrary.Services.Interfaces;
using WorldViewLibrary.Tools;

namespace WorldViewLibrary.Services;

public class CountryLanguageService : ServiceBase, ICountryLanguageService
{
    private readonly Func<string, string[]> _csvSplitter;
    
    public CountryLanguageService(Func<string, string[]> csvSplitter, Action<string>? logAction)
        : base(logAction)
    {
        _csvSplitter = csvSplitter;
    }
    public IEnumerable<CountryLanguage> LoadCountryLanguages(string fileName)
    {
        if (!File.Exists(fileName))
        {
            throw new FileNotFoundException($"File does not exist: {fileName}");
        }

        // CountryCode,Language,IsOfficial,Percentage
        // ABW,Dutch,T,5.3
        return new GAFileObjects<CountryLanguage>().GetLineObjects(
            fileName,
            line => _csvSplitter(line),
            arr =>
            {
                if (arr is [var countryCode, var language, var isOfficial, var percentageStr])
                {
                    var official = true && isOfficial.ToLower().Equals("T");
                    if (!float.TryParse(percentageStr, CultureInfo.InvariantCulture, out float percentage))
                    {
                        LogMessage($"Failed to parse Country.SurfaceArea:{percentage} as float ({string.Join(",", arr)})");
                    }


                    return new CountryLanguage()
                    {
                        CountryCode = countryCode,
                        Language = language,
                        IsOfficial = official,
                        Percentage = percentage
                    };
                    //return new CountryLanguage(countryCode, language, official, percentage);
                }

                LogMessage($"FAILED TO PARSE CSV line for CountryLanguage ({string.Join(",", arr)})");
                return null;
            });
    }


}
