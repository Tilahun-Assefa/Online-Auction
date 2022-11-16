namespace OnlineAuction.Dtos.ProductCategory
{
    public class AddProductCategoryDto
    {

        #region constructor
        public AddProductCategoryDto() { }
        #endregion

        #region properties
        ///<summary>
        ///The product id and part of primary key for this product-category
        ///</summary>       
        public int ProductId { get; set; }

        ///<summary>
        ///The category id and part of primary key for this product-category
        ///</summary>       
        public int CategoryId { get; set; }        
        
        #endregion
    }
}