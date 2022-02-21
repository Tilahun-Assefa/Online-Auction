using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers.services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ReviewService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetReviewDto>>> AddReview(AddReviewDto newReview)
        {
            ServiceResponse<List<GetReviewDto>> serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            Review review = _mapper.Map<Review>(newReview);
            review.Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == newReview.ProductId);
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.Reviews.Where(r => r.Product.Id == newReview.ProductId)
                .Select(c => _mapper.Map<GetReviewDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> DeleteReview(int id)
        {
            ServiceResponse<List<GetReviewDto>> serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            try
            {
                Review review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
                if (review != null)
                {
                    _context.Reviews.Remove(review);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Reviews.Where(r => r.Id == id)
                    .Select(c => _mapper.Map<GetReviewDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Review not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetAllReviews(int id)
        {
            ServiceResponse<List<GetReviewDto>> serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            List<Review> reviews = await _context.Reviews
                .Include(r => r.Product)
                .Where(r => r.Product.Id == id).ToListAsync();
            serviceResponse.Data = (reviews.Select(r => _mapper.Map<GetReviewDto>(r))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDto>> GetReviewByReviewId(int id)
        {
           ServiceResponse<GetReviewDto> serviceResponse = new ServiceResponse<GetReviewDto>();
            try
            {
                Review review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
                if (review != null)
                {
                   
                    serviceResponse.Data =  _mapper.Map<GetReviewDto>(review);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Review not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetReviewDto>> UpdateReview(UpdateReviewDto updatedReview)
        {
            ServiceResponse<GetReviewDto> serviceResponse = new ServiceResponse<GetReviewDto>();
            try
            {
                Review review = await _context.Reviews.Include(r => r.Product).FirstOrDefaultAsync(r => r.Id == updatedReview.Id);
                if (review.Product.Id == updatedReview.ProductId)
                {
                    review.TimeStamp = updatedReview.TimeStamp;
                    review.User = updatedReview.User;
                    review.Rating = updatedReview.Rating;
                    review.Comment = updatedReview.Comment;
                    _context.Reviews.Update(review);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetReviewDto>(review);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Review not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}