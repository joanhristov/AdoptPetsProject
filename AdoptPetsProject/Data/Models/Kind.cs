namespace AdoptPetsProject.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Kind
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<Pet> Pets { get; init; } = new List<Pet>();
    }
}
