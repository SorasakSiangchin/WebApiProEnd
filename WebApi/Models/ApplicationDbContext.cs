using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.CartAggregate;

namespace WebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=tee.kru.ac.th;Database=project-end-api;User Id=student;Password=Cs@2700;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer("Server=10.103.0.16,1433;Database=project-end-api;User Id=student;Password=Cs@2700;MultipleActiveResultSets=true;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer("Server=DESKTOP-6TJ4MKL;Database=project-end-api;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //optionsBuilder.UseSqlite("Data Source=project-end-api.db");
        }

        public DbSet<LevelProduct> LevelProducts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountPassword> AccountPasswords { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressInformation> AddressInformations { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Delivery> Deliverys { get; set; }
        public DbSet<DetailProduct> DetailProducts { get; set; }
        public DbSet<EvidenceMoneyTransfer> EvidenceMoneyTransfers { get; set; }
        public DbSet<ImageReview> ImageReviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<StatusDelivery> StatusDeliverys { get; set; }
        public DbSet<WeightUnit> WeightUnits { get; set; }
        public DbSet<OrderMessage> OrderMessages { get; set; }
        public DbSet<ImageProduct> ImageProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>()
               .HasData(
                   new Role { Id = 1, Name = "customer" },
                   new Role { Id = 2, Name = "seller" },
                   new Role { Id = 3, Name = "admin" }
               );

            builder.Entity<CategoryProduct>()
            .HasData(
                new CategoryProduct { Id = 2, Name = "มังคุด" },
                new CategoryProduct { Id = 3, Name = "ลำไย" },
                new CategoryProduct { Id = 4, Name = "ทุเรียน" },
                new CategoryProduct { Id = 5, Name = "เงาะ" },
                new CategoryProduct { Id = 6, Name = "มะม่วง" },
                new CategoryProduct { Id = 7, Name = "กล้วย" },
                new CategoryProduct { Id = 999, Name = "หายาก" }
            );
            builder.Entity<StatusDelivery>()
            .HasData(
                new StatusDelivery { Id = 1, Name = "กำลังเตรียมพัสดุ" },
                new StatusDelivery { Id = 2, Name = "บริษัทขนส่งเข้ารับพัสดุเรียบร้อยแล้ว" },
                new StatusDelivery { Id = 3, Name = "พัสดุถึงศูนย์คัดแยกสินค้า" },
                new StatusDelivery { Id = 4, Name = "พัสดุออกจากศูนย์คัดแยกสินค้า" },
                new StatusDelivery { Id = 5, Name = "พัสดุถึงสาขาปลายทาง" },
                new StatusDelivery { Id = 999, Name = "การจัดส่งสำเร็จ" }
               );
            builder.Entity<WeightUnit>()
            .HasData(
                new WeightUnit { Id = 1, Name = "ลูก" },
                new WeightUnit { Id = 2, Name = "กิโลกรัม" }
            );
            builder.Entity<LevelProduct>()
           .HasData(
               new LevelProduct { Id = 1, Level = "หาได้ทั่วไป" },
               new LevelProduct { Id = 2, Level = "ปานกลาง" },
               new LevelProduct { Id = 3, Level = "หายาก" }
           );
        }
    }

}
