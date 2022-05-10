using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class CreateUserModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string RepitePassword { get; set; }
        [Required] public string Email { get; set; }
    }

    public class LoginModel
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
