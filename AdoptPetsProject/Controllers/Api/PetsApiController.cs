namespace AdoptPetsProject.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using AdoptPetsProject.Models.Api.Pets;
    using AdoptPetsProject.Services.Pets;

    [ApiController]
    [Route("api/pets")]
    public class PetsApiController : Controller
    {
        private readonly IPetService pets;

        public PetsApiController(IPetService pets) 
            => this.pets = pets;

        [HttpGet]
        public PetQueryServiceModel All([FromQuery] AllPetsApiRequestModel query) 
            => this.pets.All(
                query.Breed,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.PetsPerPage);
    }
}
