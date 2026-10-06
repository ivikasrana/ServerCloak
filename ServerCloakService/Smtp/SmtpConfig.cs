using System;
using System.ComponentModel;

namespace ServerCloakService
{
    internal class SmtpConfig : BaseConfig
    {
        internal int SmtpPort
        {
            get
            {
                return ServerCloakData.ServerCloak.GetCloakPort(ServerCloakData.CloakEnum.Smtp);
            }
            set
            {
                if (value > 0)
                    ServerCloakData.ServerCloak.SetCloakPort(ServerCloakData.CloakEnum.Smtp, value);
            }
        }
    }
}

