using Microsoft.AspNetCore.Mvc;

public static class HouseEndpoints
{
    public static void UseHouseEndpoints(this WebApplication app)
    {
        // Batch Get
        app.MapGet("/houses", (IHouseRepository houseRepository) => (
            houseRepository.GetAll())).Produces<House>(StatusCodes.Status200OK);

        // Single Get
        app.MapGet("/houses/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
        {
            var house = await houseRepository.GetHouse(houseId);

            if (house == null)
            {
                return Results.Problem($"House with ID {houseId} cannot be found",
                statusCode: 404);
            }

            return Results.Ok(house);
        }).ProducesProblem(404).Produces<HouseDetail>(StatusCodes.Status200OK);

        // Delete API
        app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
        {
            try
            {
                await houseRepository.DeleteHouse(houseId);
                return Results.Ok();
            }
            catch (ArgumentException e)
            {
                return Results.Problem(e.Message, statusCode: 404);
            }
        }).ProducesProblem(404).Produces(StatusCodes.Status200OK);

        // Create API
        app.MapPost("/houses", async ([FromBody] HouseDetail houseDetail, IHouseRepository houseRepository) =>
        {

            if (!MiniValidation.MiniValidator.TryValidate(houseDetail, out var errors))
            {
                return Results.ValidationProblem(errors);
            }
            var newHouse = await houseRepository.AddHouse(houseDetail);
            return Results.Created($"houses/{newHouse.Id}", newHouse);

        }).Produces<HouseDetail>(StatusCodes.Status201Created).ProducesValidationProblem();

        // Update API
        app.MapPut("/houses", async ([FromBody] HouseDetail houseDetail, IHouseRepository houseRepository) =>
        {
            if (!MiniValidation.MiniValidator.TryValidate(houseDetail, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            try
            {
                var updatedHouse = await houseRepository.UpdateHouse(houseDetail);
                return Results.Ok(updatedHouse);
            }
            catch (ArgumentException e)
            {
                return Results.Problem(e.Message, statusCode: 404);
            }

        }).ProducesProblem(404).ProducesValidationProblem().Produces<HouseDetail>(StatusCodes.Status200OK);

    }
}