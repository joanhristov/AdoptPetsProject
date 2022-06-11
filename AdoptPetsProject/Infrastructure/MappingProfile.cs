namespace AdoptPetsProject.Infrastructure
{
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Models.Pets;
    using AdoptPetsProject.Services.Pets.Models;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Pet, LatestPetsServiceModel>();
            this.CreateMap<PetDetailsServiceModel, PetFormModel>();

            this.CreateMap<Pet, PetDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Donator.UserId));
        }
    }
}
