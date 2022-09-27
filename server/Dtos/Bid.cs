using System.ComponentModel.DataAnnotations;

public record Bid(
    string Id,
    string HouseId,
    [property: Required] string Bidder, 
    int Amount
);

