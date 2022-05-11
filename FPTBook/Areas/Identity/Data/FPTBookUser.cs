using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FPTBook.Models;
using Microsoft.AspNetCore.Identity;

namespace FPTBook.Areas.Identity.Data;

// Add profile data for application users by adding properties to the FPTBookUser class
public class FPTBookUser : IdentityUser
{
    public DateTime? DoB { get; set; }
    public string? Address { get; set; }
    public Store? Store { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual ICollection<Cart>? Carts { get; set; }

}


