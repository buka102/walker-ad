using Microsoft.EntityFrameworkCore;
using Wa.Application.Entities;

namespace Wa.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Lead> Leads { get; set; }
    }
}

public class InMemoryLeadRepository
{
    private static readonly List<Lead> Leads = new();
    private static int _idCounter = 1;

    public List<Lead> GetAll() => Leads;

    public Lead Add(Lead lead)
    {
        lead.Id = _idCounter++;
        Leads.Add(lead);
        return lead;
    }
}
