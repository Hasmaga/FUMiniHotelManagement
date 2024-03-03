namespace Model.ResDto
{
    public class GetCustomerResDto
    {
        public int CustomerId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? Telephone { get; set; }
        public string EmailAddress { get; set; } = null!;
        public DateOnly? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }        
    }
}
