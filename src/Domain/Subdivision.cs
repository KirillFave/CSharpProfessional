namespace Domain;

public class Subdivision
{
    public int Id { get; set; }
    public required User Manager { get; set; }
    public required List<User> Employees { get; set; }
}
