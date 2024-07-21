namespace MWalks.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataSeeder.SeedRegions(modelBuilder);
            DataSeeder.SeedDifficulties(modelBuilder);
            DataSeeder.SeedWalks(modelBuilder);

        }

        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
