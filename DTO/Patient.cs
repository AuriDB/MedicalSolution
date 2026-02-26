using System;

namespace DTO
{
    public class Patient : BaseClass
    {
        public string SocialSecurityId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
    }
}