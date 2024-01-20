using System.ComponentModel.DataAnnotations;

namespace INMAR.Service.Models
{
    public class CartItem
    {
        [Key]
        public long CartItemId { get; set; }
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsActive { get; set; }

    }
}
