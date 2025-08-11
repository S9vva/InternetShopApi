

using System.ComponentModel.DataAnnotations;

namespace InternetShopApi.Domain.Entities.Auth
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "User name or Password is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User name or Password is required")]
        public string Password { get; set; }
    }
}
