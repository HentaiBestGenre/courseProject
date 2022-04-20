using Microsoft.AspNetCore.Identity;
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

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }

    public class RoleModeficationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}
