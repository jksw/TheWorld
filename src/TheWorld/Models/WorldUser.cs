using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldUser: IdentityUser
    {
    public DateTime FirstTrip { get; set; }

    //See Wildermuth's module Storing Identities in the Database
    // right click on project, way down to bottom find Open Command Line / Default

    // what you're doing next after adding identity: you are needing to get the objects
    // that now present in model, into the database.  The class WorldUser inherits from
    // IdentifyUser, so need to get IdentityUser's fields out to database.

    /*  at command prompt, cd.. up to be in TheWorld directory.  then
     *  dotnet ef migrations add AddingIdentity
     *  
     *  look at Migrations folder -- lots of database structure added */
  }
}
