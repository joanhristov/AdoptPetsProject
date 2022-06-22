namespace AdoptPetsProject.Models.Pets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AdoptPetsProject.Services.Pets.Models;

    public class AllPetsQueryModel
    {
        public const int PetsPerPage = 3;

        public string Breed { get; init; }

        [Display(Name = "Find your new family member here:")]
        public string SearchTerm { get; init; }

        public PetSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPets { get; set; }

        public IEnumerable<string> Breeds { get; set; }

        public IEnumerable<PetServiceModel> Pets { get; set; }
    }
}
