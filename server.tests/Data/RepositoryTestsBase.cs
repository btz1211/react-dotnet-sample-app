using server.tests.Data;

namespace server.tests
{
    public abstract class RepositoryTestsBase : IDisposable
    {
        protected InMemoryDbFixture dbFixture;

        public RepositoryTestsBase() {
            dbFixture = new InMemoryDbFixture();
        }

        public void Dispose() {
            dbFixture.Dispose();
        }

    }
}