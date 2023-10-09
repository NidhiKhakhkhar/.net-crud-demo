using System.ComponentModel.DataAnnotations;

namespace demo.Areas.Branch.Models
{
    public class MST_Branch
    {
        public int? BranchID { get; set; }

        [Required]
        public string? BranchName { get; set; }

        [Required]
        public string? BranchCode { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }

    public class MST_BranchDropdownModel
    {
        public int? BranchID { get; set; }

        public string? BranchName { get; set; }
    }
}
