using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HouseEntity>().HasData(new List<HouseEntity> {
            new HouseEntity {
                Id = "45eeb0f8-3e72-11ed-b878-0242ac120002",
                Address = "123 Test st, New York",
                Country = "USA",
                Description = "A beautiful renovated Colonial in the heart of the best City, NYC!",
                Price = 90000
            },

            new HouseEntity {
                Id = "45eeaef0-3e72-11ed-b878-0242ac120002",
                Address = "234 Dummy st, Brooklyn",
                Country = "USA",
                Description = "An amazing investment property opportunity in this two-family house",
                Price = 12000000
            },
        });

        modelBuilder.Entity<BidEntity>().HasData(new List<BidEntity>
         {
            new BidEntity { Id = "45ee9f64-3e72-11ed-b878-0242ac120002", HouseId = "45eeb0f8-3e72-11ed-b878-0242ac120002", Bidder = "Sonia Reading", Amount = 200000 },
            new BidEntity { Id = "45eea252-3e72-11ed-b878-0242ac120002", HouseId = "45eeb0f8-3e72-11ed-b878-0242ac120002", Bidder = "Dick Johnson", Amount = 202400 },
            new BidEntity { Id = "45eea39c-3e72-11ed-b878-0242ac120002", HouseId = "45eeaef0-3e72-11ed-b878-0242ac120002", Bidder = "Mohammed Vahls", Amount = 302400 },
            new BidEntity { Id = "45eea4d2-3e72-11ed-b878-0242ac120002", HouseId = "45eeaef0-3e72-11ed-b878-0242ac120002", Bidder = "Jane Williams", Amount = 310500 },
            new BidEntity { Id = "45eea73e-3e72-11ed-b878-0242ac120002", HouseId = "45eeaef0-3e72-11ed-b878-0242ac120002", Bidder = "John Kepler", Amount = 315400 },
        });

    }
}