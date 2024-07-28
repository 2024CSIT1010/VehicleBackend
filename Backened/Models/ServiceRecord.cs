using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleservicesystmapi.Models
{
    public class ServiceRecord
    {
        [Key]
        public int ServiceRecordID { get; set; }
        [Required]
        public int VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }
        [Required]
        public int ServiceRepresentativeID { get; set; }
        [ForeignKey("ServiceRepresentativeID")]
        public ServiceRepresentative ServiceRepresentative { get; set; }
        [Required]
        public DateTime ServiceDate { get; set; }
        [Required]
        public string Status { get; set; }
        
        public ICollection<BillOfMaterial> BillOfMaterials { get; set; }
        public Invoice Invoice { get; set; }
    }
}
