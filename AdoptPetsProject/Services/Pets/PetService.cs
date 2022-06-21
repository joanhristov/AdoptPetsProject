namespace AdoptPetsProject.Services.Pets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AdoptPetsProject.Data;
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Models;
    using AdoptPetsProject.Services.Pets.Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class PetService : IPetService
    {
        private readonly AdoptPetsDbContext data;
        private readonly IConfigurationProvider mapper;

        public PetService(AdoptPetsDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

        public PetQueryServiceModel All(
            string breed = null,
            string searchTerm = null,
            PetSorting sorting = PetSorting.Age,
            int currentPage = 1,
            int petsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var petsQuery = this.data.Pets
                .Where(p => !publicOnly || p.IsPublic);

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

        public IEnumerable<LatestPetsServiceModel> Latest()
            => this.data
                .Pets
                .Where(p => p.IsPublic)
                .OrderByDescending(p => p.Id)
                .ProjectTo<LatestPetsServiceModel>(this.mapper)
                .Take(3)
                .ToList();

        public PetDetailsServiceModel Details(int id)
            => this.data
                .Pets
                .Where(p => p.Id == id)
                .ProjectTo<PetDetailsServiceModel>(this.mapper)
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
                DonatorId = donatorId,
                IsPublic = false
            };

            this.data.Pets.Add(petData);

            this.data.SaveChanges();

            return petData.Id;
        }

        public bool Edit(int id,
            string breed,
            string name,
            string gender,
            int age,
            DateTime birthDate,
            string description,
            string imageUrl,
            int kindId,
            bool isPublic)
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
            petData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<PetServiceModel> ByUser(string userId)
            => GetPets(this.data
                .Pets
                .Where(p => p.Donator.UserId == userId));

        public bool IsByDonator(int petId, int donatorId)
            => this.data
                .Pets
                .Any(p => p.Id == petId && p.DonatorId == donatorId);

        public void ChangeVisibility(int petId)
        {
            var pet = this.data.Pets.Find(petId);

            pet.IsPublic = !pet.IsPublic;

            this.data.SaveChanges();
        }

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
                .ProjectTo<PetKindServiceModel>(this.mapper)
                .ToList();

        public bool KindExists(int kindId)
            => this.data
                .Kinds
                .Any(k => k.Id == kindId);

        private IEnumerable<PetServiceModel> GetPets(IQueryable<Pet> petQuery)
            => petQuery
                .ProjectTo<PetServiceModel>(this.mapper)
                .ToList();
    }
}
