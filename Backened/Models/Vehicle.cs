using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace vehicleservicesystmapi.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        public ICollection<ServiceRecord> ServiceRecords { get; set; }
    }
}
