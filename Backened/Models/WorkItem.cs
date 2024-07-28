    using System.ComponentModel.DataAnnotations;

    namespace vehicleservicesystmapi.Models
    {
        public class WorkItem
        {
            [Key]
            public int WorkItemID { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public decimal Cost { get; set; }
            public ICollection<BillOfMaterial> BillOfMaterials { get; set; }
        }
    }
