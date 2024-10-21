using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SabeloSethu.Api.Data.DomainModels.Application;

namespace Polls.Api.Data
{
    public class ApiAuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApiAuthDbContext(DbContextOptions<ApiAuthDbContext> options)
            : base(options)
        {
        }
    }
}
