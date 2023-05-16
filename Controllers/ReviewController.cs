using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.services.ReviewService;
using OnlineAuction.Dtos.Review;
using OnlineAuction.Models;

namespace OnlineAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _reviewService.GetAllReviewsByProductId(id));
        }        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _reviewService.GetReviewByReviewId(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(Review newReview)
        {
            return Ok(await _reviewService.AddReview(newReview));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReview(Review updatedReview)
        {
            return Ok(await _reviewService.UpdateReview(updatedReview));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            return Ok(await _reviewService.DeleteReview(id));
        }
    }
}