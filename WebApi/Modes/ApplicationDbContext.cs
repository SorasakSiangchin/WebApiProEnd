using Microsoft.EntityFrameworkCore;
using WebApi.Modes;
using WebApi.Modes.CartAggregate;

namespace WebApiProjectEnd.Modes
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
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
        public DbSet<ListOrder> ListOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
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
            builder.Entity<Account>()
             .HasData(
                 new Account { Id = "account-01", FirstName = "Sorasak", LastName = "Siangchin", Email = "Sorasak@gmail.com", ImageUrl = "", Password = "1233211213", PhoneNumber = "0616032203", RoleID = 1 },
                 new Account { Id = "account-02", FirstName = "Anirut", LastName = "Chairuen", Email = "Anirut@gmail.com", ImageUrl = "", Password = "4566544546", PhoneNumber = "0927680099", RoleID = 2 }
             );

            builder.Entity<CategoryProduct>()
            .HasData(

                new CategoryProduct { Id = 2, Name = "category-01", AccountID = "account-01" },
                new CategoryProduct { Id = 3, Name = "category-02", AccountID = "account-02" }
            );
            builder.Entity<WeightUnit>()
            .HasData(

                new WeightUnit { Id = 1, Name = "ลูก" },
                new WeightUnit { Id = 2, Name = "กิโลกรัม" }
            );
            builder.Entity<Product>()
            .HasData(
                new Product { Id = "product-01", Name = "Product01", Price = 100, Stock = 5, Color = "red", Weight = 20, Description = "", ImageUrl = "df339981-6e81-4b28-bbb9-bdcb194a05a3.jpg", Created = DateTime.Now, LastUpdate = null, WeightUnitID = 1, CategoryProductID = 2 },
                new Product { Id = "product-02", Name = "Product02", Price = 200, Stock = 6, Color = "green", Weight = 10, Description = "", ImageUrl = "d6667cbd-f010-43b8-95e0-bf3d8ff218bb.jpg", Created = DateTime.Now, LastUpdate = null, WeightUnitID = 2, CategoryProductID = 2 },
                new Product { Id = "product-03", Name = "Product03", Price = 300, Stock = 7, Color = "blue", Weight = 30, Description = "", ImageUrl = "d3c013ec-f736-4750-86a5-53b0c6136a9c.jpg", Created = DateTime.Now, LastUpdate = null, WeightUnitID = 1, CategoryProductID = 2 },
                new Product { Id = "product-04", Name = "Product04", Price = 400, Stock = 8, Color = "black", Weight = 40, Description = "", ImageUrl = "be242077-737c-48ae-935d-f0ba03ec7d25.jpg", Created = DateTime.Now, LastUpdate = null, WeightUnitID = 2, CategoryProductID = 2 }

            ); ;
        }
    }

}
