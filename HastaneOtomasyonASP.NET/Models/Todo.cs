namespace HastaneOtomasyonASP.NET.Models
{

    public class TodoList
    {
        public List<TodoItem> todos { get; set; }
        public int total { get; set; }
        public int skip { get; set; }
        public int limit { get; set; }
    }

    public class TodoItem
    {
        public int id { get; set; }
        public string todo { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
    }

}
