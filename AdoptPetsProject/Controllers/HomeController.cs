namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Models.Home;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Services.Statistics;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly AdoptPetsDbContext data;

        public HomeController(
            IStatisticsService statistics,
            AdoptPetsDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var totalPets = this.data.Pets.Count();
            var totalUsers = this.data.Users.Count();

            var pets = this.data
                .Pets
                .OrderByDescending(p => p.Id)
                .Select(p => new PetIndexViewModel
                {
                    Id = p.Id,
                    Breed = p.Breed,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age
                })
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
