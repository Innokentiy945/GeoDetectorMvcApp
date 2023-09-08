using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoDetectorMvcApp.Models
{
    [Table("GeoTable")]
    public class GeoModel
    {
        [Key]
        [Required(ErrorMessage = "Id is required!")]
        public string? ItemId { get; set; }

        [Required(ErrorMessage = "Longitude is required!")]
        [StringLength(200, ErrorMessage = "Longitude can't be longer than 200 characters")]
        public string? Longitude { get; set; }

        [Required(ErrorMessage = "Latitude is required!")]
        [StringLength(200, ErrorMessage = "Latitude can't be longer than 200 characters")]
        public string? Latitude { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(200, ErrorMessage = "Name can't be longer than 200 characters")]
        public string? Name { get; set; }
    }
}
