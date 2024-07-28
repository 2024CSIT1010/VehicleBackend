using Microsoft.EntityFrameworkCore;

namespace vehicleservicesystmapi.Models
    
{
    
       
    public class PrimeAutomobilesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRepresentative> ServiceRepresentatives { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<BillOfMaterial> BillOfMaterials { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public PrimeAutomobilesDbContext(DbContextOptions<PrimeAutomobilesDbContext> options) : base(options) { }
       
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships and other constraints here
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Vehicles)
                .WithOne(v => v.Customer)
                .HasForeignKey(v => v.CustomerID);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.ServiceRecords)
                .WithOne(sr => sr.Vehicle)
                .HasForeignKey(sr => sr.VehicleID);

            modelBuilder.Entity<ServiceRecord>()
                .HasOne(sr => sr.ServiceRepresentative)
                .WithMany()
                .HasForeignKey(sr => sr.ServiceRepresentativeID);

            modelBuilder.Entity<ServiceRecord>()
                .HasOne(sr => sr.Invoice)
                .WithOne(i => i.ServiceRecord)
                .HasForeignKey<Invoice>(i => i.ServiceRecordID);

            modelBuilder.Entity<ServiceRecord>()
                .HasMany(sr => sr.BillOfMaterials)
                .WithOne(bom => bom.ServiceRecord)
                .HasForeignKey(bom => bom.ServiceRecordID);

            modelBuilder.Entity<BillOfMaterial>()
                .HasOne(bom => bom.WorkItem)
                .WithMany()
                .HasForeignKey(bom => bom.WorkItemID);

        }
    }
}
