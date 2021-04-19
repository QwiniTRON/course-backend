using Microsoft.AspNetCore.Identity;

namespace Domain.Entity
{
    public class UserRoleEntity: IdentityUserRole<int>
    {
        public IdentityRole<int> Role { get; set; }
    }
}