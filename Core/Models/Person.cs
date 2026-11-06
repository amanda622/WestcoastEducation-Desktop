using System.Text.Json.Serialization;
using WestcoastEducation.Interfaces;

namespace WestcoastEducation.Models;

public class Person : IHasId
{
    // RestDb gives every record its own id in the "_id" field.
    [JsonPropertyName("_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DbId { get; set; }

    [JsonIgnore]
    public string Id => PersonNumber;
    public required string PersonNumber {get; set;}    
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string Email {get; set;}
    public string? Phone {get; set;}
    // Composition
    public Address? Address {get; set;}
    public override string ToString()
    {
        return $"FirstName: {FirstName} LastName: {LastName} PersonNumber: {PersonNumber} Email: {Email} Phone: {Phone} Address: {Address}";   
    }

}
