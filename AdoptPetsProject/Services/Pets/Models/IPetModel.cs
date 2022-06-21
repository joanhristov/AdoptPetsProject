namespace AdoptPetsProject.Services.Pets.Models
{
    public interface IPetModel
    {
        string Breed { get; }

        string Name { get; }

        int Age { get; }
    }
}
