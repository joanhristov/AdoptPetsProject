namespace AdoptPetsProject.Services.Pets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Models;

    public class PetService : IPetService
    {
        private readonly AdoptPetsDbContext data;

        public PetService(AdoptPetsDbContext data)
            => this.data = data;

        public PetQueryServiceModel All(
            string breed,
            string searchTerm,
            PetSorting sorting,
            int currentPage,
            int petsPerPage)
        {
            var petsQuery = this.data.Pets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(breed))
            {
                petsQuery = petsQuery.Where(p => p.Breed == breed);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                petsQuery = petsQuery.Where(p =>
                (p.Breed + " " + p.Name).ToLower().Contains(searchTerm.ToLower()) ||
                p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            petsQuery = sorting switch
            {
                PetSorting.Age => petsQuery.OrderByDescending(p => p.Age),
                PetSorting.BreedAndKind => petsQuery.OrderBy(p => p.Breed).ThenBy(p => p.Kind),
                _ => petsQuery.OrderByDescending(p => p.Id)
            };

            var totalPets = petsQuery.Count();

            var pets = GetPets(petsQuery
                .Skip((currentPage - 1) * petsPerPage)
                .Take(petsPerPage));

            return new PetQueryServiceModel
            {
                TotalPets = totalPets,
                CurrentPage = currentPage,
                PetsPerPage = petsPerPage,
                Pets = pets
            };
        }
        public PetDetailsServiceModel Details(int id)
            => this.data
                .Pets
            .Where(p => p.Id == id)
            .Select(p => new PetDetailsServiceModel
            {
                Id = p.Id,
                Breed = p.Breed,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Age = p.Age,
                KindName = p.Kind.Name,
                DonatorId = p.DonatorId,
                DonatorName = p.Donator.Name,
                UserId = p.Donator.UserId
            })
            .FirstOrDefault();

        public int Create(string breed, string name, string gender, int age, DateTime birthDate, string description, string imageUrl, int kindId, int donatorId)
        {
            var petData = new Pet
            {
                Breed = breed,
                Name = name,
                Gender = gender,
                Age = age,
                BirthDate = birthDate,
                Description = description,
                ImageUrl = imageUrl,
                KindId = kindId,
                DonatorId = donatorId
            };

            this.data.Pets.Add(petData);

            this.data.SaveChanges();

            return petData.Id;
        }

        public bool Edit(int id, string breed, string name, string gender, int age, 
            DateTime birthDate, string description, string imageUrl, int kindId)
        {
            var petData = this.data.Pets.Find(id);

            if (petData == null)
            {
                return false;
            }

            petData.Breed = breed;
            petData.Name = name;
            petData.Gender = gender;
            petData.Age = age;
            petData.BirthDate = birthDate;
            petData.Description = description;
            petData.ImageUrl = imageUrl;
            petData.KindId = kindId;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByDonator(int petId, int donatorId)
            => this.data
                .Pets
                .Any(p => p.Id == petId && p.DonatorId == donatorId);

        public IEnumerable<PetServiceModel> ByUser(string userId)
            => GetPets(this.data
                .Pets
                .Where(p => p.Donator.UserId == userId));

        public IEnumerable<string> AllBreeds()
            => this.data
                .Pets
                .Select(p => p.Breed)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
        public IEnumerable<PetKindServiceModel> AllKinds()
            => this.data
                .Kinds
                .Select(k => new PetKindServiceModel
                {
                    Id = k.Id,
                    Name = k.Name
                })
                .ToList();

        public bool KindExists(int kindId)
            => this.data
                .Kinds
                .Any(k => k.Id == kindId);

        private static IEnumerable<PetServiceModel> GetPets(IQueryable<Pet> petQuery)
            => petQuery
                .Select(p => new PetServiceModel
                {
                    Id = p.Id,
                    Breed = p.Breed,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Age = p.Age,
                    KindName = p.Kind.Name
                })
                .ToList();
    }
}
