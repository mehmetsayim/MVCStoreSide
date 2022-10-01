using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCStoreData
{
    public enum Genders
    { 
       Male,Female
    }
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string Name { get; set; }
        public Genders?  Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }=new HashSet<Order>();
        public virtual ICollection<Comment> Comments { get; set; }=new HashSet<Comment>();
        
        public virtual ICollection<ShoppingCartItem> ShoppingCartİtems { get; set; } = new HashSet<ShoppingCartItem>();

    }
    public class ApplicationUserIdentityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
           builder
                .HasMany(p=> p.Comments)
                .WithOne(p=>p.ApplicationUser)
                .HasForeignKey(p=>p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(p => p.ShoppingCartİtems) // bir kullanıcının birden fazla shoppingcar -t item olabilir
                .WithOne(p => p.ApplicationUser) // bir shoping cart item bir kullanıcıya ait olabilir
                .HasForeignKey(p => p.ApplicationUserId) // applicationuserıD DEN BAĞLIDIR
                .OnDelete(DeleteBehavior.Cascade); // eğer kullanıcı silinirse sepetindeki ürünlerde otomatik silinir.
            builder
                .HasMany(p => p.Orders)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
