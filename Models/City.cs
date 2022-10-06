using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuction.Models
{
    public class City
    {
        #region constructor
        public City() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this city
        ///</summary>
        [Key]
        [Required]
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
        [Column(TypeName = "decimal(7,4)")]
        public decimal Lat { get; set; }

        ///<summary>
        ///City longitude
        ///</summary>
        [Column(TypeName = "decimal(7,4)")] 
        public decimal Lon { get; set; }

        ///<summary>
        ///Country id (foreign key)
        ///</summary>        
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        #endregion

        #region Navigation Properties
        ///<summary>
        ///The Country related to this city
        ///</summary>        
        public virtual Country Country { get; set; }
        #endregion
    }
}
