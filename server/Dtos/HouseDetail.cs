using System.ComponentModel.DataAnnotations;

public record HouseDetail(
    [property: Key] string Id,
    [property: Required] string? Address,
    [property: Required] string? Country,
    string? Description,
    string? Photo,
    int Price
);
