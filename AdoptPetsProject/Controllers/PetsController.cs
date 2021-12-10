namespace AdoptPetsProject.Controllers
{
    using System.Linq;
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



        public IActionResult All([FromQuery]AllPetsQueryModel query)
        {
            var petsQuery = this.data.Pets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Breed))
            {
                petsQuery = petsQuery.Where(p => p.Breed == query.Breed);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                petsQuery = petsQuery.Where(p =>
                (p.Breed + " " + p.Name).ToLower().Contains(query.SearchTerm.ToLower()) ||
                p.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            petsQuery = query.Sorting switch
            {
                PetSorting.Age => petsQuery.OrderByDescending(p => p.Age),
                PetSorting.BreedAndKind => petsQuery.OrderBy(p => p.Breed).ThenBy(p => p.Kind),
                _ => petsQuery.OrderByDescending(p => p.Id)
            };

            var totalPets = petsQuery.Count();

            var pets = petsQuery
                .Skip((query.CurrentPage - 1) * AllPetsQueryModel.PetsPerPage)
                .Take(AllPetsQueryModel.PetsPerPage)
                .Select(p => new PetListingViewModel
                {
                    Id = p.Id,
                    Breed = p.Breed,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age,
                    Kind = p.Kind.Name
                })
                .ToList();

            var petBreeds = this.data
                .Pets
                .Select(p => p.Breed)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

            query.TotalPets = totalPets;
            query.Breeds = petBreeds;
            query.Pets = pets;

            return View(query);
        }

        [HttpPost]
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
