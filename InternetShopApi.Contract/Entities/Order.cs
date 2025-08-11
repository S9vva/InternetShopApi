
using System.ComponentModel.DataAnnotations;


namespace InternetShopApi.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        public string CustomerId {  get; set; }

        [Required]
        public Customer Customer { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
