using SabeloSethu.Api.Models.User;

namespace Polls.Api.Repository.User
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(RegisterUserModel user);
        Task<string> GetToken(UserLoginModel user);
    }
}
