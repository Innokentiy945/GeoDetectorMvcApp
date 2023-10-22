using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoDetectorMvcApp.Models;

[Table("CombinedDataTable")]
public class CombinedModel
{
    
    [Key, ForeignKey("GeoLocation")]
    public string GeoLocation { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public string CloudType { get; set; }
    public string SunPersantage { get; set; }
    public string RainPersantage { get; set; }
    public string Temperature { get; set; }
}