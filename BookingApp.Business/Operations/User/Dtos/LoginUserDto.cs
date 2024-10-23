using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User.Dtos
{
    public class LoginUserDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}
