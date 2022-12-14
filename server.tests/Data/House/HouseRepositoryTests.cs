using server.tests.Data;

namespace server.tests
{
    public class HouseRepositoryTests : RepositoryTestsBase
    { 
        IHouseRepository HouseRepository;

        public HouseRepositoryTests() {
            HouseRepository = new HouseRepository(dbFixture.Context);
        }

        [Fact]
        public async void GetAll_RetrievesDataCorrectly()
        {
            List<House> houses = await HouseRepository.GetAll();

            Assert.Equal(getExpectedHouses(), houses);
        }

        [Fact]
        public async void GetHouse_RetrievesDataCorrectly()
        {
            HouseDetail? house = await HouseRepository.GetHouse("45eeb0f8-3e72-11ed-b878-0242ac120002");
            HouseDetail expectedHouse = new HouseDetail("45eeb0f8-3e72-11ed-b878-0242ac120002", "123 Test st, New York", "USA", "A beautiful renovated Colonial in the heart of the best City, NYC!", null, 90000);
            Assert.Equal(expectedHouse, house);
        }

        [Fact]
        public async void AddHouse_CompletesSuccessfully()
        {
            // Add a new house
            HouseDetail newHouse = new HouseDetail("new-house", "345 main st, Queens", "USA", "Lovely colonial", null, 1000000);
            await HouseRepository.AddHouse(newHouse);

            // assert data on the new saved house
            HouseDetail? house = await HouseRepository.GetHouse("new-house");
            Assert.Equal(house, newHouse);
        }

        [Fact]
        public async void UpdateHouse_CompletesSuccessfully()
        {
            HouseDetail? originalHouse = await HouseRepository.GetHouse("45eeb0f8-3e72-11ed-b878-0242ac120002");

            // update the house details
            HouseDetail updatedHouse = new HouseDetail("45eeb0f8-3e72-11ed-b878-0242ac120002", "123 Test st, Queens", "USA", "changed description", null, 80000);
            await HouseRepository.UpdateHouse(updatedHouse);

            // make sure house is updated
            HouseDetail? house = await HouseRepository.GetHouse("45eeb0f8-3e72-11ed-b878-0242ac120002");
            Assert.Equal(updatedHouse, house);
        }

        private List<House> getExpectedHouses()
        {
            return new List<House>()
            {
                new House("45eeaef0-3e72-11ed-b878-0242ac120002", "234 Dummy st, Brooklyn", "USA", "An amazing investment property opportunity in this two-family house", 12000000),
                new House("45eeb0f8-3e72-11ed-b878-0242ac120002", "123 Test st, New York", "USA", "A beautiful renovated Colonial in the heart of the best City, NYC!", 90000)
            };
        }
    }
}
