namespace Domain.Maps.EntitiesViews
{
    public class LessonView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPractice { get; set; }
        
        public LessonView() {}
    }
}