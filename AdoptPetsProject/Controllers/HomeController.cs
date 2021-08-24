namespace AdoptPetsProject.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Models.Home;
    using AdoptPetsProject.Models;
    using AdoptPetsProject.Data;
    

    public class HomeController : Controller
    {
        private readonly AdoptPetsDbContext data;

        public HomeController(AdoptPetsDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var totalPets = this.data.Pets.Count();

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

            return View(new IndexViewModel
            {
                TotalPets = totalPets,
                Pets = pets
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
