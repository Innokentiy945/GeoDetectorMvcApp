using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeoDetectorMvcApp.Models
{
    [Table("WeatherTable")]
    public class WeatherModel
    {
        [Key]
        [Required]
        public string CloudType { get; set; }

        [Required]
        public string RainPersantage { get; set; }

        [Required]
        public string SunPersantage { get; set; }

        [Required]
        public string Temperature { get; set; }

        [Required]
        public string GeoLocation { get; set; }
    }
}
