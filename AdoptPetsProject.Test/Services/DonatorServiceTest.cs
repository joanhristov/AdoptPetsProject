namespace AdoptPetsProject.Test.Services
{
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Services.Donators;
    using AdoptPetsProject.Test.Mocks;
    using Xunit;

    public class DonatorServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsDonatorShouldReturnTrueWhenUserIsDonator()
        {
            // Arrange
            var donatorService = GetDonatorDonatorService();

            //Act
            var result = donatorService.IsDonator(UserId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDonatorShouldReturnFalseWhenUserIsNotDonator()
        {
            // Arrange

            var donatorService = GetDonatorDonatorService();

            //Act
            var result = donatorService.IsDonator("AnotherUserId");

            //Assert
            Assert.False(result);
        }

        private static IDonatorService GetDonatorDonatorService()
        {
            var data = DatabaseMock.Instance;

            data.Donators.Add(new Donator
            {
                UserId = UserId,
                Name = "Gosho",
                PhoneNumber = "123456"
            });
            data.SaveChanges();

            return new DonatorService(data);
        }
    }
}
