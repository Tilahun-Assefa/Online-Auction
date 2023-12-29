using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.services.ReviewService;
using OnlineAuction.Dtos.ReviewDtos;

namespace OnlineAuction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _reviewService.GetAllReviewsByProductId(id));
        }          

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewDto newReview)
        {
            return Ok(await _reviewService.AddReview(newReview));
        }        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            return Ok(await _reviewService.DeleteReview(id));
        }
    }
}