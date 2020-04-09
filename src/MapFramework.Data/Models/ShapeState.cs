using System.Collections.Generic;
using System.Linq;
using MapFramework.Data.Entities;

namespace MapFramework.Data.Models
{
    public class ShapeState
    {
        public int gn_id { get; set; }
        public int ne_id { get; set; }
        public int scalerank { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string abbrev { get; set; }
        public string adm0_a3 { get; set; }
        public string adm1_code { get; set; }
        public string admin { get; set; }
        public string code_hasc { get; set; }
        public string featurecla { get; set; }
        public string fips { get; set; }
        public string geounit { get; set; }
        public string gn_a1_code { get; set; }
        public string gn_name { get; set; }
        public string gu_a3 { get; set; }
        public string iso_3166_2 { get; set; }
        public string iso_a2 { get; set; }
        public string name { get; set; }
        public string name_alt { get; set; }
        public string name_en { get; set; }
        public string postal { get; set; }
        public string region { get; set; }
        public string region_sub { get; set; }
        public string sov_a3 { get; set; }
        public string type { get; set; }
        public string type_en { get; set; }

        public static State ToState(ShapeState ss) => new State
        {
            Abbrev = ss.abbrev,
            Adm0A3 = ss.adm0_a3,
            Adm1Code = ss.adm1_code,
            Admin = ss.admin,
            CodeHasc = ss.code_hasc,
            FeatureCla = ss.featurecla,
            Fips = ss.fips,
            GeoUnit = ss.geounit,
            GnA1Code = ss.gn_a1_code,
            GnId = ss.gn_id,
            GnName = ss.gn_name,
            GuA3 = ss.gu_a3,
            Iso31662 = ss.iso_3166_2,
            IsoA2 = ss.iso_a2,
            Latitude = ss.latitude,
            Longitude = ss.longitude,
            Name = ss.name,
            NameAlt = ss.name_alt,
            NameEn = ss.name_en,
            NeId = ss.ne_id,
            Postal = ss.postal,
            Region = ss.region,
            RegionSub = ss.region_sub,
            ScaleRank = ss.scalerank,
            SovA3 = ss.sov_a3,
            Type = ss.type,
            TypeEn = ss.type_en
        };

        public static IEnumerable<State> ToStateList(IEnumerable<ShapeState> states) =>
            states.Select(s => ToState(s));
    }
}