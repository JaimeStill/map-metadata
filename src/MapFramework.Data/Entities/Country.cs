using System.Collections.Generic;

namespace MapFramework.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public int? NeId { get; set; }
        public int? ScaleRank { get; set; }
        public string Abbrev { get; set; }
        public string Adm0A3 { get; set; }
        public string Adm0A3Is { get; set; }
        public string Adm0A3Us { get; set; }
        public string Admin { get; set; }
        public string Continent { get; set; }
        public string FeatureCla { get; set; }
        public string Fips10 { get; set; }
        public string FormalEn { get; set; }
        public string GeoUnit { get; set; }
        public string GuA3 { get; set; }
        public string IsoA2 { get; set; }
        public string IsoA3 { get; set; }
        public string IsoN3 { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string NameLong { get; set; }
        public string NameSort { get; set; }
        public string Postal { get; set; }
        public string RegionUn { get; set; }
        public string RegionWb { get; set; }
        public string SovA3 { get; set; }
        public string Sovereignt { get; set; }
        public string SubRegion { get; set; }
        public string Type { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}