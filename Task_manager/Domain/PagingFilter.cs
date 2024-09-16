using Azure.Core;

namespace Task_manager.Domain
{
    public class PagingFilter
    {
        
        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
}
