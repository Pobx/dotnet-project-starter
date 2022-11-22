using System;
namespace Domain.DTO.Response
{
    public class ResponseEntity<T>
    {
        public T Entity { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }
}

