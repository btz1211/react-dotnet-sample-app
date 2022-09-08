using Microsoft.EntityFrameworkCore;

public class AppDbContext: DbContext {
    public AppDbContext(AppDbOptionsBuilder optionsBuilder) : base(optionsBuilder.getOptions()) { }
    
    public DbSet<HouseEntity> Houses => Set<HouseEntity>();
    public DbSet<BidEntity> Bids => Set<BidEntity>();

    protected override void  OnModelCreating(ModelBuilder builder) {
        SeedData.Seed(builder);
    }
}