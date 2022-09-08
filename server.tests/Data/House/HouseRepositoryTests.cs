using server.tests.Data;

namespace server.tests
{
    public class HouseRepositoryTests
    { 
        static IHouseRepository HouseRepository = new HouseRepository(new InMemoryDbFixture().Context);

        [Fact]
        public async void GetAll_RetrievesDataCorrectly()
        {
            List<House> houses = await HouseRepository.GetAll();

            Assert.Equal(getExpectedHouses(), houses);
        }

        [Fact]
        public async void GetHouse_RetrievesDataCorrectly()
        {
            HouseDetail? house = await HouseRepository.GetHouse(1);
            HouseDetail expectedHouse = new HouseDetail(1, "123 Test st, New York", "USA", "A beautiful renovated Colonial in the heart of the best City, NYC!", null, 90000);
            Assert.Equal(expectedHouse, house);
        }

        [Fact]
        public async void AddAndDeleteHouse_CompletesSuccessfully()
        {
            // Add a new house
            HouseDetail newHouse = new HouseDetail(3, "345 main st, Queens", "USA", "Lovely colonial", null, 1000000);
            await HouseRepository.AddHouse(newHouse);

            // assert data on the new saved house
            HouseDetail? house = await HouseRepository.GetHouse(3);
            Assert.Equal(house, newHouse);

            // clean up houses after
            await HouseRepository.DeleteHouse(3);
            List<House> houses = await HouseRepository.GetAll();
            Assert.Equal(getExpectedHouses(), houses);
        }

        [Fact]
        public async void UpdateHouse_CompletesSuccessfully()
        {
            HouseDetail? originalHouse = await HouseRepository.GetHouse(1);

            // update the house details
            HouseDetail updatedHouse = new HouseDetail(1, "123 Test st, Queens", "USA", "changed description", null, 80000);
            await HouseRepository.UpdateHouse(updatedHouse);

            // make sure house is updated
            HouseDetail? house = await HouseRepository.GetHouse(1);
            Assert.Equal(updatedHouse, house);

            // revert the house back
            await HouseRepository.UpdateHouse(originalHouse);
            house = await HouseRepository.GetHouse(1);
            Assert.Equal(originalHouse, house);
        }

        private List<House> getExpectedHouses()
        {
            return new List<House>()
            {
                new House(1, "123 Test st, New York", "USA", "A beautiful renovated Colonial in the heart of the best City, NYC!", 90000),
                new House(2, "234 Dummy st, Brooklyn", "USA", "An amazing investment property opportunity in this two-family house", 12000000)
            };
        }
    }
}
