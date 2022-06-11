namespace AdoptPetsProject.Test.Controllers.Api
{
    using AdoptPetsProject.Controllers.Api;
    using AdoptPetsProject.Test.Mocks;
    using Xunit;

    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            //Arrange
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            // Act
            var result = statisticsController.GetStatistics();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.TotalPets);
            Assert.Equal(10, result.TotalAdoptions);
            Assert.Equal(15, result.TotalUsers);
        }
    }
}
