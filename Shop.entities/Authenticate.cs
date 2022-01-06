using System.ComponentModel.DataAnnotations;

namespace Shop.entities
{
    public class Authenticate
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
