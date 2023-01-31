using Microsoft.AspNetCore.Identity;

namespace EMS_Api_Identity_React.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(ApplicationRoleStore appRoleStore,
            IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
            ILookupNormalizer lookupNormalizer, IdentityErrorDescriber identityErrorDescriber, ILogger<RoleManager<ApplicationRole>> logger)
            : base(appRoleStore, roleValidators, lookupNormalizer, identityErrorDescriber, logger)
        {

        }
    }
}
