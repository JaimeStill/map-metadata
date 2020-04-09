using System.Collections.Generic;

namespace MapFramework.Data.Entities
{
    public class State
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? GnId { get; set; }
        public int? NeId { get; set; }
        public int? ScaleRank { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Abbrev { get; set; }
        public string Adm0A3 { get; set; }
        public string Adm1Code { get; set; }
        public string Admin { get; set; }
        public string CodeHasc { get; set; }
        public string FeatureCla { get; set; }
        public string Fips { get; set; }
        public string GeoUnit { get; set; }
        public string GnA1Code { get; set; }
        public string GnName { get; set; }
        public string GuA3 { get; set; }
        public string Iso31662 { get; set; }
        public string IsoA2 { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string NameEn { get; set; }
        public string Postal { get; set; }
        public string Region { get; set; }
        public string RegionSub { get; set; }
        public string SovA3 { get; set; }
        public string Type { get; set; }
        public string TypeEn { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}