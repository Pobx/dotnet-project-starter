using System;
namespace Domain
{
    public class Example : BaseEntity
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public int? Age { get; set; }
        public string Image { get; set; } = String.Empty;
    }
}

