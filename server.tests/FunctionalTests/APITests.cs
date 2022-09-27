
using Newtonsoft.Json;

public class APITests : IClassFixture<FunctionalTestFixture> {

    HttpClient Client;
    public APITests(FunctionalTestFixture factory) {
        Client = factory.CreateClient();
    }

    [Fact]
    public async void GetRootPage_RetrievesDataProperly() {
        var response = await Client.GetAsync("/houses/45eeb0f8-3e72-11ed-b878-0242ac120002/bids");
        var content = await response.Content.ReadAsStringAsync();

        List<Bid> bids = JsonConvert.DeserializeObject<List<Bid>>(content);

        Assert.Equal(2, bids.Count);
        Assert.Equal("Sonia Reading", bids[0].Bidder);
        Assert.Equal("Dick Johnson", bids[1].Bidder);
    }

    [Fact]
    public async void GetHouses_RetrievesDataProperly() {
        var response = await Client.GetAsync("/houses/45eeb0f8-3e72-11ed-b878-0242ac120002");
        var content = await response.Content.ReadAsStringAsync();

        HouseDetail house = JsonConvert.DeserializeObject<HouseDetail>(content);

        Assert.Equal("123 Test st, New York", house.Address);
        Assert.Equal(90000, house.Price);
    }
}