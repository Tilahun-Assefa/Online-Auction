using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers.services.ReviewService
{
    public interface IReviewService
    {
        Task<ServiceResponse<List<GetReviewDto>>> GetAllReviews(int id);
         Task<ServiceResponse<GetReviewDto>> GetReviewByReviewId(int id);
         Task<ServiceResponse<List<GetReviewDto>>> AddReview(AddReviewDto newReview);
         Task<ServiceResponse<GetReviewDto>> UpdateReview(UpdateReviewDto updatedReview);
         Task<ServiceResponse<List<GetReviewDto>>> DeleteReview(int id);        
    
         
    }
}