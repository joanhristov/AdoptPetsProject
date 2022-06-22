namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Models.Donators;
    using AdoptPetsProject.Infrastructure.Extensions;
    
    using static WebConstants;

    public class DonatorsController : Controller
    {
        private readonly AdoptPetsDbContext data;

        public DonatorsController(AdoptPetsDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDonatorFormModel donator)
        {
            var userId = this.User.Id();

            var userIsAlreadyADonator = this.data
                .Donators
                .Any(d => d.UserId == userId);

            if (userIsAlreadyADonator)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(donator);
            }

            var donatorData = new Donator
            {
                Name = donator.Name,
                PhoneNumber = donator.PhoneNumber,
                UserId = userId
            };

            this.data.Donators.Add(donatorData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becoming a donator!";

            return RedirectToAction(nameof(PetsController.All), "Pets");
        }
    }
}
