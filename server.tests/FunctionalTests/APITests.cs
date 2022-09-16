
public class APITests : IClassFixture<FunctionalTestFixture> {

    HttpClient Client;
    public APITests(FunctionalTestFixture factory) {
        Client = factory.CreateClient();
    }

    [Fact]
    public async void GetRootPage_RetrievesDataProperly() {
        var response = await Client.GetAsync("/houses/1/bids");
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content);
    }

    [Fact]
    public async void GetHouses_RetrievesDataProperly() {
        var response = await Client.GetAsync("/houses");
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content);
    }
}