namespace AdoptPetsProject.Controllers
{
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Infrastructure.Extensions;
    using AdoptPetsProject.Models.Donators;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

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

            return RedirectToAction("All", "Pets");
        }
    }
}
