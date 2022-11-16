using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineAuction.Models
{
    public class Review
    {
        #region constructor
        public Review() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this city
        ///</summary>
        [Key]
        [Required]
        public int Id { get; set; }       

        ///<summary>
        ///The time instance the review was done 
        ///</summary>
        public DateTime TimeStamp { get; set; }

        ///<summary>
        ///User who reviews the product
        ///</summary>
        public string User { get; set; }

        ///<summary>
        ///Customer's ratingof the product
        ///</summary>
        public float Rating { get; set; }

        ///<summary>
        ///Comment given for this product
        ///</summary>
        public string Comment { get; set; }

        ///<summary>
        ///Product id (foreign key)
        ///</summary>        
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        #endregion

        #region Navigation Properties
        ///<summary>
        /// The Product about which this review is given
        ///</summary>
        public virtual Product Product { get; set; }        
        #endregion
    }
}