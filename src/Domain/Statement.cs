namespace Domain;

/// <summary>
/// Заявление
/// </summary>
public class Statement
{
    public Guid Id { get; set; }
    public bool IsConfirmed { get; set; }
    public required User User { get; set; }
    public required List<Vacation> Vacations { get; set; }
}