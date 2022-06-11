namespace AdoptPetsProject.Test.Mocks
{
    using AdoptPetsProject.Infrastructure;
    using AutoMapper;

    public static class MapperMock
    {
        public static IMapper Instance
        { 
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            } 
        }
    }
}
