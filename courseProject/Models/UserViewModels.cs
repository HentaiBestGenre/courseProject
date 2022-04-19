using System.ComponentModel.DataAnnotations;

namespace courseProject.Models
{
    public class UserViewModels{}

    public class CreateUserModel
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Email { get; set; }
    }

    public class LoginModel
    {
        [Required][UIHint("email")] public string Email { get; set; }
        [Required][UIHint("password")] public string Password { get; set; }
    }
}
