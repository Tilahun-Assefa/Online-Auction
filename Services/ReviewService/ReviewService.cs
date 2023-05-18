using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Dtos.ProductCategory;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Models;

namespace OnlineAuction.services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ReviewService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<UpdateReviewDto>>> GetAllReviewsByProductId(int id)
        {
            ServiceResponse<List<UpdateReviewDto>> serviceResponse = new ServiceResponse<List<UpdateReviewDto>>();
            List<Review> reviews = await _context.Reviews.Include(r => r.Product)
                .Where(r => r.Product.Id == id).ToListAsync();

            serviceResponse.Data = reviews.Select(r => _mapper.Map<UpdateReviewDto>(r)).ToList();

            return serviceResponse;
        }

        
        public async Task<ServiceResponse<ReviewDto>> AddReview(ReviewDto newReview)
        {
            ServiceResponse<ReviewDto> serviceResponse = new ServiceResponse<ReviewDto>();
            try
            {
                _context.Reviews.Add(_mapper.Map<Review>(newReview));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "unsuccesful";
            }
            serviceResponse.Success = true;
            serviceResponse.Message = "Product added succesfully";
            serviceResponse.Data = _mapper.Map<ReviewDto>(newReview) ;

            return serviceResponse;
        }

        public async Task<bool> DeleteReview(int id)
        {
            try
            {
                Review review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
                if (review != null)
                {
                    _context.Reviews.Remove(review);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }        
    }
}