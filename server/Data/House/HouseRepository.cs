using Microsoft.EntityFrameworkCore;

public interface IHouseRepository {
    Task<List<House>> GetAll();
    Task<HouseDetail?> GetHouse(int id);
    Task DeleteHouse(int id);
    Task<HouseDetail> AddHouse(HouseDetail houseDetail);
    Task<HouseDetail> UpdateHouse(HouseDetail houseDetail);
}

public class HouseRepository: IHouseRepository {

    private readonly AppDbContext context;

    public HouseRepository(AppDbContext context) {
        this.context = context;
    }

    public async Task<List<House>> GetAll() {
        return await context.Houses.Select(row => (
            new House(row.Id, row.Address, row.Country, 
            row.Description, row.Price))).ToListAsync();
    }

    public async Task<HouseDetail?> GetHouse(int id) {
        var result = await context.Houses.SingleOrDefaultAsync(
            h => h.Id == id);
        
        if (result == null) {
            return null;
        }

        return EntityToDto(result);
    }

    public async Task<HouseDetail> AddHouse(HouseDetail houseDetail) {

        var houseEntity = new HouseEntity();
        this.DtoToEntity(houseEntity, houseDetail);

        context.Houses.Add(houseEntity);
        await context.SaveChangesAsync();

        return EntityToDto(houseEntity);
    }

    public async Task<HouseDetail> UpdateHouse(HouseDetail houseDetail) {
        var houseEntity = await context.Houses.FindAsync(houseDetail.Id);

        if (houseEntity == null) {
            throw new ArgumentException($"House with Id: {houseDetail.Id} cannot be found");
        }

        // update the house entity to the new data
        DtoToEntity(houseEntity, houseDetail);
        context.Entry(houseEntity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return EntityToDto(houseEntity);
    }

    public async Task DeleteHouse(int houseId) {
        var houseEntity = await context.Houses.FindAsync(houseId);

        if (houseEntity == null) {
            throw new ArgumentException($"House with Id: {houseId} cannot be found");
        }

        context.Houses.Remove(houseEntity);
        await context.SaveChangesAsync();
    }

    private void DtoToEntity(HouseEntity houseEntity, HouseDetail houseDetail) {
        houseEntity.Address = houseDetail.Address;
        houseEntity.Country = houseDetail.Country;
        houseEntity.Description = houseDetail.Description;
        houseEntity.Id = houseDetail.Id;
        houseEntity.Price = houseDetail.Price;
        houseEntity.Photo = houseDetail.Photo;
    }

    private HouseDetail EntityToDto(HouseEntity houseEntity) {
        return new HouseDetail(houseEntity.Id, houseEntity.Address, houseEntity.Country,
        houseEntity.Description, houseEntity.Photo, houseEntity.Price);
    }
}