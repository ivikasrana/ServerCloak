using ServerCloakData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ServerCloakService
{
    public class RdpCloak : BaseCloak
    {
        private List<RdpSniffer> sniffers = new List<RdpSniffer>();
        private Thread td;
        private ThreadStart ts;

        internal event EventHandler Trace;

        internal override CloakEnum CloakName
        {
            get
            {
                return CloakEnum.Rdp;
            }
        }

        public RdpCloak()
        {
            base.Configuration.CloakSettings = new RdpConfig();
            base.Configuration.ConfigurationSettingsTypeName = base.Configuration.CloakSettings.GetType().FullName;
        }

        protected override void OnStart()
        {
            this.ts = new ThreadStart(this.RunWatcher);
            this.td = new Thread(this.ts);
            this.td.Start();
            base.OnStart();
        }

        protected override void OnStop()
        {
            foreach (RdpSniffer sniffer in this.sniffers)
            {
                sniffer.Abort();
                sniffer.CloseSocket();
            }
            this.sniffers.Clear();
            base.OnStop();
        }

        private void OnTrace(IPHeader tlsPackage)
        {
            if (this.Trace != null)
                this.Trace(tlsPackage, EventArgs.Empty);
        }

        private void RunWatcher()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            if (hostEntry.AddressList.Length > 0)
                foreach (IPAddress address in hostEntry.AddressList)
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ParameterizedThreadStart start = new ParameterizedThreadStart(this.WatchAddress);
                        start(address);
                    }
        }

        private void s_IpPacketSent(object sender, EventArgs e)
        {
            IPHeader header = (IPHeader)sender;
            if (header.ProtocolType == Protocol.Tcp)
            {
                try
                {
                    int num;
                    TCPHeader header2 = new TCPHeader(header.Data, header.MessageLength);
                    if (int.TryParse(header2.SourcePort, out num) && (num == ((RdpConfig)base.Configuration.CloakSettings).RdpPort))
                    {
                        if (this.Tracing)
                        {
                            this.OnTrace((IPHeader)sender);
                        }
                        if (header2.Data.Length > 0)
                        {
                            RdpLayer ssl = new RdpLayer(header2.Data, header2.Data.Length);
                            if ((((ssl.TlsHeader.MinorVersion >= 1) && (ssl.TlsHeader.MinorVersion < 10)) && ((ssl.TlsHeader.MajorVersion >= 1) && (ssl.TlsHeader.MajorVersion < 10))) && (ssl.TlsHeader.ContentType == 0x15))
                            {
                                this.UnsuccessfulLogin(header.DestinationAddress.ToString());
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    RdpSniffer.LogTrace(exception);
                }
            }
        }

        private void UnsuccessfulLogin(string ipAddress)
        {
            AttackDetectedEventArgs data = new AttackDetectedEventArgs
            {
                CreateDate = DateTime.Now,
                EventId = 0x2398,
                EventMessage = "Remote desktop connection TLS/SSL authentication failure",
                IpAddress = ipAddress,
                CloakName = CloakName
            };
            base.OnAttackDetected(this, data);
        }

        private void WatchAddress(object ipAddress)
        {
            RdpSniffer item = new RdpSniffer();
            item.IpPacketSent += new EventHandler(this.s_IpPacketSent);
            item.TcpPort = new int?(((RdpConfig)base.Configuration.CloakSettings).RdpPort);
            WriteEntry("ServerCloak.Rdp", string.Format("Remote Desktop Security Cloak is listening on port {0}", item.TcpPort));
            item.WatchAddress((IPAddress)ipAddress);
            this.sniffers.Add(item);
        }

        internal bool Tracing { get; set; }
    }
}

