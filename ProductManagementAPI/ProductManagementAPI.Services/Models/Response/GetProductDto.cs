namespace ProductManagementAPI.Services.Models.Response
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public bool? Available { get; set; }
        public decimal? Price { get; set; }
    }
}
