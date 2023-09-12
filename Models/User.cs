using System.ComponentModel.DataAnnotations;

namespace GeoDetectorMvcApp.Models;

public class User
{
    [Display(Name = "User Id")]
    public string UserName { get; set; }
    public string Password { get; set; }
}

