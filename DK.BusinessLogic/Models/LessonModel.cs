using DK.BusinessLogic.Models.Base;
using DK.Dal.Enums;
using System.Runtime.Serialization;

namespace DK.BusinessLogic.Models
{
    [DataContract]
    public class LessonModel : BaseModel
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public LessonType Type { get; set; }

        [DataMember]
        public TeacherModel Teacher { get; set; }
    }
}