﻿namespace Domain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DismissalDate { get; set; }
    public Subdivision Subdivision { get; set; }
    public Statement Statement { get; set; }
}
