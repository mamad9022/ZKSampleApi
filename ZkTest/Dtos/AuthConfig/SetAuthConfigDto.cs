using System.Collections.Generic;
using ZkTest.Enum;

namespace ZkTest.Dtos
{
    public class SetAuthConfigDto
    {
        public List<AuthConfigType> AuthConfigIds { get; set; }
        public int MatchTimeout { get; set; }
        public int AuthTimeout { get; set; }
    }
}