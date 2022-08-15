using System.ComponentModel.DataAnnotations;

public record Bid(
    int Id,
    int HouseId,
    [property: Required] string Bidder, 
    int Amount
);

