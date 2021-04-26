using System;

namespace Domain.Maps.Views.Comment
{
    public class CommentView
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        
        public UserView Author { get; set; }    
        
        public CommentView() {}
    }
}