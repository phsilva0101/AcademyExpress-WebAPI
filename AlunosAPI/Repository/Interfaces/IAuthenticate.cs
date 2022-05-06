namespace AlunosAPI.Repository.Interfaces
{
    public interface IAuthenticate
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> Authenticate(string email, string password);
        Task Logout();
    }
}
