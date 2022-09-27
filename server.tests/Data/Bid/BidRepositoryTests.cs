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
            List<Bid> bids = await BidRepository.GetBids("45eeb0f8-3e72-11ed-b878-0242ac120002");

            Assert.Equal(getExpectdBidsForHouse1(), bids);
        }

        [Fact]
        public async void Add_AppendsBidCorrectly() 
        {   
            Bid newBid = new Bid("new-bid", "45eeb0f8-3e72-11ed-b878-0242ac120002", "Jane Doe", 1000);
            await BidRepository.Add(newBid);

            List<Bid> bids = await BidRepository.GetBids("45eeb0f8-3e72-11ed-b878-0242ac120002");
            Dictionary<string, Bid> bidMap = bids.ToDictionary((Bid) => Bid.Id);
            Assert.Equal(newBid, bidMap["new-bid"]);
        }

        private List<Bid> getExpectdBidsForHouse1() {
            return new List<Bid>() {
                new Bid("45ee9f64-3e72-11ed-b878-0242ac120002", "45eeb0f8-3e72-11ed-b878-0242ac120002", "Sonia Reading", 200000),
                new Bid("45eea252-3e72-11ed-b878-0242ac120002", "45eeb0f8-3e72-11ed-b878-0242ac120002", "Dick Johnson", 202400)
            };
        }
    }
}