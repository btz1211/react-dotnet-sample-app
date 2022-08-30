public interface IBidRepository
{
    Task<List<Bid>> GetBids(int houseId);
    Task<Bid> Add(Bid bid);
}

public class BidRepository : IBidRepository
{
    public readonly AppDbContext dbContext;

    public BidRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Bid>> GetBids(int houseId)
    {
        var data = dbContext.Bids
        .Where(b => b.HouseId == houseId)
        .Select(b => entityToDto(b))
        .ToList();

        return await Task.FromResult(data);
    }

    public async Task<Bid> Add(Bid bid)
    {
        BidEntity bidEntity = dtoToEntity(bid);
        dbContext.Bids.Add(bidEntity);
        await dbContext.SaveChangesAsync();

        return entityToDto(bidEntity);
    }

    private static BidEntity dtoToEntity(Bid bid)
    {
        var entity = new BidEntity();
        entity.HouseId = bid.HouseId;
        entity.Bidder = bid.Bidder;
        entity.Amount = bid.Amount;

        return entity;
    }

    private static Bid entityToDto(BidEntity bidEntity)
    {
        return new Bid(bidEntity.Id, bidEntity.HouseId, bidEntity.Bidder, bidEntity.Amount);
    }
}