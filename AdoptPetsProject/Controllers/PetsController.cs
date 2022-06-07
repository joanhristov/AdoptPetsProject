﻿namespace AdoptPetsProject.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Models.Pets;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authorization;
    using AdoptPetsProject.Services.Donators;
    using AdoptPetsProject.Services.Pets;
    using AdoptPetsProject.Infrastructure;
    using AutoMapper;

    public class PetsController : Controller
    {
        private readonly IPetService pets;
        private readonly IDonatorService donators;
        private readonly IMapper mapper;

        public PetsController(IPetService pets,
            IDonatorService donators, 
            IMapper mapper)
        {
            this.pets = pets;
            this.donators = donators;
            this.mapper = mapper;
        }

        public IActionResult All([FromQuery] AllPetsQueryModel query)
        {
            var queryResult = this.pets.All(
                query.Breed,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllPetsQueryModel.PetsPerPage);

            var petBreeds = this.pets.AllBreeds();

            query.Breeds = petBreeds;
            query.TotalPets = queryResult.TotalPets;
            query.Pets = queryResult.Pets;

            return View(query);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myPets = this.pets.ByUser(this.User.Id());

            return View(myPets);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.donators.IsDonator(this.User.Id()))
            {
                return RedirectToAction(nameof(DonatorsController.Become), "Donators");
            }

            return View(new PetFormModel
            {
                Kinds = this.pets.AllKinds()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(PetFormModel pet, IFormFile image)
        {


            //if (image == null || image.Length > 2 * 1024 * 1024)
            //{
            //    this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB.");
            //}

            var donatorId = this.donators.IdByUser(this.User.Id());

            if (donatorId == 0)
            {
                return RedirectToAction(nameof(DonatorsController.Become), "Donators");
            }

            if (!this.pets.KindExists(pet.KindId))
            {
                this.ModelState.AddModelError(nameof(pet.KindId), "Kind does not exist.");
            }

            if (!ModelState.IsValid)
            {
                pet.Kinds = this.pets.AllKinds();

                return View(pet);
            }

            this.pets.Create(
                pet.Breed,
                pet.Name,
                pet.Gender,
                pet.Age,
                pet.BirthDate,
                pet.Description,
                pet.ImageUrl,
                pet.KindId,
                donatorId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.donators.IsDonator(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DonatorsController.Become), "Donators");
            }

            var pet = this.pets.Details(id);

            if (pet.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var petForm = this.mapper.Map<PetFormModel>(pet);

            petForm.Kinds = this.pets.AllKinds();

            return View(petForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, PetFormModel pet)
        {
            var donatorId = this.donators.IdByUser(this.User.Id());

            if (donatorId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DonatorsController.Become), "Donators");
            }

            if (!this.pets.KindExists(pet.KindId))
            {
                this.ModelState.AddModelError(nameof(pet.KindId), "Kind does not exist.");
            }

            if (!ModelState.IsValid)
            {
                pet.Kinds = this.pets.AllKinds();

                return View(pet);
            }

            if (!this.pets.IsByDonator(id, donatorId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.pets.Edit(
                id,
                pet.Breed,
                pet.Name,
                pet.Gender,
                pet.Age,
                pet.BirthDate,
                pet.Description,
                pet.ImageUrl,
                pet.KindId);

            return RedirectToAction(nameof(All));
        }
    }
}
