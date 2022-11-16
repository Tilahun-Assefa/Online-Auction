using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Models
{
    public class ProductCategory
    {
        
        #region constructor
        public ProductCategory() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this productcategory
        ///</summary>
        [Key]
        [Required]
        public int ProductId { get; set; }

        ///<summary>
        ///The unique id and primary key for this productcategory
        ///</summary>
        [Key]
        [Required]
        public int CategoryId { get; set; }
        #endregion

        #region Navigation Properties
        ///<summary>
        ///A product which is related to this product-category 
        ///</summary>        
        public virtual Product Product { set; get; }

        ///<summary>
        ///A category which is related to this product-category 
        ///</summary>
        public virtual Category Category { get; set; }
        #endregion
    }
}