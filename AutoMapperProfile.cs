using System.Linq;
using AutoMapper;
using OnlineAuction.Dtos.Category;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Dtos.ProductCategory;
using OnlineAuction.Models;

namespace OnlineAuction
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>()
            .ForMember(dto => dto.Categories, x => x.MapFrom(p => p.ProductCategories.Select(pc => pc.Category)));
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Review, GetReviewDto>();
            CreateMap<AddReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<ProductCategory, GetProductCategoryDto>();
        }

    }
}