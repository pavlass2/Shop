using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using System.Data;

namespace WebApp.Data
{    
    public class IdentityDbContext 
        : IdentityDbContext<ApplicationUser, ApplicationIdentityRole, int>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }
    }
}
