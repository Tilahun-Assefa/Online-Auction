using System;

namespace OnlineAuction.Dtos.Review
{
    public class GetReviewDto
    {
        #region constructor
        public GetReviewDto() { }
        #endregion

        #region properties
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

        #endregion
    }
}