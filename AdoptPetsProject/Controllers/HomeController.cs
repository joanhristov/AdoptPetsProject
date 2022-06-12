namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Services.Pets;
    using Microsoft.Extensions.Caching.Memory;
    using System.Collections.Generic;
    using AdoptPetsProject.Services.Pets.Models;
    using System;

    public class HomeController : Controller
    {
        private readonly IPetService pets;
        private readonly IMemoryCache cache;

        public HomeController(
            IPetService pets,
            IMemoryCache cache)
        {
            this.pets = pets;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestPetsCacheKey = "LatestPetsCacheKey";

            var latestPets = this.cache.Get<List<LatestPetsServiceModel>>(latestPetsCacheKey);

            if (latestPets == null)
            {
                latestPets = this.pets
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestPetsCacheKey, latestPets, cacheOptions);
            }

            return View(latestPets);
        }

        public IActionResult Error() => View();
    }
}
