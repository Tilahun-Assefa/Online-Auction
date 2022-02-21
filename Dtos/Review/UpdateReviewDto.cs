using System;

namespace OnlineAuction.Dtos.Review
{
    public class UpdateReviewDto
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string User { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
    }
}