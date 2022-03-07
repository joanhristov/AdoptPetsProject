namespace AdoptPetsProject.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Pet;

    public class Pet
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(BreedMaxLength)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(GenderMaxLength)]
        public string Gender { get; set; }
        
        [Required]
        public int Age { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public int KindId { get; set; }

        public Kind Kind { get; init; }

        public int DonatorId { get; init; }

        public Donator Donator { get; init; }
    }
}
