using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDomain.Common
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }

    }
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

    }
}
