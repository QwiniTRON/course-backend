namespace Domain.Maps.Views
{
    public class LessonView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPractice { get; set; }
        public int Index { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        
        public LessonView() {}
    }
}