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
            CreateMap<AddProductDto, Product>();
            CreateMap<Product, GetProductDto>()
            .ForMember(dto => dto.Categories, x => x.MapFrom(p => p.ProductCategories.Select(pc => pc.Category)));
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewDto, UpdateReviewDto>();
            CreateMap<UpdateReviewDto, ReviewDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<ProductCategory, GetProductCategoryDto>();
        }

    }
}