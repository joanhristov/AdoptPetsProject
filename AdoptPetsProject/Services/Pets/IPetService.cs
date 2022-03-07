namespace AdoptPetsProject.Services.Pets
{
    using System.Collections.Generic;
    using AdoptPetsProject.Models;

    public interface IPetService
    {
        PetQueryServiceModel All(
            string breed,
            string searchTerm,
            PetSorting sorting,
            int currentPage,
            int petsPerPage);

        IEnumerable<string> AllPetBreeds();
    }
}
