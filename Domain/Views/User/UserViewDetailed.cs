using System.Collections.Generic;
using Domain.Enums;
using Domain.Views.SubjectSertificate;

namespace Domain.Maps.Views
{
    public class UserViewDetailed
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public List<UserRoles> Roles { get; set; }
        public string Mail { get; set; }
        public bool IsBanned { get; set; }
        
        public List<SubjectSertificateView> SubjectSertificates { get; set; }
        
        public UserViewDetailed() {}
    }
}