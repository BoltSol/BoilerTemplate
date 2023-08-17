using AppDomain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Common
{
    public class AuditEntity : IEntity<Guid>
    {
        public AuditEntity()
        {
            CreationDateTime = DateTime.Now;
            IsDeleted = false;
            CreatedUser = new User();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public DateTime CreationDateTime { get; set; }
        public long? CreatedUserId { get; set; }
        [ForeignKey("CreatedUserId")]
        public User CreatedUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
