using server.tests.Data;

namespace server.tests 
{
    public class BidRepositoryTests : RepositoryTestsBase
    {
        IBidRepository BidRepository;

        public BidRepositoryTests() {
            BidRepository = new BidRepository(dbFixture.Context);
        }

        [Fact]
        public async void GetAll_RetrievesDataCorrectly()
        {
            List<Bid> bids = await BidRepository.GetBids(1);

            Assert.Equal(getExpectdBidsForHouse1(), bids);
        }

        [Fact]
        public async void Add_AppendsBidCorrectly() 
        {   
            Bid newBid = new Bid(6, 2, "Jane Doe", 1000);
            await BidRepository.Add(newBid);

            List<Bid> bids = await BidRepository.GetBids(2);
            Dictionary<int, Bid> bidMap = bids.ToDictionary((Bid) => Bid.Id);
            Assert.Equal(newBid, bidMap[6]);
        }

        private List<Bid> getExpectdBidsForHouse1() {
            return new List<Bid>() {
                new Bid(1, 1, "Sonia Reading", 200000),
                new Bid(2, 1, "Dick Johnson", 202400)
            };
        }
    }
}