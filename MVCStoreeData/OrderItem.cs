using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCStoreData
{
    public class OrderItem:EntityBase
    {
        public Guid OrderId { get; set; }

        public Guid Id { get; set; }
        
        public Guid ProductId { get; set; }
        public decimal   Price { get; set; }
        public int   Quantity  { get; set; }
        public decimal   DiscountRate { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            
        }
    }
}
    