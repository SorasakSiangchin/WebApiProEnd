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
        public DbSet<ImageProduct> ImageProducts { get; set; }
        //สร้างข้อมูลเริ่มต้นให้กับ Role
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

                new CategoryProduct { Id = 2, Name = "category-01" },
                new CategoryProduct { Id = 3, Name = "category-02" },
                 new CategoryProduct { Id = 999, Name = "rare" }
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
