using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    Code,Name,Continent,Region,SurfaceArea,IndepYear,Population,LifeExpectancy,GNP,GNPOld,LocalName,GovernmentForm,HeadOfState,Capital,Code2
    ABW,Aruba,"North America",Caribbean,193.00,NULL,103000,78.4,828.00,793.00,Aruba,"Nonmetropolitan Territory of The Netherlands",Beatrix,129,AW
 */

namespace WorldModelLibrary.models
{

    public sealed class Country
    {


        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Continent { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public float SurfaceArea { get; set; }
        //public int IndepYear { get; set; }
        public int Population { get; set; }
        //public float LifeExpectancy { get; set; }
        //public float GNP { get; set;}
        //public float GNP_Old { get; set; }
        public string LocalName { get; set; } = string.Empty;
        //public string GovernmentForm { get; set; } = string.Empty;
        //public string HeadOfState { get; set; } = string.Empty;
        public int Capital { get; set; }
        //public string Code2 { get; set; } = string.Empty;

        //public Dictionary<int, City>? Cities { get; set; }

        //public Dictionary<string, CountryLanguage>? Languages { get; set; }
        /*
        public void AddCity(City city)
        {
            if (Cities == null)
                Cities = new();

            if (!Cities.ContainsKey(city.Id))
                Cities.Add(city.Id, city);
        }

        public void AddLanguage(CountryLanguage language)
        {
            if (Languages == null)
                Languages = new();

            if (!Languages.ContainsKey(language.Language))
                Languages.Add(language.Language, language);
        }
        */
        public override string ToString()
        {
            return $"Code: {Code}, Name:{Name}, Continent:{Continent}, Region:{Region}," +
                $"SurfaceArea: {SurfaceArea},  Population:{Population}," +
                $"LocalName:{LocalName}, " +
                $" Capital:{Capital}";
            /*
              return $"Code: {Code}, Name:{Name}, Continent:{Continent}, Region:{Region}," +
                $"SurfaceArea: {SurfaceArea}, IndepYear:{IndepYear}, Population:{Population}, LifeExpectancy:{LifeExpectancy}," +
                $"GNP: {GNP}, GNP_Old:{GNP_Old}, LocalName:{LocalName}, GovernmentForm:{GovernmentForm}," +
                $"HeadOfState: {HeadOfState}, Capital:{Capital}, Code2:{Code2}";
             */
        }

    }

}
