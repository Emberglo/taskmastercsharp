namespace taskmastercsharp.Models
{
    public class MyTask
    {
        public string Body { get; set; }
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }

    public class ListTaskViewModel : MyTask
    {
        public int ListTaskId { get; set; }
    }
}