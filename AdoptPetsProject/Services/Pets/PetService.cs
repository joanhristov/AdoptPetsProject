namespace AdoptPetsProject.Services.Pets
{
    using System.Collections.Generic;
    using System.Linq;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Models;

    public class PetService : IPetService
    {
        private readonly AdoptPetsDbContext data;

        public PetService(AdoptPetsDbContext data) 
            => this.data = data;

        public PetQueryServiceModel All(
            string breed, 
            string searchTerm,
            PetSorting sorting,
            int currentPage,
            int petsPerPage)
        {
            var petsQuery = this.data.Pets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(breed))
            {
                petsQuery = petsQuery.Where(p => p.Breed == breed);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                petsQuery = petsQuery.Where(p =>
                (p.Breed + " " + p.Name).ToLower().Contains(searchTerm.ToLower()) ||
                p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            petsQuery = sorting switch
            {
                PetSorting.Age => petsQuery.OrderByDescending(p => p.Age),
                PetSorting.BreedAndKind => petsQuery.OrderBy(p => p.Breed).ThenBy(p => p.Kind),
                _ => petsQuery.OrderByDescending(p => p.Id)
            };

            var totalPets = petsQuery.Count();

            var pets = petsQuery
                .Skip((currentPage - 1) * petsPerPage)
                .Take(petsPerPage)
                .Select(p => new PetServiceModel
                {
                    Id = p.Id,
                    Breed = p.Breed,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age,
                    Kind = p.Kind.Name
                })
                .ToList();

            return new PetQueryServiceModel
            {
                TotalPets = totalPets,
                CurrentPage = currentPage,
                PetsPerPage = petsPerPage,
                Pets = pets
            };
        }

        public IEnumerable<string> AllPetBreeds()
            => this.data
                .Pets
                .Select(p => p.Breed)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
    }
}
