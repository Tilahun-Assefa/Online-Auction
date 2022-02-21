using System;

namespace OnlineAuction.Models
{
    public class Review
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public DateTime TimeStamp { get; set; }
        public string User { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}