namespace AdoptPetsProject.Areas.Admin.Controllers
{
    using AdoptPetsProject.Services.Pets;
    using Microsoft.AspNetCore.Mvc;

    public class PetsController : AdminController
    {
        private readonly IPetService pets;

        public PetsController(IPetService pets) 
            => this.pets = pets;

        public IActionResult All()
        {
            var pets = this.pets
                .All(publicOnly: false)
                .Pets;

            return View(pets);
        }

        public IActionResult ChangeVisibility(int id)
        {
            this.pets.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
