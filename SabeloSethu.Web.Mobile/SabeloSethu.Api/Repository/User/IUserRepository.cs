using SabeloSethu.Api.Models.Base;
using SabeloSethu.Api.Models.User;

namespace Polls.Api.Repository.User
{
    public interface IUserRepository
    {
        Task<GenericResponse<bool>> RegisterUser(RegisterUserModel user);
        Task<GenericResponse<string>> GetToken(UserLoginModel user);
    }
}
