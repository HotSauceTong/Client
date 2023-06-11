
using General;
using UnityEngine;

namespace Network.PacketStructure
{
    public class BaseRequest
    {
        public string email;
        public string token;
        public string version;

        public void InitBase()
        {
            token = PlayerPrefs.GetString("Token");
            email = PlayerPrefs.GetString("EmailID");
            version = NetworkValues.ClientVersion;
        }
    }
    
    public class BaseResponse
    {
        public ErrorCode errorCode;
    }
}


