using System;
using Domain.Abstractions.Mediatr;
using Domain.Entity;
using Domain.Maps.EntitiesViews;

namespace Domain.UseCases.PracticeOrder.OneInfo
{
    public class GetPracticeInfoOutput: IUseCaseOutput
    {
        public int Id { get; set; }

        public UserView Author { get; set; }

        
        public string RejectReason { get; set; }
        public bool IsDone { get; set; }
        public bool IsResolved { get; set; }

        
        public LessonView Lesson { get; set; }
        
        public UserView Teacher { get; set; }

        public string CodePath { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}