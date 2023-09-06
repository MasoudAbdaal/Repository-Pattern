namespace Domain.Entities;
public record IEntity
{
    public byte[]? ID { get; init; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    //TODO: How to detect whos edit our record?
    //ModifiedBy ?
    //CreatedBy?
}
