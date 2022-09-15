
using Moq;

public class HouseControllerTests {
    IHouseController HouseController;
    Mock<IHouseRepository> MockHouseRepository;

    public HouseControllerTests() {
        MockHouseRepository = new Mock<IHouseRepository>();
        HouseController = new HouseController(MockHouseRepository.Object);
    }

    [Fact]
    public async void GetAll_GetsHousesProperly() {
        // setup mocks
        MockHouseRepository.Setup(m => m.GetAll()).Returns(Task.FromResult(getMockHouses()));

        List<House> houses = await HouseController.GetAll();
        Assert.Equal(getMockHouses(), houses);
    }

    [Fact]
    public async void GetHouse_GetsHouseProperly() {
        // setup mocks
        HouseDetail expectedHouse = getMockHouseDetail();
        MockHouseRepository.Setup(m => m.GetHouse(1)).Returns(Task.FromResult(expectedHouse));

        HouseDetail house = await HouseController.GetHouse(1);
        Assert.Equal(expectedHouse, house);
    }

    [Fact]
    public async void AddHouse_CompletesProperly() {
        // setup mocks
        HouseDetail expectedHouse = getMockHouseDetail();
        MockHouseRepository.Setup(m => m.AddHouse(expectedHouse)).Returns(Task.FromResult(expectedHouse));

        HouseDetail house = await HouseController.AddHouse(expectedHouse);
        Assert.Equal(expectedHouse, house);
    }

    [Fact]
    public async void AddHouse_ValidationFailure() {
        await Assert.ThrowsAsync<BadArgumentException>(() => HouseController.AddHouse(getBadMockHouseDetail()));
    }

    [Fact]
    public async void UpdateHouse_CompletesProperly() {
        // setup mocks
        HouseDetail expectedHouse = getMockHouseDetail();
        MockHouseRepository.Setup(m => m.UpdateHouse(expectedHouse)).Returns(Task.FromResult(expectedHouse));

        HouseDetail house = await HouseController.UpdateHouse(expectedHouse);
        Assert.Equal(expectedHouse, house);
    }

    [Fact]
    public async void UpdateHouse_ValidationFailure() {
        await Assert.ThrowsAsync<BadArgumentException>(() => HouseController.UpdateHouse(getBadMockHouseDetail()));
    }

    private List<House> getMockHouses() {
        return new List<House>() {
            new House(1, "123 test st.", "US", "Great House", 10000),
            new House(2, "234 main st.", "US", "Better House", 20000),
        };
    }

    private HouseDetail getMockHouseDetail() {
        return new HouseDetail(1, "123 test st.", "US", "Great House", "photo", 10000);
    }

    private HouseDetail getBadMockHouseDetail() {
        // address is required, thus making this house detail invalid
        return new HouseDetail(1, null, "US", "Great House", "photo", 10000);
    }
}