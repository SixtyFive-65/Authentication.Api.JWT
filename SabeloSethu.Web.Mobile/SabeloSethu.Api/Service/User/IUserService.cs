using SabeloSethu.Api.Models.Base;
using SabeloSethu.Api.Models.User;

namespace Polls.Api.Service.User
{
    public interface IUserService
    {
        Task<GenericResponse<bool>> RegisterUser(RegisterUserModel user);
        Task<GenericResponse<string>> GetToken(UserLoginModel user);
    }
}
