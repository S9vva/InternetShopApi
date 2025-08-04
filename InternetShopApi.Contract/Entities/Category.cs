using System.ComponentModel.DataAnnotations;

namespace InternetShopApi.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }
    }
}
