﻿namespace AdoptPetsProject.Test.Controllers
{
    using AdoptPetsProject.Controllers;
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Models.Home;
    using AdoptPetsProject.Services.Pets;
    using AdoptPetsProject.Services.Statistics;
    using AdoptPetsProject.Test.Mocks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var pets = Enumerable
                .Range(0, 10)
                .Select(i => new Pet());

            data.Pets.AddRange(pets);
            data.Users.AddRange(new User());

            data.SaveChanges();

            var petService = new PetService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(petService, statisticsService);

            // Act
            var result = homeController.Index();

            // Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Pets.Count);
            Assert.Equal(10, indexViewModel.TotalPets);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            // Arrange
            var homeController = new HomeController(
                null,
                null);

            // Act
            var result = homeController.Error();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}