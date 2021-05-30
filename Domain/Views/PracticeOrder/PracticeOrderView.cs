using System;
using Domain.Entity;
using Domain.Views.AppFile;

namespace Domain.Views.PracticeOrder
{
    public class PracticeOrderView
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        
        public string RejectReason { get; set; }
        /* completion status */
        public bool IsDone { get; set; }
        public bool IsResolved { get; set; }

        
        public int LessonId { get; set; }

        
        public int? TeacherId { get; set; }

        public AppFileView PracticeContent { get; set; }
        public int PracticeContentId { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}