namespace ProductManagementAPI.Services.Models.Response
{
    public class GetProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public bool? Available { get; set; }
        public decimal? Price { get; set; }

        public string? Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
