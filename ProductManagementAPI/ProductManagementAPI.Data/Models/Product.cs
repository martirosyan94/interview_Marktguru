namespace ProductManagementAPI.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public decimal? Price { get; set; }

        public bool? Available { get; set; }

        public string? Description { get; set; }

        public DateTime DateCreated { get; set; }

        public byte[] RowVersion { get; set; } = default!;
    }
}
