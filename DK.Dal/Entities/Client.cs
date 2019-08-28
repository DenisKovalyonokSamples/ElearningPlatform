using DK.Dal.Entities.Base;
using System;
using System.Collections.Generic;

namespace DK.Dal.Entities
{
    public class Client : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime Birthday { get; set; }

        public virtual IList<Teacher> Teachers { get; set; }
    }
}
