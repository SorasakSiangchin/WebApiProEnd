using Microsoft.EntityFrameworkCore;
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
            //optionsBuilder.UseSqlServer("Server=10.103.0.16,1433;Database=project-end-api;User Id=student;Password=Cs@2700;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=false;");
            optionsBuilder.UseSqlServer("Server=DESKTOP-6TJ4MKL;Database=project-end-api;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            //optionsBuilder.UseSqlite("Data Source=project-end-api.db");
            //optionsBuilder.UseSqlServer("Server=10.103.0.15,1433;Database=project-end-api;User Id=student;Password=Stu@2600;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        public DbSet<LevelProduct> LevelProducts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
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

            builder.Entity<Account>()
               .HasData(
                   new Account { Id = "account01", 
                       FirstName = "ArmCustomer" , 
                       LastName = "Kung" ,  
                       Email = "XxArmCustomerxX@gmail.com",
                       ImageUrl = "https://cv.lnwfile.com/_/cv/_raw/rn/s7/3p.png",
                       LoginBy = "" ,
                       Password = "Mw52so+8rK3NMGo/MDb85w==.7Rxql67dW+VxjYSja2uusRgJy860Q0KKTD0RyA9ju8Q=" ,
                       PhoneNumber = "0616032203" ,
                       Status = true ,
                       RoleID = 1 ,
                   } ,
                    new Account
                    {
                        Id = "account02",
                        FirstName = "ArmSeller",
                        LastName = "Kung",
                        Email = "XxArmSellerxX@gmail.com",
                        ImageUrl = "https://cv.lnwfile.com/_/cv/_raw/rn/s7/3p.png",
                        LoginBy = "",
                        Password = "Mw52so+8rK3NMGo/MDb85w==.7Rxql67dW+VxjYSja2uusRgJy860Q0KKTD0RyA9ju8Q=",
                        PhoneNumber = "0616032203",
                        Status = true,
                        RoleID = 2,
                    },
                    new Account
                    {
                        Id = "account03",
                        FirstName = "ArmAdmin",
                        LastName = "Kung",
                        Email = "XxArmAdminxX@gmail.com",
                        ImageUrl = "https://cv.lnwfile.com/_/cv/_raw/rn/s7/3p.png",
                        LoginBy = "",
                        Password = "Mw52so+8rK3NMGo/MDb85w==.7Rxql67dW+VxjYSja2uusRgJy860Q0KKTD0RyA9ju8Q=",
                        PhoneNumber = "0616032203",
                        Status = true,
                        RoleID = 3,
                    }
               );

            builder.Entity<CategoryProduct>()
            .HasData(
                new CategoryProduct { Id = 2, Name = "มังคุด" },
                new CategoryProduct { Id = 3, Name = "ลำไย" },
                new CategoryProduct { Id = 4, Name = "ทุเรียน" },
                new CategoryProduct { Id = 5, Name = "เงาะ" },
                new CategoryProduct { Id = 6, Name = "มะม่วง" },
                new CategoryProduct { Id = 7, Name = "กล้วย" },
                new CategoryProduct { Id = 8, Name = "มะพร้าว" },
                new CategoryProduct { Id = 9, Name = "ขนุน" },
                new CategoryProduct { Id = 10, Name = "หมาก" },
                new CategoryProduct { Id = 11, Name = "มะละกอ" },
                new CategoryProduct { Id = 12, Name = "น้อยหน่า" },
                new CategoryProduct { Id = 13, Name = "ลองกอง" },
                new CategoryProduct { Id = 14, Name = "ส้ม" },
                new CategoryProduct { Id = 15, Name = "ชมพู่" },
                new CategoryProduct { Id = 16, Name = "ขนุน" },
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
                new WeightUnit { Id = 2, Name = "กิโลกรัม" },
                new WeightUnit { Id = 3, Name = "หวี" }
            );

            builder.Entity<LevelProduct>()
           .HasData(
               new LevelProduct { Id = 1, Level = "หาได้ทั่วไป" },
               new LevelProduct { Id = 2, Level = "ปานกลาง" },
               new LevelProduct { Id = 3, Level = "หายาก" }
           );

            builder.Entity<Product>()
               .HasData(
                   new Product
                   {
                       Id = "product01",
                       Name = "ส้มโอทับทิมสยาม",
                       AccountID = "account01",
                       CategoryProductID = 2 ,
                       Color = "เขียว" ,
                       Description = "อร่อย" ,
                       Created = DateTime.Now,
                       ImageUrl = "https://www.ตลาดกลางผลไม้.com/wp-content/uploads/2019/10/72414759_106730297406260_6103449987075538944_n.jpg",
                       LevelProductID = 1 ,
                       Price = 150 ,
                       Stock = 50 ,
                       Weight = 1 ,
                       WeightUnitID= 1 ,
                   } ,

                   new Product
                   {
                       Id = "product02",
                       Name = "กล้วยน้ำว้ามะลิอ่อง",
                       AccountID = "account01",
                       CategoryProductID = 2,
                       Color = "เขียว",
                       Description = "อร่อย",
                       Created = DateTime.Now,
                       ImageUrl = "https://scontent-sin6-4.xx.fbcdn.net/v/t39.30808-6/299863661_433735048781055_6878774558773361005_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=09cbfe&_nc_ohc=3DcFFDZIL-0AX_oGgNx&_nc_ht=scontent-sin6-4.xx&oh=00_AfDQhZa4WiqXQ5mbCx6057-jHaiFA7Llf5Caf9UuFfyl9w&oe=64EC700D",
                       LevelProductID = 1,
                       Price = 150,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },

                   new Product
                   {
                       Id = "product03",
                       Name = "เสาวรส",
                       AccountID = "account01",
                       CategoryProductID = 2,
                       Color = "เขียว",
                       Description = "อร่อย",
                       Created = DateTime.Now,
                       ImageUrl = "https://cdn.shortpixel.ai/spai/w_870+q_lossy+ret_img+to_webp/amprohealth.com/wp-content/uploads/2017/09/135-%E0%B9%80%E0%B8%AA%E0%B8%B2%E0%B8%A7%E0%B8%A3%E0%B8%AA.jpg",
                       LevelProductID = 1,
                       Price = 150,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },

                   new Product
                   {
                       Id = "product04",
                       Name = "มังคุดสด มาตรฐาน GAP",
                       AccountID = "account01",
                       CategoryProductID = 2,
                       Color = "ม่วง",
                       Description = "อร่อย",
                       Created = DateTime.Now,
                       ImageUrl = "https://ตลาดเกษตรกรออนไลน์.com/uploads/products/cover_6285c7898575f.jpg",
                       LevelProductID = 1,
                       Price = 250,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },

                   new Product
                   {
                       Id = "product05",
                       Name = "ทุเรียนหมอนทอง",
                       AccountID = "account01",
                       CategoryProductID = 2,
                       Color = "เหลืองทอง",
                       Description = "อร่อย",
                       Created = DateTime.Now,
                       ImageUrl = "https://dk8dmtco5sckz.cloudfront.net/wp-content/uploads/2023/03/AnyConv.com__15-8.webp",
                       LevelProductID = 1,
                       Price = 250,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },
                  
                   new Product
                   {
                       Id = "product06",
                       Name = "แก้วมังกร",
                       AccountID = "account01",
                       CategoryProductID = 3,
                       Color = "เหลืองทอง",
                       Description = "อร่อย",
                       Created = DateTime.Now,
                       ImageUrl = "https://www.ตลาดเกษตรกรออนไลน์.com/uploads/products/cover_64914f3b033fd.jpg",
                       LevelProductID = 1,
                       Price = 250,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },
                   new Product
                   {
                       Id = "product07",
                       Name = "ลำไยอีดอ",
                       AccountID = "account01",
                       CategoryProductID = 3,
                       Color = "เหลืองทอง",
                       Description = "หวานฉ่ำชื่นใจ ไม่แสบคอ เนื้อเยอะ เม็ดลีบ แห้งไม่แฉะ",
                       Created = DateTime.Now,
                       ImageUrl = "https://www.ตลาดเกษตรกรออนไลน์.com/uploads/products/cover_64914f3b033fd.jpg",
                       LevelProductID = 1,
                       Price = 250,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },
                   new Product
                   {
                       Id = "product08",
                       Name = "เสาวรส",
                       AccountID = "account03",
                       CategoryProductID = 999,
                       Color = "เลือดหมู",
                       Description = "หวานฉ่ำชื่นใจ ไม่แสบคอ เนื้อเยอะ เม็ดลีบ แห้งไม่แฉะ",
                       Created = DateTime.Now,
                       ImageUrl = "https://www.77kaoded.com/wp-content/uploads/2021/08/IMG_5061.jpg",
                       LevelProductID = 1,
                       Price = 250,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   },
                   new Product
                   {
                       Id = "product09",
                       Name = "เมล่อน",
                       AccountID = "account01",
                       CategoryProductID = 3,
                       Color = "เหลือง",
                       Description = "อร่อย",
                       Created = DateTime.Now,
                       ImageUrl = "https://www.ตลาดเกษตรกรออนไลน์.com/uploads/products/398.jpg",
                       LevelProductID = 1,
                       Price = 250,
                       Stock = 50,
                       Weight = 1,
                       WeightUnitID = 1,
                   } 

               );
        }
    }

}
