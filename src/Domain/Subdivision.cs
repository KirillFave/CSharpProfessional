namespace Domain;

/// <summary>
/// Подразделение (дирекция, отдел, группа и т.п.)
/// </summary>
public class Subdivision
{
    public Guid Id { get; set; }
    public required User Manager { get; set; }
    public required List<User> Employees { get; set; }
}
