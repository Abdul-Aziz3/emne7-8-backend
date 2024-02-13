using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
    CountryCode,Language,IsOfficial,Percentage
    ABW,Dutch,T,5.3
 */
namespace WorldModelLibrary.models
{
    //public record CountryLanguage(string CountryCode, string Language, bool IsOfficial, float Percentage);
    
    public sealed class CountryLanguage
    {
        public string CountryCode { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public bool IsOfficial { get; set; }
        public float Percentage { get; set; }

        public override string ToString()
        {
            return $"CountryCode: {CountryCode}, Language:{Language}, IsOfficial:{IsOfficial}, Percentage:{Percentage}";
        }
    }
 }
