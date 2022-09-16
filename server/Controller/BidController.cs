public interface IBidController {
    Task<List<Bid>> GetBids(int houseId);
    Task<Bid> Add(int houseId, Bid bid);
}

/* 
    Controller containing all business logic for manipulating bids
 */
public class BidController : IBidController
{
    IHouseRepository HouseRepository { get; }
    IBidRepository BidRepository { get; }

    public BidController(IHouseRepository houseRepository, IBidRepository bidRepository) {
        HouseRepository = houseRepository;
        BidRepository = bidRepository;
    }

    public async Task<Bid> Add(int houseId, Bid bid)
    {
        validateHouse(houseId);
        validateBid(bid);

        return await BidRepository.Add(bid);    
    }

    public async Task<List<Bid>> GetBids(int houseId)
    {
        validateHouse(houseId);

        return await BidRepository.GetBids(houseId);    
    }

    private void validateHouse(int houseId) {
        // validate to ensure house exists
        if (HouseRepository.GetHouse(houseId).Result == null)
        {
            throw new NotFoundException($"House with ID {houseId} cannot be found");
        }
    }

    private void validateBid(Bid bid) {
        if (!MiniValidation.MiniValidator.TryValidate(bid, out var errors))
        {
            throw new BadArgumentException(errors);
        }
    }
}