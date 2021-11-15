using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltCode.API.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiresIn { get; set; }
        public UserInfoDto UserInfo { get; set; }
    }
}
