using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace demo.Areas.State.Models
{
    public class LOC_State
    {
        public int? StateID { get; set; }

        [Required]
        [DisplayName("State Name")]
        public string? StateName { get; set; }

        [Required]
        [DisplayName("State Code")]
        public string? StateCode { get; set;}

        [Required]
        [DisplayName("Country ID")]
        public int? CountryID { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }  
    }

    public class LOC_StateDropdownModel
    {
        public int? StateID { get; set; }

        [Required]
        public string? StateName { get; set;}
    }
}
