using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Review;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Models
{
    public class Product
    {
        #region constructor
        public Product() { }
        #endregion

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