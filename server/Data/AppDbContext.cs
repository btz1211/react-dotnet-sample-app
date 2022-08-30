using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext {
    protected readonly IConfiguration configuration;

    public AppDbContext(IConfiguration configuration) {
        this.configuration = configuration;
    }

    public DbSet<HouseEntity> Houses => Set<HouseEntity>();
    public DbSet<BidEntity> Bids => Set<BidEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        optionsBuilder
            .UseNpgsql(this.configuration.GetSection("ConnectionStrings")["WebApiDatabase"]);
    }

    protected override void  OnModelCreating(ModelBuilder builder) {
        SeedData.Seed(builder);
    }
}