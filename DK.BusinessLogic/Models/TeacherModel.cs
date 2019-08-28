using DK.BusinessLogic.Attributes;
using DK.BusinessLogic.Models.Base;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DK.BusinessLogic.Models
{
    [DataContract]
    public class TeacherModel : BaseModel
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime Birthday { get; set; }

        [DataMember]
        [DateTimeKind(DateTimeKind.Utc)]
        public virtual DateTime Hired { get; set; }
    }
}