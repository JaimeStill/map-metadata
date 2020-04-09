using System.Collections.Generic;
using System.Linq;
using MapFramework.Data.Entities;

namespace MapFramework.Data.Models
{
    public class ShapeCity
    {
        public int GEONAMEID { get; set; }
        public int ne_id { get; set; }
        public int scalerank { get; set; }
        public int UN_FID { get; set; }
        public double LATITUDE { get; set; }
        public double LONGITUDE { get; set; }
        public double UN_LAT { get; set; }
        public double UN_LONG { get; set; }
        public string ADM0_A3 { get; set; }
        public string ADM0NAME { get; set; }
        public string ADM1NAME { get; set; }
        public string CITYALT { get; set; }
        public string ISO_A2 { get; set; }
        public string LS_NAME { get; set; }
        public string MEGANAME { get; set; }
        public string NAME { get; set; }
        public string NAMEALT { get; set; }
        public string NAMEASCII { get; set; }
        public string name_en { get; set; }
        public string SOV0NAME { get; set; }
        public string SOV_A3 { get; set; }
        public string TIMEZONE { get; set; }
        public string UN_ADM0 { get; set; }

        public static City ToCity(ShapeCity sc) => new City
        {
            Adm0A3 = sc.ADM0_A3,
            Adm0Name = sc.ADM0NAME,
            Adm1Name = sc.ADM1NAME,
            CityAlt = sc.CITYALT,
            GeoNameId = sc.GEONAMEID,
            IsoA2 = sc.ISO_A2,
            Latitude = sc.LATITUDE,
            Longitude = sc.LONGITUDE,
            LsName = sc.LS_NAME,
            MegaName = sc.MEGANAME,
            Name = sc.NAME,
            NameAlt = sc.NAMEALT,
            NameAscii = sc.NAMEASCII,
            NameEn = sc.name_en,
            NeId = sc.ne_id,
            ScaleRank = sc.scalerank,
            Sov0Name = sc.SOV0NAME,
            SovA3 = sc.SOV_A3,
            Timezone = sc.TIMEZONE,
            UnAdm0 = sc.UN_ADM0,
            UnFid = sc.UN_FID,
            UnLat = sc.UN_LAT,
            UnLong = sc.UN_LONG
        };

        public static IEnumerable<City> ToCityList(IEnumerable<ShapeCity> cities) =>
            cities.Select(c => ToCity(c));
    }
}