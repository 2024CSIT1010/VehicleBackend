using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicleservicesystmapi.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }
        [Required]
        public int ServiceRecordID { get; set; }
        [ForeignKey("ServiceRecordID")]
        public ServiceRecord ServiceRecord { get; set; }
        [Required]
        public decimal TotalCost { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
    }
}
