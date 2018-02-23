using System.ComponentModel.DataAnnotations;

namespace BAU.Website.Models
{
    /*  EngineerEditModel

        Author: Chris Fildes
        Date: 22/02/2018
        Description: Implementation of model for creating an Engineer
    */

    public class EngineerEditModel : EngineerCreateModel
    {
        public int ID { get; set; }
    }
}