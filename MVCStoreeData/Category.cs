using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace MVCStoreData
{
    public class Category:EntityBase
    {
        [Display(Name = "Reyon")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]

        public Guid RayonId { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
        [DataType(DataType.Text)] 
        public string Name { get; set; }
        public virtual Rayon Rayon { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();// many to many bağlantı aşağıya builder yapmamıza gerek kalmadı

    }
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
       public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(50); 
                
            builder
                .HasIndex(p => new { p.Name, p.RayonId }) // name alanının category ismiyle aynı olmaması lazım
                .IsUnique();
            

        }
    }
}
    