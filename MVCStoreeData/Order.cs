using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVCStoreData
{
    public enum OrderStatus
    { 
       New,Shipped,Cancelled,Void
    }
    public class Order:EntityBase
    {
        public string? DeliveryCode { get; set; }
        public OrderStatus Status { get; set; }
        public Guid  ApplicationUserId { get; set; }
        public Guid DeliveryAddressId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } 
        
        public virtual Address DeliveryAdress { get; set; } // bir siparişin bir tane teslimat adresi olabilir.
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .HasMany(p => p.OrderItems)
               .WithOne(p => p.Order)
               .HasForeignKey(p => p.OrderId) // DEFAULT OLARAK BURAYI ALIYOR ONUN YERİNE ORDER_ID  ALIYOR
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
    