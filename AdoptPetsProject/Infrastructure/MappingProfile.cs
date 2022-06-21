namespace AdoptPetsProject.Infrastructure
{
    using AdoptPetsProject.Data.Models;
    using AdoptPetsProject.Models.Pets;
    using AdoptPetsProject.Services.Pets;
    using AdoptPetsProject.Services.Pets.Models;
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Kind, PetKindServiceModel>();

            this.CreateMap<Pet, LatestPetsServiceModel>();
            this.CreateMap<PetDetailsServiceModel, PetFormModel>();

            this.CreateMap<Pet, PetServiceModel>()
                .ForMember(p => p.KindName, cfg => cfg.MapFrom(p => p.Kind.Name));

            this.CreateMap<Pet, PetDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Donator.UserId))
                .ForMember(p => p.KindName, cfg => cfg.MapFrom(p => p.Kind.Name));
        }
    }
}
