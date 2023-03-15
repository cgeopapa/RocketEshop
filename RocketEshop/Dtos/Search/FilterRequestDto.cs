namespace RocketEshop.Dtos.Search
{
    public class FilterRequestDto
    {
        public bool Availability { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string Name { get; set; }
    }
}
