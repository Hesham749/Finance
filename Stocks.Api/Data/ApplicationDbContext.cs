using Microsoft.EntityFrameworkCore;
using Stocks.Api.Models;

namespace Stocks.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
     
    }
}
