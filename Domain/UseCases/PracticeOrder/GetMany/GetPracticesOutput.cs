using System;
using Domain.Abstractions.Mediatr;

namespace Domain.UseCases.PracticeOrder.GetMany
{
    public class GetPracticesOutput: IUseCaseOutput
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string LessonName { get; set; }
        public string UserNick { get; set; }

        public GetPracticesOutput() {}
    }
}