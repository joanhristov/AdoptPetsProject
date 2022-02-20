using System.Collections.Generic;

namespace AdoptPetsProject.Models.Home
{
    public class IndexViewModel
    {
        public int TotalPets { get; init; }
        public int TotalUsers { get; init; }
        public int TotalAdoptions { get; init; }

        public List<PetIndexViewModel> Pets { get; init; }
    }
}
