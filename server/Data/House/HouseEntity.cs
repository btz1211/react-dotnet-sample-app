using System.ComponentModel.DataAnnotations;

public class HouseEntity {
    [property: Key]
    public string Id{ get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? Description { get; set; }
    public int Price { get; set; }
    public string? Photo { get; set; }
}