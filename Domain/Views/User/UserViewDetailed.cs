using System.Collections.Generic;
using Domain.Entity;
using Domain.Enums;
using Domain.Views.PracticeOrder;
using Domain.Views.SubjectSertificate;
using Domain.Views.UserProgress;

namespace Domain.Maps.Views
{
    public class UserViewDetailed
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public List<UserRoles> Roles { get; set; }
        public string Mail { get; set; }
        public bool IsBanned { get; set; }
        public string Photo { get; set; }
        
        public List<SubjectSertificateView> SubjectSertificates { get; set; }
        public List<UserProgressView> UserProgresses { get; set; }
        public List<PracticeOrderView> PracticeOrders { get; set; }
        public UserViewDetailed() {}
    }
}