namespace SimulationBilet16MPA201.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Trainer> Trainers { get; set; }
}
