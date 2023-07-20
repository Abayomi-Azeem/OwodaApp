using System.ComponentModel.DataAnnotations;

namespace OwodaApp.Models
{
    public class Member
    {

        public Member()
        {
            Vehicles = new HashSet<Vehicle>();
            Payments = new HashSet<Payment>();
        }

        [Key]
        public int MemberId { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(50, MinimumLength =5, ErrorMessage ="Enter a name between 5 and 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Enter a name between 5 and 50 characters")]
        public string LastName { get; set; }

        [Required]
        [Range(18,65, ErrorMessage ="You must be between 18 and 65 to register")]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RegDate { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }


    }
}
