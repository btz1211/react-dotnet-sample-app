using Microsoft.EntityFrameworkCore;

// Class for building DbContextOptions based on either Configuration or existing DbContextOptions
// This is necessary for injecting a test context options during unit tests
public class AppDbOptionsBuilder {
    protected readonly IConfiguration Configuration;
    protected readonly DbContextOptions DbContextOptions;

    public AppDbOptionsBuilder(IConfiguration configuration) {
        this.Configuration = configuration;
    }

    public AppDbOptionsBuilder(DbContextOptions dbContextOptions) {
        this.DbContextOptions = dbContextOptions;
    }

    public DbContextOptions getOptions() {
        if (DbContextOptions != null) {
            return DbContextOptions;
        }

        return new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(this.Configuration.GetSection("ConnectionStrings")["WebApiDatabase"])
            .Options;
    }
}