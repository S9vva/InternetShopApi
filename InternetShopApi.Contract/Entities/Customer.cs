
using InternetShopApi.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InternetShopApi.Domain.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; }

        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string SurName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }


    }
}
