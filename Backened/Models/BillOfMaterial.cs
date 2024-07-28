using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleservicesystmapi.Models
{
    public class BillOfMaterial
    {
        [Key]
        public int BillOfMaterialID { get; set; }
        [Required]
        public int ServiceRecordID { get; set; }
        [ForeignKey("ServiceRecordID")]
        public ServiceRecord ServiceRecord { get; set; }
        [Required]
        public int WorkItemID { get; set; }
        [ForeignKey("WorkItemID")]
        public WorkItem WorkItem { get; set; }
        [Required]
        public int Quantity { get; set; }



    }
}
