using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineAuction.Data;
using OnlineAuction.Dtos.Product;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Models;

namespace OnlineAuction.services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext _context;
        public ReviewService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<GetReviewDto>>> GetAllReviews(int id)
        {
            ServiceResponse<List<GetReviewDto>> serviceResponse = new ServiceResponse<List<GetReviewDto>>();
            List<Review> reviews = await _context.Reviews.Include(r => r.Product)
                .Where(r => r.Product.Id == id).ToListAsync();

            serviceResponse.Data = reviews.Select(r => new GetReviewDto()
            {
                TimeStamp = r.TimeStamp,
                User = r.User,
                Comment = r.Comment,
                Rating = r.Rating
            }).ToList();

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
                    serviceResponse.Data = new GetReviewDto()
                    {
                        TimeStamp = review.TimeStamp,
                        User = review.User,
                        Comment = review.Comment,
                        Rating = review.Rating
                    };
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
        public async Task<ServiceResponse<GetReviewDto>> AddReview(Review newReview)
        {
            ServiceResponse<GetReviewDto> serviceResponse = new ServiceResponse<GetReviewDto>();
            try
            {
                _context.Reviews.Add(newReview);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "unsuccesful";
            }

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

        public async Task<bool> UpdateReview(Review updatedReview)
        {
            _context.Entry(updatedReview).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}