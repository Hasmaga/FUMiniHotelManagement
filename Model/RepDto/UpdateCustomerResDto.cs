using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class UpdateCustomerResDto
    {
        [Required]
        public int CustomerId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? Telephone { get; set; }
        public DateOnly? CustomerBirthday { get; set; }
        public string? Password { get; set; }
    }
}
