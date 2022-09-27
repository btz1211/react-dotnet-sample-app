
public interface IHouseController {
    Task<List<House>> GetAll();
    Task<HouseDetail?> GetHouse(string id);
    void DeleteHouse(string id);
    Task<HouseDetail> AddHouse(HouseDetail houseDetail);
    Task<HouseDetail> UpdateHouse(HouseDetail houseDetail);
}

/* 
    Controller containing all business logic for manipulating houses
 */
public class HouseController : IHouseController {
    IHouseRepository HouseRepository { get; }

    public HouseController(IHouseRepository houseRepository) {
        this.HouseRepository = houseRepository;
    }

    public async Task<List<House>> GetAll() {
        return await HouseRepository.GetAll();
    }

    public async Task<HouseDetail?> GetHouse(string houseId) {
         return await HouseRepository.GetHouse(houseId);
    }

    public async Task<HouseDetail> AddHouse(HouseDetail houseDetail) {
        validateHouse(houseDetail);

        var newHouse = await HouseRepository.AddHouse(houseDetail);
        return newHouse;
    }

    public async Task<HouseDetail> UpdateHouse(HouseDetail houseDetail) {
        validateHouse(houseDetail);

        var updatedHouse = await HouseRepository.UpdateHouse(houseDetail);
        return updatedHouse;
    }

    public async void DeleteHouse(string houseId) {
        await HouseRepository.DeleteHouse(houseId);
    }

    private void validateHouse(HouseDetail houseDetail) {
        if (!MiniValidation.MiniValidator.TryValidate(houseDetail, out var errors))
        {
            throw new BadArgumentException(errors);
        }
    }
}