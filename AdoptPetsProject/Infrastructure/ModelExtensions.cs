namespace AdoptPetsProject.Infrastructure
{
    using AdoptPetsProject.Services.Pets.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IPetModel pet)
            => pet.Breed + "-" + pet.Name + "-" + pet.Age;
    }
}
