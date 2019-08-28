using System;

namespace DK.Dal.Entities.Base
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}
