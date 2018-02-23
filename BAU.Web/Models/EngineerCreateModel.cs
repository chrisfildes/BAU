using System.ComponentModel.DataAnnotations;

namespace BAU.Website.Models
{
    /*  EngineerCreateModel

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation of model for creating an Engineer
    */

    public class EngineerCreateModel
    {
        [Required(ErrorMessage = "* required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }
    }
}