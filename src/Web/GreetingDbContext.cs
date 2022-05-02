using Microsoft.EntityFrameworkCore;

public class GreetingDbContext : DbContext
{
    public GreetingDbContext(DbContextOptions<GreetingDbContext> options)
        : base(options) { }

    public DbSet<Greeting> Greetings => Set<Greeting>();
}
