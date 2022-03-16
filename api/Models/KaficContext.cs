using Microsoft.EntityFrameworkCore;
namespace api.Models
{
    public class KaficContext : DbContext
    {
        public DbSet<Kafic> Kafici { get; set; }
        public DbSet<Sto> Stolovi { get; set; }
        public DbSet<Stolica> Stolice { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Porudzbenica> spojPorudzbine { get; set; }
        public KaficContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Sto>()
            .HasMany(s => s.stolice).WithOne(p=>p.sto)
            .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Stolica>()
            .HasMany(s => s.porudzbine).WithOne(p => p.stolica)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
