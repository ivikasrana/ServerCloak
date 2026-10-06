using System;
using System.ComponentModel;

namespace ServerCloakService
{
    internal class RdpConfig : BaseConfig
    {
        internal int RdpPort
        {
            get
            {
                return ServerCloakData.ServerCloak.GetCloakPort(ServerCloakData.CloakEnum.Rdp);
            }
            set
            {
                if (value > 0)
                    ServerCloakData.ServerCloak.SetCloakPort(ServerCloakData.CloakEnum.Rdp, value);
            }
        }
    }
}

