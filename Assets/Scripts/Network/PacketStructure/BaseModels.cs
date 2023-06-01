
namespace Network.PacketStructure
{
    public class BaseRequest
    {
        public string email;
        public string token;
        public string version;
    }
    
    public class BaseResponse
    {
        public ErrorCode errorCode;
    }
}


