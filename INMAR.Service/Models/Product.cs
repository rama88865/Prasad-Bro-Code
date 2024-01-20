using System.ComponentModel.DataAnnotations;

namespace INMAR.Service.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal? Price { get; set; }
        public bool? InStock { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
