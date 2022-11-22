namespace Domain.Entities;
public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedTimestamp { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; } = String.Empty;
    public DateTime UpdatedTimestamp { get; set; } = DateTime.Now;
    public string UpdatedBy { get; set; } = String.Empty;
}

