
using System;

namespace Network.PacketStructure
{
    public class AttendanceRequest : BaseRequest
    {
    }

    public class AttendanceResponse : BaseResponse
    {
        public Int16 consecutiveLoginCount { get; set; }
        public DateTime lastLoginDate { get; set; }
    }
}