namespace AdoptPetsProject.Services.Donators
{
    public interface IDonatorService
    {
        public bool IsDonator(string userId);

        public int IdByUser(string userId);
    }
}
