using Microsoft.AspNetCore.Mvc;

public static class BidEndpoints
{
    public static void UseBidEndpoints(this WebApplication app)
    {
        // Get Bids for House
        app.MapGet("/houses/{houseId:int}/bids", async (int houseId, IHouseRepository houseRepository,
        IBidRepository bidRepository) =>
        {
            // validate to ensure house exists
            if (await houseRepository.GetHouse(houseId) == null)
            {
                return Results.Problem($"House with ID {houseId} cannot be found",
                statusCode: 404);
            }

            var bids = await bidRepository.GetBids(houseId);
            return Results.Ok(bids);

        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        // Create Bid
        app.MapPost("/houses/{houseId:int}/bids", async (int houseId, [FromBody] Bid bid, IHouseRepository houseRepository,
        IBidRepository bidRepository) =>
        {
            // validate to ensure house exists
            if (await houseRepository.GetHouse(houseId) == null)
            {
                return Results.Problem($"House with ID {houseId} cannot be found",
                statusCode: 404);
            }

            if (!MiniValidation.MiniValidator.TryValidate(bid, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            var newBid = await bidRepository.Add(bid);
            return Results.Created($"/houses/{houseId}/bids", newBid);
        }).Produces<Bid>(StatusCodes.Status201Created)
        .ProducesProblem(400).ProducesValidationProblem().Produces(StatusCodes.Status200OK);

    }
}