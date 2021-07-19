using System.Collections.Generic;
using ZkTest.Enum;

namespace ZkTest.Dtos
{
    public class AuthConfigDto
    {
        public List<AuthConfigType> AuthConfigIds { get; set; }
        
        public byte MatchTimeout { get; set; }
        public byte AuthTimeout { get; set; }
    }
}