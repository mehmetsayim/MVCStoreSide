    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCStoreData
{
    public class ShoppingCartItem:EntityBase
    {
        public Guid ProductId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public virtual Product? Product { get; set; } // shopping cartı gösterirken bu ürünün fiyatına erişebilmek içi kullanıyoruz

        public int Quantity { get; set; }
        public virtual ApplicationUser?  ApplicationUser { get; set; }
         

    }
    public class ShoppingCartEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
        }
    }
}
    