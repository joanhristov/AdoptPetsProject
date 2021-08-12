namespace AdoptPetsProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using static DataConstants;

    public class Pet
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(PetBreedMaxLength)]
        public string Breed { get; set; }

        [Required]
        [MaxLength(PetNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PetGenderMaxLength)]
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
    }
}
