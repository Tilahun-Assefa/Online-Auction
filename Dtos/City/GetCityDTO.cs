using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuction.Dtos.City
{
    public class GetCityDTO
    {
        #region constructor
        public GetCityDTO() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this city
        ///</summary>       
        public int Id { get; set; }

        ///<summary>
        ///City name(in UTF8 format)
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///City name(in ASCII format)
        ///</summary>
        public string Name_ASCII { get; set; }

        ///<summary>
        ///City latitude
        ///</summary>
        public decimal Lat { get; set; }

        ///<summary>
        ///City longitude
        ///</summary>

        public decimal Lon { get; set; }

        ///<summary>
        ///Country id (foreign key)
        ///</summary>        
        public int CountryId { get; set; }
        
        ///<summary>
        ///The Country name related to this city
        ///</summary>        
        public string CountryName { get; set; }
        #endregion
    }
}
