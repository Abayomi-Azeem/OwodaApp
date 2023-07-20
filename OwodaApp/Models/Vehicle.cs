using System.ComponentModel.DataAnnotations;


namespace OwodaApp.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
            Payments = new HashSet<Payment>();
        }

        [Key]
        public int VehicleId { get; set; }

        [Required]
        [Display(Name ="Vehicle Type")]
        public string VehicleType { get; set; }

        [Required]
        [Display(Name ="Vehicle Make")]
        public string VehicleMake { get; set; }

        [Display(Name ="Vehicle Model")]
        [Required]
        [RegularExpression("[0-9]{4}", ErrorMessage ="Enter a Valid Model Year")]
        public int VehicleModel { get; set; }

        [Display(Name ="Vehicle Colour")]
        public string? VehicleColor { get; set; }

        [Required]
        [Display(Name ="Complete Papers?")]
        public bool IsPapersComplete { get; set; }

        [Required]
        [Display(Name ="Owner ID")]
        public int MemberId { get; set; }

        public virtual Member? Member { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
       
    }
}
