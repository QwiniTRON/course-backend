using System;
using Domain.Abstractions.Mediatr;
using Domain.Maps.Views;

namespace Domain.UseCases.Progress.ProgressesById
{
    public class ProgressesByIdOutput: IUseCaseOutput
    {
        public int Id { get; set; }
        
        public UserView User { get; set; }
        public LessonView Lesson { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public ProgressesByIdOutput() {}
    }
}