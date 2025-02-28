namespace TopCon.Api.Services
{
    public interface ISeedUserRole
    {
        Task SeedRolesAsync();
        Task SeedUserAsync();
    }
}
