namespace AdoptPetsProject.Models.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AdoptPetsProject.Services.Pets;

    using static Data.DataConstants.Pet;

    public class PetFormModel
    {
        [Required]
        [StringLength(BreedMaxLength, MinimumLength = BreedMinLength)]
        public string Breed { get; init; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        public string Gender { get; init; }

        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; init; }

        public DateTime BirthDate { get; init; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(
            int.MaxValue,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Display(Name = "Kind")]
        public int KindId { get; init; }

        public IEnumerable<PetKindServiceModel> Kinds { get; set; }
    }
}
