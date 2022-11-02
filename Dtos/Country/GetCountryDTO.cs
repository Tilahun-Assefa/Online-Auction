
namespace OnlineAuction.Dtos.Country
{
    public class GetCountryDTO
    {
        #region constructor
        public GetCountryDTO() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this country
        ///</summary>       
        public int Id { get; set; }

        ///<summary>
        ///Country name(in UTF8 format)
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///Country code (in ISO 3166-1 ALPHA-2 format)
        ///</summary>
        public string ISO2 { get; set; }

        ///<summary>
        ///Country code (in ISO 3166-1 ALPHA-3 format)
        ///</summary>
        public string ISO3 { get; set; }

        ///<summary>
        ///Number of cities in each country
        ///</summary>             
        public int TotCities { get; set; }
        #endregion
    }
}
