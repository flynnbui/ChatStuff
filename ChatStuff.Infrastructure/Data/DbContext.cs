using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChatStuff.Core.Entities; 

namespace ChatStuff.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ChatStuffUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}
