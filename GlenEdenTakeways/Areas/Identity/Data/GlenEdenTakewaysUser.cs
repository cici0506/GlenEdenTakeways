using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GlenEdenTakeways.Areas.Identity.Data;

// Add profile data for application users by adding properties to the GlenEdenTakewaysUser class
public class GlenEdenTakewaysUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

