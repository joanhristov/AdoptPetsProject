namespace AdoptPetsProject.Services.Pets
{
    using System;
    using System.Collections.Generic;
    using AdoptPetsProject.Models;
    using AdoptPetsProject.Services.Pets.Models;

    public interface IPetService
    {
        PetQueryServiceModel All(
            string breed = null,
            string searchTerm = null,
            PetSorting sorting = PetSorting.Age,
            int currentPage = 1,
            int petsPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestPetsServiceModel> Latest();

        PetDetailsServiceModel Details(int petId);

        int Create(
            string breed,
            string name,
            string gender,
            int age,
            DateTime birthDate,
            string description,
            string imageUrl,
            int kindId,
            int donatorId);

        bool Edit(
            int petId,
            string breed,
            string name,
            string gender,
            int age,
            DateTime birthDate,
            string description,
            string imageUrl,
            int kindId,
            bool isPublic);

        IEnumerable<PetServiceModel> ByUser(string userId);

        bool IsByDonator(int petId, int donatorId);

        void ChangeVisibility(int carId);

        IEnumerable<string> AllBreeds();

        IEnumerable<PetKindServiceModel> AllKinds();

        bool KindExists(int KindId);
    }
}
