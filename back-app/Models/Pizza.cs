namespace ContosoPizza.Models;


public class Pizza
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public bool IsGlutenFree { get; set; }
}