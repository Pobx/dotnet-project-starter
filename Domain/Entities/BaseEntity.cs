namespace Domain;
public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public string CreatedBy { get; set; } = String.Empty;
    public DateTime UpdatedTimestamp { get; set; }
    public string UpdatedBy { get; set; } = String.Empty;
}

