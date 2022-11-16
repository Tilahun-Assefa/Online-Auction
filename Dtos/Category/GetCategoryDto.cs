namespace OnlineAuction.Dtos.Category
{
    public class GetCategoryDto
    {
        #region constructor
        public GetCategoryDto() { }
        #endregion

        #region properties
        ///<summary>
        ///Product name(in UTF8 format)
        ///</summary>
        public string Name { get; set; }
        #endregion
    }
}