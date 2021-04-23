using System;
using Domain.Abstractions.Mediatr;

namespace Domain.UseCases.PracticeOrder.GetOne
{
    public class GetOnePracticeOutput: IUseCaseOutput
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RejectReason { get; set; }
        public bool IsDone { get; set; }
        public bool IsResolved { get; set; }

        public TYPE Type { get; set; }

        public GetOnePracticeOutput() {}
    }
}