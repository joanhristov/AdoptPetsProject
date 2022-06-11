namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Models.Home;
    using AdoptPetsProject.Services.Statistics;
    using AdoptPetsProject.Services.Pets;


    public class HomeController : Controller
    {
        private readonly IPetService pets;
        private readonly IStatisticsService statistics;

        public HomeController(
            IPetService pets,
            IStatisticsService statistics)
        {
            this.pets = pets;
            this.statistics = statistics;
        }

        public IActionResult Index()
        {
            var latestPets = this.pets
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalPets = totalStatistics.TotalPets,
                TotalUsers = totalStatistics.TotalUsers,
                Pets = latestPets
            });
        }

        public IActionResult Error() => View();
    }
}
