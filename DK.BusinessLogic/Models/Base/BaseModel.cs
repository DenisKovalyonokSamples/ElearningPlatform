using DK.BusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DK.BusinessLogic.Models.Base
{
    [DataContract]
    [KnownType(typeof(LessonModel))]
    [KnownType(typeof(TeacherModel))]
    [KnownType(typeof(ClientModel))]
    public class BaseModel
    { 
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime CreatedOn { get; set; }
    }
}