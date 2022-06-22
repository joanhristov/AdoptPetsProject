namespace AdoptPetsProject.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using AdoptPetsProject.Data.Models;

    public class AdoptPetsDbContext : IdentityDbContext<User>
    {
        public AdoptPetsDbContext(DbContextOptions<AdoptPetsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; init; }
        public DbSet<Kind> Kinds { get; init; }
        public DbSet<Donator> Donators { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Pet>()
                .HasOne(p => p.Kind)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.KindId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Pet>()
                .HasOne(p => p.Donator)
                .WithMany(d => d.Pets)
                .HasForeignKey(p => p.DonatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Donator>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Donator>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
