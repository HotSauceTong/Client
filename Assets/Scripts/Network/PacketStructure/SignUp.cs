
using System;

namespace Network.PacketStructure
{
    public struct SignUpRequest
    {
        public string email;
        public string nickname;
        public string password;
        public string version;

        public SignUpRequest(string email, string nickname, string password, string version)
        {
            this.email = email;
            this.nickname = nickname;
            this.password = password;
            this.version = version;
        }
    }

    public struct SignUpResponse
    {
        public Int16 errorCode;
    }
}


