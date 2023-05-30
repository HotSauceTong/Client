
namespace General
{
    public static class NetworkValues
    {
        public const string ClientVersion = "1.0.0";
    }

    public static class RexValues
    {
        public const string EmailIdRex =
            @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";
        public const string PassWordRex = @"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9!@#$%^&*()._-]{6,12}$";
        public const string NickNameRex = @"^(?=.*[a-z0-9가-힣!@#$%^&*()._-])[a-z0-9가-힣!@#$%^&*()._-]{2,12}$";
    }
}


