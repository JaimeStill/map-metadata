using System;
using System.Collections.Generic;
using System.Linq;
using MapFramework.Data.Entities;

namespace MapFramework.Data.Models
{
    public class ShapeCountry
    {
        public int NE_ID { get; set; }
        public int scalerank { get; set; }
        public string ABBREV { get; set; }
        public string ADM0_A3 { get; set; }
        public string ADM0_A3_IS { get; set; }
        public string ADM0_A3_US { get; set; }
        public string ADMIN { get; set; }
        public string CONTINENT { get; set; }
        public string featurecla { get; set; }
        public string FIPS_10_ { get; set; }
        public string FORMAL_EN { get; set; }
        public string GEOUNIT { get; set; }
        public string GU_A3 { get; set; }
        public string ISO_A2 { get; set; }
        public string ISO_A3 { get; set; }
        public string ISO_N3 { get; set; }
        public string NAME { get; set; }
        public string NAME_EN { get; set; }
        public string NAME_LONG { get; set; }
        public string NAME_SORT { get; set; }
        public string POSTAL { get; set; }
        public string REGION_UN { get; set; }
        public string REGION_WB { get; set; }
        public string SOV_A3 { get; set; }
        public string SOVEREIGNT { get; set; }
        public string SUBREGION { get; set; }
        public string TYPE { get; set; }

        public static Country ToCountry(ShapeCountry sc) => new Country
        {
            Abbrev = sc.ABBREV,
            Adm0A3 = sc.ADM0_A3,
            Adm0A3Is = sc.ADM0_A3_IS,
            Adm0A3Us = sc.ADM0_A3_US,
            Admin = sc.ADMIN,
            Continent = sc.CONTINENT,
            FeatureCla = sc.featurecla,
            Fips10 = sc.FIPS_10_,
            FormalEn = sc.FORMAL_EN,
            GeoUnit = sc.GEOUNIT,
            GuA3 = sc.GU_A3,
            IsoA2 = sc.ISO_A2,
            IsoA3 = sc.ISO_A3,
            IsoN3 = sc.ISO_N3,
            Name = sc.NAME,
            NameEn = sc.NAME_EN,
            NameLong = sc.NAME_LONG,
            NameSort = sc.NAME_SORT,
            NeId = sc.NE_ID,
            Postal = sc.POSTAL,
            RegionUn = sc.REGION_UN,
            RegionWb = sc.REGION_WB,
            ScaleRank = sc.scalerank,
            SovA3 = sc.SOV_A3,
            Sovereignt = sc.SOVEREIGNT,
            SubRegion = sc.SUBREGION,
            Type = sc.TYPE
        };

        public static IEnumerable<Country> ToCountryList(IEnumerable<ShapeCountry> countries) =>
            countries.Select(c => ToCountry(c));
    }
}