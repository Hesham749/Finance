

namespace Finance.Api.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; }
    }
}
