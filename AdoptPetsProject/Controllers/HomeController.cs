namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Models.Home;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Services.Statistics;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IConfigurationProvider mapper;
        private readonly AdoptPetsDbContext data;

        public HomeController(
            IStatisticsService statistics,
            AdoptPetsDbContext data, 
            IMapper mapper)
        {
            this.statistics = statistics;
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public IActionResult Index()
        {
            var totalPets = this.data.Pets.Count();
            var totalUsers = this.data.Users.Count();

            var pets = this.data
                .Pets
                .OrderByDescending(p => p.Id)
                .ProjectTo<PetIndexViewModel>(this.mapper)
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalPets = totalStatistics.TotalPets,
                TotalUsers = totalStatistics.TotalUsers,
                Pets = pets
            });
        }

        public IActionResult Error() => View();
    }
}
