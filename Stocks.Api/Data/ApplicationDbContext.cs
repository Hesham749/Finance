namespace Stocks.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userRoleId = "97d784d1-d212-4c28-815f-e4b00c5f85e3";
            var adminRoleId = "3a97653a-9b3b-42e1-b158-7922a30e3f08";
            var roles = new List<IdentityRole>() {
            new () { Name = "User", NormalizedName = "USER" , Id = userRoleId  , ConcurrencyStamp = userRoleId},
            new () { Name = "Admin", NormalizedName = "ADMIN" , Id = adminRoleId , ConcurrencyStamp = adminRoleId }
            };
            builder.Entity<IdentityRole>()
                .HasData(roles);

            builder.Entity<Portfolio>().HasKey(p => new { p.StockId, p.AppUserId });
            builder.Entity<Stock>()
                .HasIndex(s => s.Symbol);

        }

    }
}
