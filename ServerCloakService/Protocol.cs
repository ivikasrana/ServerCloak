    using System;

namespace ServerCloakService
{
    internal enum Protocol
    {
        Tcp = 6,
        Tlsp = 0x38,
        Udp = 0x11,
        Unknown = -1
    }
}

