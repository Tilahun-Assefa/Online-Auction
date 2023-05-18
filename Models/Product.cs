using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Models
{
    public class Product
    {
        #region properties
        ///<summary>
        ///The unique id and primary key for this city
        ///</summary>
        [Key]
        [Required]
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
        ///product image Url
        ///</summary>
        public string ImgPath { get; set; }
        #endregion

        #region Navigation Properties
        ///<summary>
        ///A list containing all the product-category related to this product
        ///</summary>        
        public virtual List<ProductCategory> ProductCategories { set; get; }

        ///<summary>
        ///A list containing all the reviews related to this product
        ///</summary>
        public virtual List<Review> Reviews { get; set; }
        #endregion
    }
}