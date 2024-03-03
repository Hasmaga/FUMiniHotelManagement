using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.RepDto
{
    public class UpdateProfileByCustomerResDto
    {
        public string? CusomerFullName { get; set; }
        public string? Telephone { get; set; }        
        public DateOnly? CustomerBirthday { get; set; }
        public string? Password { get; set; }
    }
}
