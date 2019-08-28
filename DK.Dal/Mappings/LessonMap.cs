using DK.Dal.Entities;
using DK.Dal.Enums;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.Dal.Mappings
{
    public class LessonMap : ClassMapping<Lesson>
    {
        public LessonMap()
        {
            Table("lesson");

            Id(p => p.Id, m => m.Generator(NHibernate.Mapping.ByCode.Generators.Identity));
            Property(x => x.Title, m => { m.Length(255); });
            Property(x => x.Description, m => { m.Length(800); });
            Property(x => x.CreatedOn);
            Property(x => x.Type);

            ManyToOne(x => x.Teacher, mappping =>
            {
                mappping.Column("TeacherId");
            });
        }
    }
}
