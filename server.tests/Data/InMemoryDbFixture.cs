using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace server.tests.Data
{
    /// <summary>
    /// Building a in-memory database using Sqlite for testing Repositories
    /// </summary>
    public class InMemoryDbFixture : IDisposable
    {
        private readonly SqliteConnection DbConnection;
        public AppDbContext Context { get; private set; }

        public InMemoryDbFixture()
        {
            DbConnection = new SqliteConnection("Data Source=:memory:");
            DbConnection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(DbConnection);

            Context = new AppDbContext(new AppDbOptionsBuilder(optionsBuilder.Options));
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            DbConnection.Dispose();
            Context.Dispose();
        }
    }
}
