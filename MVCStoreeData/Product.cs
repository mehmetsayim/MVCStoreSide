using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using System.Xml.Linq;

namespace MVCStoreData
{
    public class Product:EntityBase
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage ="{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Açıklamalar")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.MultilineText)]
        public string? Descriptions { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        [NotMapped]
        [Display(Name = "Liste Fiyat")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Currency)]
        [RegularExpression("^[0-9]+(,[0-9]+)?$", ErrorMessage ="Lütfen geçerli bir fiyat yazınız!")]
        public string PriceText { get; set; }

        [NotMapped] 
        [Display(Name = "İndirimli Fiyat")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Currency)]
        [RegularExpression("^[0-9]+(,[0-9]+)?$", ErrorMessage = "Lütfen geçerli bir fiyat yazınız!")]
        public decimal DiscountedPriceText { get; set; }

        [NotMapped]
        public IEnumerable<IFormFile> Images { get; set; }
        [NotMapped]
        [Display(Name = "Kategori(ler)")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        public IEnumerable<Guid> CategoryIds { get; set; }

        [NotMapped]
        public int DiscountRate => (int)Math.Round((Price-(DiscountedPrice ?? Price)) * 100 / Price);    
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
    