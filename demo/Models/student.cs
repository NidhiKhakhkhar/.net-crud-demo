using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    public class student
    {
        [Required]
       
        public int EnrollmentNO { get; set; }

        [Required(ErrorMessage ="This filed is required")]

        public string? FirstName { get; set; }

        [Required]

        public string? LastName { get; set; }
        [Required]
        public string? City { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public int Age { get; set; }




        


    }
}
