using System.ComponentModel.DataAnnotations;

namespace INMAR.Service.Models
{
    public class Users
    {
        [Key]
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
