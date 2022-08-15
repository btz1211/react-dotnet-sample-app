using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HouseEntity>().HasData(new List<HouseEntity> {
            new HouseEntity {
                Id = 1,
                Address = "123 Test st, New York",
                Country = "USA",
                Description = "A beautiful renovated Colonial in the heart of the best City, NYC!",
                Price = 90000
            },

            new HouseEntity {
                Id = 2,
                Address = "234 Dummy st, Brooklyn",
                Country = "USA",
                Description = "An amazing investment property opportunity in this two-family house",
                Price = 12000000
            },
        });

        modelBuilder.Entity<BidEntity>().HasData(new List<BidEntity>
         {
            new BidEntity { Id = 1, HouseId = 1, Bidder = "Sonia Reading", Amount = 200000 },
            new BidEntity { Id = 2, HouseId = 1, Bidder = "Dick Johnson", Amount = 202400 },
            new BidEntity { Id = 3, HouseId = 2, Bidder = "Mohammed Vahls", Amount = 302400 },
            new BidEntity { Id = 4, HouseId = 2, Bidder = "Jane Williams", Amount = 310500 },
            new BidEntity { Id = 5, HouseId = 2, Bidder = "John Kepler", Amount = 315400 },
        });

    }
}