using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoDetectorMvcApp.Models;

[Table("CombinedTable")]
public class CombinedModel
{
    
    [Key, ForeignKey("Id")]
    public string Id { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string Name { get; set; }
    public string Temperature { get; set; }
    public string GeoLocation { get; set; }
}