
using System;

namespace General
{
    public static class NetworkValues
    {
        public const string ClientVersion = "1.0.0";
        public const string Url = "http://52.197.242.93/";
    }

    public static class RexValues
    {
        public const string EmailIdRex =
            @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
        public const string PassWordRex = @"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9!@#$%^&*()._-]{6,12}$";
        public const string NickNameRex = @"^(?=.*[a-zA-Z0-9가-힣!@#$%^&*()._-])[a-zA-Z0-9가-힣!@#$%^&*()._-]{2,12}$";
    }

    public enum ErrorCode : Int16
    {
        None = 0,
        // Db Error 50 ~ 99
        SessionError = 50,
        GameDbError = 51,

        // request format Error 100 ~ 199
        InvalidRequestFormat = 100,
        InvalidEmailFormat = 101,
        InvalidPasswordFormat = 102,
        InvalidClientVersion = 103,
        InvalidNicknameFormat = 104,

        // Regist Error
        EmailAlreadyExist = 201,
        NicknameAlreadyExist = 202,

    }
}


