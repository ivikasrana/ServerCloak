using NetFwTypeLib;
using System;

namespace VikWare.Cloaks
{
    public class FirewallManager
    {
        private static FirewallManager _instance;
        public static FirewallManager Instance { get { return _instance ?? (_instance = new FirewallManager()); } }

        private INetFwMgr firewallManager = ((INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr")));

        public bool FirewallEnabled
        {
            get
            {
                return firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled;
            }
            set
            {
                firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled = value;
            }
        }

        private FirewallManager()
        {
        }

        public void AddAuthorizedApplication(string strName, string processImageFileName, NET_FW_SCOPE_ Scope)
        {
            INetFwAuthorizedApplication app = (INetFwAuthorizedApplication)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication"));
            app.Name = strName;
            app.Scope = Scope;
            app.Enabled = true;
            app.ProcessImageFileName = processImageFileName;
            this.firewallManager.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);
        }

        public void AddPort(string strName, int Port, NET_FW_SCOPE_ Scope, NET_FW_IP_PROTOCOL_ Protocol, string remoteAddresses)
        {
            INetFwOpenPort port = (INetFwOpenPort)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWOpenPort"));
            port.RemoteAddresses = remoteAddresses;
            port.Enabled = true;
            port.Name = strName;
            port.Port = Port;
            port.Protocol = Protocol;
            this.firewallManager.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(port);
        }

        public INetFwOpenPort ReadPort(string name)
        {
            foreach (INetFwOpenPort port in this.firewallManager.LocalPolicy.CurrentProfile.GloballyOpenPorts)
            {
                if (port.Name == name)
                {
                    return port;
                }
            }
            return null;
        }

        public void RemoveAuthorizedApplication(string processFileName)
        {
            this.firewallManager.LocalPolicy.CurrentProfile.AuthorizedApplications.Remove(processFileName);
        }

        public void RemovePort(int Port, NET_FW_IP_PROTOCOL_ Protocol)
        {
            this.firewallManager.LocalPolicy.CurrentProfile.GloballyOpenPorts.Remove(Port, Protocol);
        }
    }
}

