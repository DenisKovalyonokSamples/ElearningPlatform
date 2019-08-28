using System.Runtime.Serialization;

namespace DK.BusinessLogic.Enums
{
    [DataContract]
    public enum ResponseType : int
    {
        [EnumMember]
        Unknown = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        Error = 2
    }
}
