using DemoNet.Api.Models.Common;

namespace DemoNet.Api.Models.Entities
{
    public class User : EntityBase
    {
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Department { get; set; }
        public string? Role { get; set; }
        
    }
}
