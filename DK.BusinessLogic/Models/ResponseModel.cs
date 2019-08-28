using DK.BusinessLogic.Enums;
using System.Runtime.Serialization;

namespace DK.BusinessLogic.Models
{
    [DataContract]
    public class ResponseModel
    {
        [DataMember]
        public ResponseType Result { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
