namespace AdoptPetsProject.Test.Mocks
{
    using System;
    using AdoptPetsProject.Data;
    using Microsoft.EntityFrameworkCore;

    public static class DatabaseMock
    {
        public static AdoptPetsDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<AdoptPetsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new AdoptPetsDbContext(dbContextOptions);
            }
        }
    }
}
