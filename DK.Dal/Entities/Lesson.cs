using DK.Dal.Entities.Base;
using DK.Dal.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.Dal.Entities
{
    public class Lesson : Entity
    {
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual LessonType Type { get; set; }

        public virtual int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
