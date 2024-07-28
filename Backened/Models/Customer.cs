using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using vehicleservicesystmapi.Models;

public class Customer
{
    [Key]
    public int CustomerID { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 5)]
    public string Address { get; set; }

    [Required]
    [Phone]
    public string ContactNumber { get; set; }

    public ICollection<Vehicle> Vehicles { get; set; }
}
