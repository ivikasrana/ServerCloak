using System;
using System.ComponentModel;

namespace ServerCloakService
{
    internal class FtpConfig : BaseConfig
    {
        internal int FtpPort
        {
            get
            {
                return ServerCloakData.ServerCloak.GetCloakPort(ServerCloakData.CloakEnum.Ftp);
            }
            set
            {
                if (value > 0)
                    ServerCloakData.ServerCloak.SetCloakPort(ServerCloakData.CloakEnum.Ftp, value);
            }
        }
    }
}

