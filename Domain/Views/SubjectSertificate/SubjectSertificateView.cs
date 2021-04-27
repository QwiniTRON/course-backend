using System;
using Domain.Maps.Views;
using Domain.Views.Subject;

namespace Domain.Views.SubjectSertificate
{
    public class SubjectSertificateView
    {
        public int Id { get; set; }

        public UserView Owner { get; set; }
        
        public SubjectView Subject { get; set; }
        
        public DateTime CreatedTime { get; set; }
    }
}