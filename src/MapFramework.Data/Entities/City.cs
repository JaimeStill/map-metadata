namespace MapFramework.Data.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int? GeoNameId { get; set; }
        public int? NeId { get; set; }
        public int? ScaleRank { get; set; }
        public int StateId { get; set; }
        public int? UnFid { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? UnLat { get; set; }
        public double? UnLong { get; set; }
        public string Adm0A3 { get; set; }
        public string Adm0Name { get; set; }
        public string Adm1Name { get; set; }
        public string CityAlt { get; set; }
        public string IsoA2 { get; set; }
        public string LsName { get; set; }
        public string MegaName { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string NameAscii { get; set; }
        public string NameEn { get; set; }
        public string Sov0Name { get; set; }
        public string SovA3 { get; set; }
        public string Timezone { get; set; }
        public string UnAdm0 { get; set; }

        public State State { get; set; }
    }
}