using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserLogin
    {
        [Required, MaxLength(255)]
        public string LoginProp { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
