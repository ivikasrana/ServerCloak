using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ServerCloakService
{
    internal class SmtpSniffer : BaseSniffer
    {
        protected override void OnReceive(IAsyncResult ar)
        {
            if (!this.aborted)
            {
                try
                {
                    int nReceived = this.ipSocket.EndReceive(ar);
                    IPHeader ipHeader = new IPHeader(this.byteData, nReceived);
                    if (ipHeader.SourceAddress.Equals(this.IPAddress))
                    {
                        this.OnPacketSent(ipHeader);
                    }
                    if (ipHeader.DestinationAddress.Equals(this.IPAddress))
                    {
                        this.OnPacketReceived(ipHeader);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    this.byteData = new byte[0x80];
                    this.ipSocket.BeginReceive(this.byteData, 0, this.byteData.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
                }
            }
        }

        internal override void WatchAddress(object ipAddressToMonitor)
        {
            this.byteData = new byte[0x80];
            this.IPAddress = (System.Net.IPAddress)ipAddressToMonitor;
            this.ipSocket = new Socket(this.IPAddress.AddressFamily, SocketType.Raw, ProtocolType.IP);
            this.ipSocket.ExclusiveAddressUse = false;
            this.ipSocket.Bind(new IPEndPoint(this.IPAddress, this.TcpPort.HasValue ? this.TcpPort.Value : 0x19));
            this.ipSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, true);
            byte[] buffer3 = new byte[4];
            buffer3[0] = 3;
            byte[] optionInValue = buffer3;
            byte[] buffer4 = new byte[4];
            buffer4[0] = 1;
            byte[] optionOutValue = buffer4;
            this.ipSocket.IOControl(IOControlCode.ReceiveAll, optionInValue, optionOutValue);
            this.ipSocket.BeginReceive(this.byteData, 0, this.byteData.Length, SocketFlags.None, new AsyncCallback(this.OnReceive), null);
        }

        internal static void LogTrace(Exception ex)
        {
            //LogTrace(ex, @"\VikWare.SmtpCloak.ErrorLog.txt");
        }
    }
}

