using System.Globalization;

namespace LoginAPI.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Jwt { get; set; }
    }
}
