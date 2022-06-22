namespace AdoptPetsProject.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Xunit;
    using AdoptPetsProject.Controllers;

    public class HomeControllerTest
    {
        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModelAndData()
        //    => MyMvc
        //        .Pipeline()
        //        .ShouldMap("/")
        //        .To<HomeController>(c => c.Index())
        //        .Which(controller => controller
        //            .WithData(GetPets()))
        //        .ShouldReturn()
        //        .View(view => view
        //            .WithModelOfType<List<LatestPetsServiceModel>>()
        //            .Passing(m => m.Should().HaveCount(3)));

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

        //private static IEnumerable<Pet> GetPets()
        //    => Enumerable.Range(0, 10).Select(i => new Pet());
    }
}
