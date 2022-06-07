namespace AdoptPetsProject.Services.Pets.Models
{
    using System;

    public class PetServiceModel
    {
        public int Id { get; init; }

        public string Breed { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public DateTime BirthDate { get; init; } // If something goes bad move this to PetDetailsServiceModel

        public string Gender { get; init; }

        public int Age { get; init; }

        public string KindName { get; init; }
    }
}
