namespace AdoptPetsProject.Services.Pets.Models
{
    public class PetDetailsServiceModel : PetServiceModel
    {
        public string Description { get; init; }

        public int KindId { get; init; }

        public int DonatorId { get; init; }

        public string DonatorName { get; init; }

        public string UserId { get; set; }
    }
}
