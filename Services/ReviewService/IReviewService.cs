using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.ReviewDtos;
using OnlineAuction.Models;

namespace OnlineAuction.services.ReviewService
{
    public interface IReviewService
    {
        Task<ServiceResponse<List<UpdateReviewDto>>> GetAllReviewsByProductId(int id);
        Task<ServiceResponse<ReviewDto>> AddReview(ReviewDto newReview);
        Task<bool> DeleteReview(int id);
    }
}