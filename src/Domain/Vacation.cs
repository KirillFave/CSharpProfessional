namespace Domain;

/// <summary>
/// Отпуск
/// </summary>
public class Vacation
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Statement Statement { get; set; }
    public required Guid StatementId { get; set; }
}
