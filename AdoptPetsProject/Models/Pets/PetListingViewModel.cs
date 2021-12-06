namespace AdoptPetsProject.Models.Pets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PetListingViewModel
    {
        public int Id { get; init; }

        public string Breed { get; init; }

        public string Name { get; init; }

        public string ImageUrl { get; init; }

        public int Age { get; init; }

        public string Kind { get; init; }
    }
}
