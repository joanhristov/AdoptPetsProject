namespace AdoptPetsProject.Models.Pets
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddPetFormModel
    {
        [Required]
        [StringLength(PetBreedMaxLength, MinimumLength = PetBreedMinLength)]
        public string Breed { get; init; }

        [Required]
        [StringLength(PetNameMaxLength, MinimumLength = PetNameMinLength)]
        public string Name { get; init; }

        public string Gender { get; init; }

        [Range(PetAgeMinValue, PetAgeMaxValue)]
        public int Age { get; init; }

        public DateTime BirthDate { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(
            int.MaxValue, 
            MinimumLength = PetDescriptionMinLength, 
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Kind")]
        public int KindId { get; init; }
        
        public  IEnumerable<PetKindViewModel> Kinds { get; set; }
    }
}
