using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class CreateCustomerResDto
    {
        [Required]
        public string CustomerFullName { get; set; } = null!;

        [Phone]
        public string? Telephone { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; } = null!;

        public DateOnly? CustomerBirthday { get; set; }
        
        [Required]
        public string Password { get; set; } = null!;
    }
}
