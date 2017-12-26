using System;

namespace Tierion.Client
{
    public class AuthRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class AuthRefresh
    {
        public string refreshToken { get; set; }
    }
}
