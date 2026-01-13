namespace SimulationBilet16MPA201.ViewModels.TrainerViewModels;

public class TrainerUpdateVM
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public IFormFile? Image { get; set; }
}
