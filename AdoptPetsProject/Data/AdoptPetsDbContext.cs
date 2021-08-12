namespace AdoptPetsProject.Data
{
    using AdoptPetsProject.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AdoptPetsDbContext : IdentityDbContext
    {
        public AdoptPetsDbContext(DbContextOptions<AdoptPetsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; init; }
        public DbSet<Kind> Kinds { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Pet>()
                .HasOne(p => p.Kind)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.KindId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
