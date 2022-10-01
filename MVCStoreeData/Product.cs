using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCStoreData
{
    public class Product:EntityBase
    {
        
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Descriptions { get; set; }
        public decimal? DiscountRate { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<ShoppingCartItem> ShoppingCartİtems { get; set; } = new HashSet<ShoppingCartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }   
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
       public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(50);
            builder
                 .HasMany(p => p.ProductImages)
                 .WithOne(p => p.Product)
                 .HasForeignKey(p => p.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(p => p.Comments)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder 
                .HasMany(p => p.ShoppingCartİtems)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
               .HasMany(p => p.OrderItems)
               .WithOne(p => p.Product)
               .HasForeignKey(p => p.ProductId)
               .OnDelete(DeleteBehavior.Restrict); // Bir ürünü daha önce sildiysen satamazsın kardeşim...
            builder
                .Property(p => p.Price)
                .HasPrecision(18, 4);
           

        }
    }
}
    