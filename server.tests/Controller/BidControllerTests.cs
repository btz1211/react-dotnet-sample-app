using Moq;

public class BidControllerTests {
    IBidController BidController;
    Mock<IHouseRepository> MockHouseRepository;
    Mock<IBidRepository> MockBidRepository;

    public BidControllerTests() {
        MockBidRepository = new Mock<IBidRepository>();
        MockHouseRepository = new Mock<IHouseRepository>();
        BidController = new BidController(MockHouseRepository.Object, MockBidRepository.Object);
    }

    [Fact]
    public async void  GetBids_GetsBidsProperly() {
        List<Bid> expectedBids = getMockBids();
        MockHouseRepository.Setup(m => m.GetHouse(1))
            .Returns(Task.FromResult(new HouseDetail(1, "", "", "", "",100000)));
        MockBidRepository.Setup(m => m.GetBids(1)).Returns(Task.FromResult<List<Bid>>(expectedBids));

        List<Bid> bids = await BidController.GetBids(1);    
        Assert.Equal(expectedBids, bids);
    }

    [Fact]
    public async void  GetBids_ThrowsNotFoundIfHouseIsUnavailable() {
        await Assert.ThrowsAsync<NotFoundException>(() => BidController.GetBids(1));
    }


    [Fact]
    public async void  Add_CreatesBidProperly() {
        Bid expectedBid = getMockBids()[0];
        MockHouseRepository.Setup(m => m.GetHouse(1))
            .Returns(Task.FromResult(new HouseDetail(1, "", "", "", "",100000)));
        MockBidRepository.Setup(m => m.Add(expectedBid)).Returns(Task.FromResult<Bid>(expectedBid));

        Bid bid = await BidController.Add(1, expectedBid);
        Assert.Equal(expectedBid, bid);
    }

    [Fact]
    public async void  Add_ThrowsNotFoundIfHouseIsUnavailable() {
        await Assert.ThrowsAsync<NotFoundException>(() => BidController.Add(1, getMockBids()[0]));
    }

    [Fact]
    public async void  Add_ThrowsNotFoundIfBidIsInvalid() {
        MockHouseRepository.Setup(m => m.GetHouse(1))
            .Returns(Task.FromResult(new HouseDetail(1, "", "", "", "",100000)));
        
        await Assert.ThrowsAsync<BadArgumentException>(() => BidController.Add(1, new Bid(1, 1, null, 10000)));

    }

    private List<Bid> getMockBids() {
        return new List<Bid>() {
            new Bid(1, 1, "john doe", 100),
            new Bid(2, 1, "jane doe", 200)
        };
    }
}