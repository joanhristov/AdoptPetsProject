namespace AdoptPetsProject.Services.Pets
{
    using System;
    using System.Collections.Generic;
    using AdoptPetsProject.Models;
    

    public interface IPetService
    {
        PetQueryServiceModel All(
            string breed,
            string searchTerm,
            PetSorting sorting,
            int currentPage,
            int petsPerPage);

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
            int kindId);

        IEnumerable<PetServiceModel> ByUser(string userId);

        bool IsByDonator(int petId, int donatorId);

        IEnumerable<string> AllBreeds();

        IEnumerable<PetKindServiceModel> AllKinds();

        bool KindExists(int KindId);
    }
}
