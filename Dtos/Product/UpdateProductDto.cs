namespace OnlineAuction.Dtos.Product
{
    public class UpdateProductDto
    {
        public int Id { set; get; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
    }
}