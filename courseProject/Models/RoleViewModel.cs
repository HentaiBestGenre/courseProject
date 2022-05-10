using Microsoft.AspNetCore.Identity;

namespace courseProject.Models
{
    public class RoleEditModel
    {
        public string ActionName { get; set; }
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
    }

    public class RoleModeficationModel
    {
        public string ActionName { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] Ids { get; set; }
    }
}
