
namespace Network.PacketStructure
{
    public class LoginRequest
    {
        public string email;
        public string password;
        public string version;
    }

    public class LoginResponse : BaseResponse
    {
        public string token;
    }
}
