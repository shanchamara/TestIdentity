using System.ComponentModel.DataAnnotations;

namespace TestIdentity.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public object RememberMe { get; internal set; }

        public IEnumerable<string> ExternalLogins { get; set; }

        public string ReturnUrl1 { get; set; }
    }

    public class EmailComformation
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
