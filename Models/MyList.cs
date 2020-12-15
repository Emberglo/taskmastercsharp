namespace taskmastercsharp.Models
{
    public class MyList
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }
}