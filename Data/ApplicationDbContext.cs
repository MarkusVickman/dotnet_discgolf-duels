using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using discgolf_duels.Models;

namespace discgolf_duels.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }


    public DbSet<Competition> Competitions { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<Play> Plays { get; set; }

    public DbSet<Playing> Playing { get; set; }

    public DbSet<Registration> Registrations { get; set; }

    //public DbSet<Friend> Friends { get; set; }

}
