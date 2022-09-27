using Microsoft.AspNetCore.Mvc;

public static class BidEndpoints
{
    public static void UseBidEndpoints(this WebApplication app)
    {
        // Get Bids for House
        app.MapGet("/houses/{houseId}/bids", async (string houseId, IBidController bidController) =>
        {
            try {
                var bids = await bidController.GetBids(houseId);
                return Results.Ok(bids);
            } catch (NotFoundException e) {
                return Results.Problem($"House with ID {houseId} cannot be found",
                statusCode: 404);
            }
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        // Create Bid
        app.MapPost("/houses/{houseId}/bids", async (string houseId, [FromBody] Bid bid, IBidController bidController) =>
        {
            try {
                var newBid = await bidController.Add(houseId, bid);
                return Results.Created($"/houses/{houseId}/bids", newBid);

            } catch (NotFoundException e) {
                return Results.Problem($"House with ID {houseId} cannot be found",
                statusCode: 404);
            } catch (BadArgumentException e) {
                return Results.ValidationProblem(e.Errors);
            }
        }).Produces<Bid>(StatusCodes.Status201Created)
        .ProducesProblem(400).ProducesValidationProblem().Produces(StatusCodes.Status200OK);

    }
}