namespace WestcoastEducation.Models;

public class Address
{
    public required string AddressLine {get; set;} 
    public required string PostalCode {get; set;} 
    public required string City {get; set;} 
    public override string ToString()
    {
        return $"AddressLine: {AddressLine} PostalCode: {PostalCode} City: {City}";
    }

}
