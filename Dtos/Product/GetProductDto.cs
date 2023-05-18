using System.Collections.Generic;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Review;

namespace OnlineAuction.Dtos.Product
{
    public class GetProductDto
    {
        #region properties
        ///<summary>
        ///The unique id and primary key for this product
        ///</summary>       
        public int Id { get; set; }

        ///<summary>
        ///Product name(in UTF8 format)
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///Product price 
        ///</summary>
        public float Price { get; set; }

        ///<summary>
        ///product description
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        ///product image path
        ///</summary>
        public string ImgPath { get; set; }

        ///<summary>
        ///List of categories
        ///</summary>             
        public List<string> Categories { set; get; }

        ///<summary>
        ///List of reviews
        ///</summary>  
        public List<ReviewDto> Reviews { get; set; }
        #endregion
    }
}