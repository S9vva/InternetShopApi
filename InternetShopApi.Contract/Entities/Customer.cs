
using System.ComponentModel.DataAnnotations;


namespace InternetShopApi.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string SurName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
