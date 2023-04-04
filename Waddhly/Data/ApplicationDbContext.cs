using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Waddhly.Models;
using Waddhly.Models.Community;
using Waddhly.Models.UserServices;

namespace Waddhly.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Comment> Comments{ get; set; }
        public DbSet<RoomMessage> RoomMessages { get; set; }
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Certificate> Certificates{ get; set; }
        public DbSet<Portfolio> Portfolios{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Proposal> Proposals{ get; set; }
        public DbSet<Service> Services{ get; set; }
        public DbSet<Session> Sessions{ get; set; }

    }
}
