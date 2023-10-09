using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace demo.Areas.Student.Models
{
    public class MST_Student
    {
        [Required]
        public int? StudentID { get; set; }

        [Required]
        public string? StudentName { get; set;}


        [Required]
        public int? BranchID { get; set; }


        [Required]
        public int? CityID { get; set; }

        [Required]
        public string? MobileNoStudent { get; set; }

        public string? MobileNoFather { get; set; }


        [Required]
        public string? Email { get; set; }

        public string? Address { get; set; }


        [Required]            
        public DateTime? BirthDate { get; set; }

        public int? Age { get; set; }


        [Required]
        public bool? IsActive { get; set; }


        [Required]
        public string? Gender { get; set; }


        [Required]
        public string? Password { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get ; set; }



    }
}
