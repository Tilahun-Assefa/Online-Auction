using System.Collections.Generic;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Review;

namespace OnlineAuction.Dtos.Product
{
    public class GetProductDto
    {
        #region constructor
        public GetProductDto() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this product
        ///</summary>       
        public int Id { get; set; }

        ///<summary>
        ///Product name(in UTF8 format)
        ///</summary>
        public string Title { get; set; }

        ///<summary>
        ///Product price 
        ///</summary>
        public float Price { get; set; }

        ///<summary>
        ///product description
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        ///Product Customer rating 
        ///</summary>
        public float Rating { get; set; }        
        #endregion
    }
}