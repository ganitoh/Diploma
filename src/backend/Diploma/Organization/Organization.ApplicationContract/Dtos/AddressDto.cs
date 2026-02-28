namespace Organization.ApplicationContract.Dtos;

public class AddressDto
{
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Settlement { get; set; }
    public string? Street { get; set; }
    public string? BuildingNumber { get; set; }
    public string? RoomNumber { get; set; }
    public string? PostCode { get; set; }
}