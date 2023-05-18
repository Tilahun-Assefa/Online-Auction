using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Dtos.Review
{
    public class UpdateReviewDto
    {
        #region properties
        ///<summary>
        ///The unique id and primary key for this city
        ///</summary>        
        public int Id { get; set; }

        ///<summary>
        ///The time instance the review was done 
        ///</summary>
        public DateTime TimeStamp { get; set; }

        ///<summary>
        ///User who reviews the product
        ///</summary>
        public string UserName { get; set; }

        ///<summary>
        ///Customer's rating of the product
        ///</summary>
        public float Rating { get; set; }

        ///<summary>
        ///Comment given for this product
        ///</summary>
        public string Comment { get; set; }

        ///<summary>
        ///Product id (foreign key)
        ///</summary>       
        public int ProductId { get; set; }

        #endregion
    }
}