using WestcoastEducation.Enums;
using WestcoastEducation.Interfaces;
using System.Text.Json.Serialization;
namespace WestcoastEducation.Models;

public class Course : IHasId
{
    // RestDb gives every record its own id in the "_id" field.
    // We need it to update or delete the record later.
    [JsonPropertyName("_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DbId { get; set; }

    [JsonIgnore]
    public string Id => CourseNumber;

    public required string CourseNumber { get; set; } 
    public required string Title { get; set; }
    public required int LengthInWeeks { get; set; }
    
    public required DateTime StartDate {get; set;}

    [JsonIgnore]
    public DateTime EndDate 
    {
        get {return StartDate.AddDays(LengthInWeeks * 7);}
    }

    public required CourseType CourseType {get; set;}

    public override string ToString()
    {
        return $"CourseNumber: {CourseNumber} | Title: {Title} | CourseType: {CourseType} | StartDate: {StartDate:yyyy-MM-dd}| EndDate: {EndDate:yyyy-MM-dd} | LengthInWeeks: {LengthInWeeks}" ;
    }
}
