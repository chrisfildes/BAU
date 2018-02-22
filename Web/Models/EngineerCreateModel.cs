using System.ComponentModel.DataAnnotations;

namespace BAU.Website.Models
{
    public class EngineerCreateModel
    {
        [Required(ErrorMessage = "* required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* required")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Display(Name ="Is Available?")]
        public bool IsAvailable { get; set; }
    }

    public class EngineerEditModel : EngineerCreateModel
    {
        public int ID { get; set; }
    }
}