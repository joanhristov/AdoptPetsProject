namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Models.Pets;
    using AdoptPetsProject.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class PetsController : Controller
    {
        private readonly AdoptPetsDbContext data;

        public PetsController(AdoptPetsDbContext data) 
            => this.data = data;

        public IActionResult Add() => View(new AddPetFormModel
        { 
            Kinds = this.GetPetKinds()
        });

        [HttpPost]
        public IActionResult Add(AddPetFormModel pet, IFormFile image)
        {


            if (image == null || image.Length > 2 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB.");
            }

            if (!this.data.Kinds.Any(k => k.Id == pet.KindId))
            {
                this.ModelState.AddModelError(nameof(pet.KindId), "Kind does not exist.");
            }

            if (!ModelState.IsValid)
            {
                pet.Kinds = this.GetPetKinds();

                return View(pet);
            }

            var petData = new Pet
            {
                KindId = pet.KindId,
                Breed = pet.Breed,
                Name = pet.Name,
                Gender = pet.Gender,
                Age = pet.Age,
                BirthDate = pet.BirthDate,
                ImageUrl = pet.ImageUrl,
                Description = pet.Description
            };

            this.data.Pets.Add(petData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<PetKindViewModel> GetPetKinds()
            => this.data
                    .Kinds
                    .Select(k => new PetKindViewModel
                    {
                        Id = k.Id,
                        Name = k.Name
                    })
                    .ToList();
    }
}
