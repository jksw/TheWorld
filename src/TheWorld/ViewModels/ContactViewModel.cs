using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class ContactViewModel
    {
    
    // Purpose of view model is to separate the Model class data (internal use, none of the user's business) from the ViewModel class data (
    // what we choose to expose to user).
    
    //Quoting Shawn W: 
    //Using this class lets us do model binding, meaning: lets us accept data from from
    // directly inside of a method of the contoller


    //Validation attributes won't work until you edit http://json.schemastore.org/bower and
    //add jquery-validation and jquery-validation-unobtrsive;

    //then, go back to the Contact page view, and in proj explorer, navigate to wwwroot/lib/jquery-validation/dist;
    //click and hold on jquery.validatate.min.js and drag it top top of contact.cshtml page

    [Required]  
    public string Name{ get; set; }

    [Required]
    [EmailAddress]
    public string Email{ get; set; }

    [Required]
    [StringLength(4096,MinimumLength =10)]
    public string Message{ get; set; }

  }
}
