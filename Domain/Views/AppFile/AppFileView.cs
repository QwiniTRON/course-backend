namespace Domain.Views.AppFile
{
    public class AppFileView
    {
        public int Id { get; set; }
        
        /* file name */
        public string Name { get; set; }
        /* full path relative main catalog */
        public string Path { get; set; }

        public int? UserId { get; set; }
    }
}