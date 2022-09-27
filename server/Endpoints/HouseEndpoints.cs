using Microsoft.AspNetCore.Mvc;

public static class HouseEndpoints
{
    public static void UseHouseEndpoints(this WebApplication app)
    {
        // Batch Get
        app.MapGet("/houses", (IHouseController houseController) => (
            houseController.GetAll())).Produces<House>(StatusCodes.Status200OK);

        // Single Get
        app.MapGet("/houses/{houseId}", async (string houseId, IHouseController houseController) =>
        {
            var house = await houseController.GetHouse(houseId);

            if (house == null)
            {
                return Results.Problem($"House with ID {houseId} cannot be found",
                statusCode: 404);
            }

            return Results.Ok(house);
        }).ProducesProblem(404).Produces<HouseDetail>(StatusCodes.Status200OK);

        // Delete API
        app.MapDelete("/houses/{houseId}", async (string houseId, IHouseController houseController) =>
        {
            try
            {
                houseController.DeleteHouse(houseId);
                return Results.Ok();
            }
            catch (ArgumentException e)
            {
                return Results.Problem(e.Message, statusCode: 404);
            }
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        // Create API
        app.MapPost("/houses", async ([FromBody] HouseDetail houseDetail, IHouseController houseController) =>
        {

            try 
            {
                var newHouse = await houseController.AddHouse(houseDetail);
                return Results.Created($"houses/{newHouse.Id}", newHouse);
            } 
            catch (BadArgumentException e) 
            {
                return Results.ValidationProblem(e.Errors);
            }
        }).Produces<HouseDetail>(StatusCodes.Status201Created).ProducesValidationProblem();

        // Update API
        app.MapPut("/houses", async ([FromBody] HouseDetail houseDetail, IHouseController houseController) =>
        {
            try 
            {
                var updatedHouse = await houseController.UpdateHouse(houseDetail);
                return Results.Ok(updatedHouse);
            } 
            catch (ArgumentException e) 
            {
                return Results.Problem(e.Message, statusCode: 404);
            } 
            catch (BadArgumentException e) 
            {
                return Results.ValidationProblem(e.Errors);
            }
        }).ProducesProblem(404).ProducesValidationProblem().Produces<HouseDetail>(StatusCodes.Status200OK);

    }
}