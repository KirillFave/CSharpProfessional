namespace Domain;

/// <summary>
/// Заявление
/// </summary>
public class Statement
{
    public Guid Id { get; set; }
    public bool IsConfirmed { get; set; }
    public User User { get; set; }
    public required Guid UserId { get; set; }
    public List<Vacation> Vacations { get; set; }
}