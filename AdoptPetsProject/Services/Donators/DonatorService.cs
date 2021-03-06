namespace AdoptPetsProject.Services.Donators
{
    using System.Linq;
    using AdoptPetsProject.Data;

    public class DonatorService : IDonatorService
    {
        private readonly AdoptPetsDbContext data;

        public DonatorService(AdoptPetsDbContext data)
            => this.data = data;

        public bool IsDonator(string userId)
            => this.data
                .Donators
                .Any(d => d.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Donators
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
