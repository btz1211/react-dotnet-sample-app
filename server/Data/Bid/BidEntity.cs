using System.ComponentModel.DataAnnotations;

public class BidEntity
{
    [property: Key]
    public string Id { get; set; }
    public string HouseId { get; set; }
    public HouseEntity? House {get; set; }
    public string Bidder { get; set; } = string.Empty;
    public int Amount { get; set; }

}