namespace AdoptPetsProject.Models.Api.Pets
{
    public class AllPetsApiRequestModel
    {
        public string Breed { get; init; }

        public string SearchTerm { get; init; }

        public PetSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int PetsPerPage { get; init; } = 10;
    }
}
