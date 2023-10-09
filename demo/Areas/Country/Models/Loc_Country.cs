using System.ComponentModel.DataAnnotations;

namespace demo.Areas.Country.Models
{
    public class Loc_Country
    {

        public int? CountryID { get; set; }

        //[Required]
        public string? CountryName { get; set; }

        //[Required]
        public string? CountryCode { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }

    public class LOC_CountryDropdownModel
    {
        public int? CountryID { get; set; }

        [Required]
        public string? CountryName { get; set;}
    }
}
