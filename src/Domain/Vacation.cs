namespace Domain;

public class Vacation
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required Statement Statement { get; set; }
}
