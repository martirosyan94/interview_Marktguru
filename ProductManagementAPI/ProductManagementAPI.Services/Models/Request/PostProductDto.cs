using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Services.Models.Request
{
    public class PostProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        public decimal? Price { get; set; }
        public bool? Available { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
