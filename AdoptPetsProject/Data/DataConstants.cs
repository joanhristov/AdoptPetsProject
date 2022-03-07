using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptPetsProject.Data
{
    public class DataConstants
    {
        public class Pet
        {
            public const int BreedMaxLength = 20;
            public const int BreedMinLength = 2;
            public const int NameMaxLength = 30;
            public const int NameMinLength = 3;
            public const int GenderMaxLength = 6;
            public const int DescriptionMinLength = 10;
            public const int AgeMinValue = 0;
            public const int AgeMaxValue = 20;
        }

        public class Dealer
        {
            public const int NameMaxLength = 25;
            public const int PhoneNumberMaxLength = 30;
        }

        public class Kind
        {
            public const int NameMaxLength = 25;
        }
    }
}
