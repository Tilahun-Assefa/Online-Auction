using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineAuction.Models
{
    public class Category
    {       

        #region constructor
        public Category() { }
        #endregion

        #region properties
        ///<summary>
        ///The unique id and primary key for this city
        ///</summary>
        [Key]
        [Required]
        public int Id { get; set; }

        ///<summary>
        ///Category name(in UTF8 format)
        ///</summary>
        public string Name { get; set; }        
        #endregion

        #region Navigation Properties
        ///<summary>
        ///A list containing all the product-category related to this category
        ///</summary>        
        public virtual List<ProductCategory> ProductCategories { set; get; }
        #endregion
    }
}