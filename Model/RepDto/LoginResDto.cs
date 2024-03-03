using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class LoginResDto
    {
        [EmailAddress]
        public string Email { get; set; } = null!;

        [MinLength(3)]
        public string Password { get; set; } = null!;
    }
}
