using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EMS_Api_Identity_React.Models.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(ApplicationUserStore applicationUserStore, IOptions<IdentityOptions> options,
             IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
             IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer lookupNormalizer,
             IdentityErrorDescriber identityErrorDescriber, IServiceProvider service, ILogger<ApplicationUserManager> logger)
             : base(applicationUserStore, options, passwordHasher, userValidators, passwordValidators, lookupNormalizer, identityErrorDescriber, service, logger)
        {
        }
    }
}
