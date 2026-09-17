using OnlineStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace OnlineStoreAPI.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Address> Addresses{get; set;}
    public DbSet<Cart> Carts{get; set;}
    public DbSet<CartItem> CartItems{get; set;}
    public DbSet<Category> Categories{get; set;}
    public DbSet<Order> Orders{get; set;}
    public DbSet<OrderItem> OrderItems{get; set;}
    public DbSet<Payment> Payments{get; set;}
    public DbSet<Product> Products{get; set;}
    public DbSet<ProductImage> ProductImages{get; set;}
    public DbSet<Review> Reviews{get; set;}
    public DbSet<Role> Roles{get; set;}
    public DbSet<User> Users{get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .HasOne(p=>p.Category)
        .WithMany(c=>c.Products)
        .HasForeignKey(p=>p.CategoryId);

        modelBuilder.Entity<User>()
        .HasOne(u=>u.Role)
        .WithMany(r=>r.Users)
        .HasForeignKey(u=>u.RoleId);

        modelBuilder.Entity<Address>()
        .HasOne(a=>a.User)
        .WithMany(u=>u.Addresses)
        .HasForeignKey(a=>a.UserId);

        modelBuilder.Entity<ProductImage>()
        .HasOne(pi=>pi.Product)
        .WithMany(p=>p.Images)
        .HasForeignKey(pi=>pi.ProductId);

        modelBuilder.Entity<CartItem>()
        .HasOne(ci=>ci.Cart)
        .WithMany(c=>c.Items)
        .HasForeignKey(ci=>ci.CartId);

        modelBuilder.Entity<CartItem>()
        .HasOne(ci=>ci.Product)
        .WithMany(p=>p.CartItems)
        .HasForeignKey(ci=>ci.ProductId);

        modelBuilder.Entity<OrderItem>()
        .HasOne(oi=>oi.Order)
        .WithMany(o=>o.Items)
        .HasForeignKey(oi=>oi.OrderId);

        modelBuilder.Entity<OrderItem>()
        .HasOne(oi=>oi.Product)
        .WithMany(p=>p.OrderItems)
        .HasForeignKey(oi=>oi.ProductId);

        modelBuilder.Entity<Review>()
        .HasOne(r=>r.User)
        .WithMany(u=>u.Reviews)
        .HasForeignKey(r=>r.UserId);

        modelBuilder.Entity<Review>()
        .HasOne(r=>r.Product)
        .WithMany(p=>p.Reviews)
        .HasForeignKey(r=>r.ProductId);

        modelBuilder.Entity<Order>()
        .HasOne(o=>o.User)
        .WithMany(u=>u.Orders)
        .HasForeignKey(o=>o.UserId);

        modelBuilder.Entity<Payment>()
        .HasOne(p=>p.Order)
        .WithOne(o=>o.Payment)
        .HasForeignKey<Payment>(p=>p.OrderId);

        modelBuilder.Entity<Cart>()
        .HasOne(c=>c.User)
        .WithOne(u=>u.Cart)
        .HasForeignKey<Cart>(c=>c.UserId);

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id=1,
                Name="Admin"
            },
            new Role
            {
                Id=2,
                Name="Customer"
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id=1,
                Name="Electronics",
                Description="Electronic devices and accessories."
            },
            new Category
            {
                Id=2,
                Name="Books",
                Description="Books and educational materials."
            },
            new Category
            {
                Id=3,
                Name="Clothing",
                Description="Clotes and fashion products."
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id=1,
                Title="Gaming Laptop",
                Price=1200,
                CategoryId=1
            },
            new Product
            {
                Id=2,
                Title="Wireless Headphones",
                Price=150,
                CategoryId=1
            },
            new Product
            {
                Id=3,
                Title="Clean Code",
                Price=45,
                CategoryId=2
            },
            new Product
            {
                Id=4,
                Title="Classic T-Shirt",
                Price=30,
                CategoryId=3
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id=1,
                FirstName="Sohrab",
                LastName="Ahmadi",
                Email="Sohrab@gmail.com",
                Password="123456",
                RoleId=2
            },
            new User
            {
                Id=2,
                FirstName="Sara",
                LastName="Jahani",
                Email="Sara@yahoo.com",
                Password="admin123",
                RoleId=1
            }
        );

        modelBuilder.Entity<Cart>().HasData(
            new Cart
            {
                Id=1,
                UserId=1
            },
            new Cart
            {
                Id=2,
                UserId=2
            }
        );

        modelBuilder.Entity<CartItem>().HasData(
            new CartItem
            {
                Id=1,
                ProductId=1,
                CartId=1,
                Quantity=1
            },
            new CartItem
            {
                Id=2,
                ProductId=2,
                CartId=1,
                Quantity=3
            }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id=1,
                OrderDate=new DateTime(2026,9,3),
                UserId=1
            }
        );

        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem
            {
                Id=1,
                OrderId=1,
                ProductId=1,
                Quantity=1,
                UnitPrice=1200
            },
            new OrderItem
            {
                Id=2,
                OrderId=1,
                ProductId=2,
                Quantity=3,
                UnitPrice=150
            }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                Id=1,
                City="Tehran",
                Street="Valiasr Street",
                PostalCode="1234567890",
                UserId=1
            },
            new Address
            {
                Id=2,
                City="Tehran",
                Street="Azadi Street",
                PostalCode="0987654321",
                UserId=1
            }
        );

        modelBuilder.Entity<Payment>().HasData(
            new Payment
            {
                Id=1,
                Amount=1650,
                PaymentDate=new DateTime(2026,9,3),
                Status="Paid",
                OrderId=1
            }
        );

        modelBuilder.Entity<ProductImage>().HasData(
            new ProductImage
            {
                Id=1,
                ImageUrl="images/laptop-1.jpg",
                ProductId=1
            },
            new ProductImage
            {
                Id=2,
                ImageUrl="images/laptop-2.jpg",
                ProductId=1
            },
            new ProductImage
            {
                Id=3,
                ImageUrl="images/headphones-1.jpg",
                ProductId=2
            },
            new ProductImage
            {
                Id=4,
                ImageUrl="image/clean-code-1.jpg",
                ProductId=3
            },
            new ProductImage
            {
                Id=5,
                ImageUrl="images/tshirt-1.jpg",
                ProductId=4
            }
        );

        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                Id=1,
                Rating=5,
                Comment="Great laptop,very powerful and fast.",
                CreatedAt=new DateTime(2026,9,3),
                UserId=1,
                ProductId=1
            },
            new Review
            {
                Id=2,
                Rating=4,
                Comment="The headphones have good sound quality.",
                CreatedAt=new DateTime(2026,9,3),
                UserId=1,
                ProductId=2
            }
        );

        modelBuilder.Entity<User>()
        .Property(u=>u.FirstName)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<User>()
        .Property(u=>u.LastName)
        .HasMaxLength(50)
        .IsRequired();

        modelBuilder.Entity<User>()
        .Property(u=>u.Email)
        .HasMaxLength(100)
        .IsRequired();

        modelBuilder.Entity<User>()
        .HasIndex(u=>u.Email)
        .IsUnique();

        modelBuilder.Entity<Product>()
        .Property(p=>p.Title)
        .HasMaxLength(200)
        .IsRequired();

        modelBuilder.Entity<Product>()
        .Property(p=>p.Price)
        .HasPrecision(18,2);

        modelBuilder.Entity<Review>()
        .ToTable(r=>r.HasCheckConstraint(
            "CK_Review_Rating",
            "Rating >= 1 AND Rating<= 5"
        ));

        modelBuilder.Entity<CartItem>()
        .ToTable(ci=>ci.HasCheckConstraint(
            "CK_CartItem_Quantity",
            "Quantity > 0"
        ));

        modelBuilder.Entity<OrderItem>()
        .ToTable(oi=>oi.HasCheckConstraint(
            "CK_OrderItem_Quantity",
            "Quantity > 0"
        ));

    }
}