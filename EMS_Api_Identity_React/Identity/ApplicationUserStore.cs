using EMS_Api_Identity_React.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EMS_Api_Identity_React.Models.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context) : base(context)
        {

        }
    }
}
