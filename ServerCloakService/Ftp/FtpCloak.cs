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
    public class FtpCloak : BaseCloak
    {
        private List<FtpSniffer> sniffers = new List<FtpSniffer>();
        private Thread td;
        private ThreadStart ts;

        internal event EventHandler Trace;

        internal override CloakEnum CloakName
        {
            get
            {
                return CloakEnum.Ftp;
            }
        }

        public FtpCloak()
        {
            base.Configuration.CloakSettings = new FtpConfig();
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
            foreach (FtpSniffer sniffer in this.sniffers)
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
            {
                this.Trace(tlsPackage, EventArgs.Empty);
            }
        }

        private void RunWatcher()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            if (hostEntry.AddressList.Length > 0)
            {
                foreach (IPAddress address in hostEntry.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        new ParameterizedThreadStart(this.WatchAddress)(address);
                    }
                }
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
                    if (int.TryParse(header2.SourcePort, out num) && (num == ((FtpConfig)base.Configuration.CloakSettings).FtpPort))
                    {
                        if (this.Tracing)
                        {
                            this.OnTrace((IPHeader)sender);
                        }
                        if (header2.Data.Length > 0)
                        {
                            FtpLayer ftp = new FtpLayer(header2.Data, header2.Data.Length);
                            if (ftp.FtpReplyCode == "530")
                            {
                                this.UnsuccessfulLogin(header.DestinationAddress.ToString());
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    FtpSniffer.LogTrace(exception);
                }
            }
        }

        private void UnsuccessfulLogin(string ipAddress)
        {
            AttackDetectedEventArgs data = new AttackDetectedEventArgs
            {
                CreateDate = DateTime.Now,
                EventId = 0x2398,
                EventMessage = "FTP authentication failure",
                IpAddress = ipAddress,
                CloakName = CloakName
            };
            base.OnAttackDetected(this, data);
        }

        private void WatchAddress(object ipAddress)
        {
            FtpSniffer item = new FtpSniffer();
            item.IpPacketSent += new EventHandler(this.s_IpPacketSent);
            item.TcpPort = new int?(((FtpConfig)base.Configuration.CloakSettings).FtpPort);
            WriteEntry("ServerCloak.Ftp", string.Format("Ftp Server Security Cloak is listening on port {0}", item.TcpPort));
            item.WatchAddress((IPAddress)ipAddress);
            this.sniffers.Add(item);
        }

        internal bool Tracing { get; set; }

    }
}

