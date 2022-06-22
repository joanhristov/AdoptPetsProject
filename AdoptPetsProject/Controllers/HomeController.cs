namespace AdoptPetsProject.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using AdoptPetsProject.Services.Pets;
    using AdoptPetsProject.Services.Pets.Models;
    
    using static WebConstants.Cache;

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

            var latestPets = this.cache.Get<List<LatestPetsServiceModel>>(LatestPetsCacheKey);

            if (latestPets == null)
            {
                latestPets = this.pets
                    .Latest()
                    .ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestPetsCacheKey, latestPets, cacheOptions);
            }

            return View(latestPets);
        }

        public IActionResult Error() => View();
    }
}
