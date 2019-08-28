using DK.Dal.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.Dal.Mappings
{
    public class TeacherMap : ClassMapping<Teacher>
    {
        public TeacherMap()
        {
            Table("teacher");

            Id(p => p.Id, m => m.Generator(NHibernate.Mapping.ByCode.Generators.Identity));
            Property(x => x.FirstName, m => { m.Length(45); });
            Property(x => x.LastName, m => { m.Length(45); });
            Property(x => x.Birthday);
            Property(x => x.Hired);
            Property(x => x.CreatedOn);

            Bag(x => x.Lessons, mapping => 
            {
                mapping.Inverse(true);
                mapping.Cascade(Cascade.All);
                mapping.Key(k => k.Column("TeacherId"));
            }, 
            action => action.OneToMany());

            Bag(x => x.Clients, mapping =>
            {
                mapping.Table("TeacherClients");
                mapping.Cascade(Cascade.None);
                mapping.Key(k => k.Column("TeacherId"));
            },
            action => action.ManyToMany(p => p.Column("ClientId")));
        }
    }
}
