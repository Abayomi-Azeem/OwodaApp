using System.ComponentModel.DataAnnotations;

namespace OwodaApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public int Amount { get; set; }

        [Display(Name ="Payment Date")]
        [Required]
        public DateTime PaymentDate { get; set; }

        public virtual Member? Member { get; set; }

        public virtual Vehicle? Vehicle { get; set; }
    }
}
