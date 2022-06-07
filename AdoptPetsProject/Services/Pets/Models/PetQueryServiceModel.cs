namespace AdoptPetsProject.Services.Pets.Models
{
    using System.Collections.Generic;

    public class PetQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int PetsPerPage { get; init; }

        public int TotalPets { get; init; }

        public IEnumerable<PetServiceModel> Pets { get; init; }
    }
}
