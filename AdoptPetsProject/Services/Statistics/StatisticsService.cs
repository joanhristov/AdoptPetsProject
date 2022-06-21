namespace AdoptPetsProject.Services.Statistics
{
    using System.Linq;
    using AdoptPetsProject.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly AdoptPetsDbContext data;

        public StatisticsService(AdoptPetsDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalPets = this.data.Pets.Count(p => p.IsPublic);
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalPets = totalPets,
                TotalUsers = totalUsers
            };
        }
    }
}
