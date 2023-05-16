using System.Collections.Generic;
using OnlineAuction.Dtos.Category;

namespace OnlineAuction.Dtos.Product
{
    public class AddProductDto
    {
        #region constructor
        public AddProductDto() { }
        #endregion

        #region properties

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
        ///product image path
        ///</summary>
        public string ImgPath { get; set; }        

        ///<summary>
        ///Product Customer rating 
        ///</summary>
        public float Rating { get; set; }

        ///<summary>
        ///Array of categories
        ///</summary>             
        public string[] Categories { get; set; }
        #endregion
    }
}