using Polls.Api.Repository.User;
using SabeloSethu.Api.Models.Base;
using SabeloSethu.Api.Models.User;

namespace Polls.Api.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<GenericResponse<string>> GetToken(UserLoginModel user)
        {
            return await userRepository.GetToken(user);
        }

        public async Task<GenericResponse<bool>> RegisterUser(RegisterUserModel user)
        {
            return await userRepository.RegisterUser(user);
        }
    }
}
