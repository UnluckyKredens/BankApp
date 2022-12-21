using BankAppModels.Informations.User;

namespace AuthInfrastructure.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetToken(string username, string password);
        Task CreateUser(RegisterDTO registerDTO);
    }
}
