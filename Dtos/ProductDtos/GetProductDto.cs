using System.Collections.Generic;
using AutoMapper;
using OnlineAuction.Dtos.ReviewDtos;
using OnlineAuction.Models;

namespace OnlineAuction.Dtos.ProductDtos
{
    public class GetProductDto : IMapFrom<Product>
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

        public void Mapping(Profile profile)
        {
            var c = profile.CreateMap<Product, GetProductDto>()
                .ForMember(d => d.ImgPath, opt => opt.NullSubstitute("N/A"));
        }
    }
}