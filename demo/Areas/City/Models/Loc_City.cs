using System.ComponentModel.DataAnnotations;

namespace demo.Areas.City.Models
{
    public class Loc_City
    {
       public int? CityID { get; set; }

        [Required]
        public string? CityName { get; set; }

        [Required]
        public string? CityCode { get; set; }

        [Required]
        public int? StateID { get; set; }

        [Required]
        public int? CountryID { get; set; } 

        public DateTime Created {get; set; }

        public DateTime Modified { get; set; }
    }

    public class LOC_CityDropdownModel
    {
        public int? CityID { get; set; }

        public string? CityName { get; set; }
    }
}
