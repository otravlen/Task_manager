using Task_manager.Enum;

namespace Task_manager.Domain
{
    public class TaskFilter : PagingFilter
    {
        public string Name { get; set; }    
        public Priority? Priority { get; set; }
    }
}
