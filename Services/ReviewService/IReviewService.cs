using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Models;

namespace OnlineAuction.services.ReviewService
{
    public interface IReviewService
    {
        Task<ServiceResponse<List<GetReviewDto>>> GetAllReviews(int id);
        Task<ServiceResponse<GetReviewDto>> GetReviewByReviewId(int id);
        Task<ServiceResponse<GetReviewDto>> AddReview(Review newReview);
        Task<bool> UpdateReview(Review updatedReview);
        Task<bool> DeleteReview(int id);


    }
}