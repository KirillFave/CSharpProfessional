namespace Domain;

public class Statement
{
    public int Id { get; set; }
    public bool IsConfirmed { get; set; }
    public required User User { get; set; }
    public required List<Vacation> Vacations { get; set; }
}