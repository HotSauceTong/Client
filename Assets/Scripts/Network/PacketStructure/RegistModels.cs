
namespace Network.PacketStructure
{
    public class RegistRequest
    {
        public string email;
        public string nickname;
        public string password;
        public string version;
    }

    public class RegistResponse : BaseResponse
    {
    }
}
