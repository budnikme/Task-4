using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Task4.Areas.Identity.Data;

public class ApplicationUser : IdentityUser<int>
{
    public string Name { get; set; } = string.Empty;
    public DateTime RegistrationTime { get; set; } 
    public DateTime LastLoginTime { get; set; }
    
}

