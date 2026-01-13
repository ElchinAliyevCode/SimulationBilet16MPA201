namespace SimulationBilet16MPA201.Models;

public class Trainer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }
    public string ImagePath { get; set; }
}
