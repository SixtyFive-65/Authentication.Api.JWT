using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SabeloSethu.Api.Data.DomainModels.Application;
using SabeloSethu.Api.Models.Base;
using SabeloSethu.Api.Models.User;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Polls.Api.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly RoleManager<ApplicationRole> roleManager;

        private readonly IConfiguration configuration;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<GenericResponse<string>> GetToken(UserLoginModel user)
        {
            var response = new GenericResponse<string>();

            try
            {
                var dbUser = await userManager.FindByEmailAsync(user.Username);

                if (dbUser == null)
                {
                    Log.Warning($"User {user.Username} does not exist");

                    response.HttpStatusCode = System.Net.HttpStatusCode.NotFound;

                    response.ErrorMessage.Add($"User {user.Username} does not exist");

                    return response;
                }

                if (user != null && await userManager.CheckPasswordAsync(dbUser, user.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

                    var claims = new List<Claim>
                         {
                            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
                            new Claim(ClaimTypes.Email, dbUser.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                         };

                    var roles = await userManager.GetRolesAsync(dbUser);

                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        Issuer = configuration["Jwt:Issuer"],
                        Audience = configuration["Jwt:Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var generatedToken = tokenHandler.CreateToken(tokenDescriptor);

                    Log.Information($"Successfuly generated token for {user.Username}");

                    response.Data = tokenHandler.WriteToken(generatedToken);
                    response.IsSuccess = true;
                    response.Message = "Success";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                }

                return response;
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to generate token for {user.Username} - {ex.Message}", ex);

                response.ErrorMessage.Add(ex.Message);
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;

                return response;
            }
        }

        public async Task<GenericResponse<bool>> RegisterUser(RegisterUserModel model)
        {
            var response = new GenericResponse<bool>();

            try
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                var createUserResult = await userManager.CreateAsync(user, model.Password);

                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");

                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Success";
                    response.HttpStatusCode = System.Net.HttpStatusCode.OK;
                   
                    return response;
                }
                else
                {
                    foreach(var error in createUserResult.Errors)
                    {
                        response.ErrorMessage.Add(error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

                response.ErrorMessage.Add(ex.Message);
                response.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;

                return response;
            }

            return response;
        }
    }
}
