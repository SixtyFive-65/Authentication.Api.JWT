using Polls.Api.Repository.User;
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
        public async Task<string> GetToken(UserLoginModel user)
        {
            return await userRepository.GetToken(user);
        }

        public Task<bool> RegisterUser(RegisterUserModel user)
        {
            return userRepository.RegisterUser(user);
        }
    }
}
