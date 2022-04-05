namespace AdoptPetsProject.Models.Donators
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Donator;

    public class BecomeDonatorFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
