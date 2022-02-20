namespace AdoptPetsProject.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Models.Pets;
    using AdoptPetsProject.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;
    using AdoptPetsProject.Services.Pets;

    public class PetsController : Controller
    {
        private readonly IPetService pets;
        private readonly AdoptPetsDbContext data;

        public PetsController(IPetService pets, AdoptPetsDbContext data)
        {
            this.pets = pets;
            this.data = data;
        }

        public IActionResult Add() => View(new AddPetFormModel
        {
            Kinds = this.GetPetKinds()
        });



        public IActionResult All([FromQuery] AllPetsQueryModel query)
        {
            var queryResult = this.pets.All(
                query.Breed,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPetsQueryModel.PetsPerPage);

            var petBreeds = this.pets.AllPetBreeds();

            query.Breeds = petBreeds;
            query.TotalPets = queryResult.TotalPets;
            query.Pets = queryResult.Pets;

            return View(query);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPetFormModel pet, IFormFile image)
        {


            //if (image == null || image.Length > 2 * 1024 * 1024)
            //{
            //    this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB.");
            //}

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

            return RedirectToAction(nameof(All));
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
