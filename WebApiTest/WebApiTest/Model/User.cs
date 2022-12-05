using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest.Model
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
