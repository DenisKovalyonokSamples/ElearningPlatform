using DK.Dal.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.Dal.Entities
{
    public class Teacher : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual DateTime Hired { get; set; }

        public virtual IList<Lesson> Lessons { get; set; }
        public virtual IList<Client> Clients { get; set; }
    }
}
