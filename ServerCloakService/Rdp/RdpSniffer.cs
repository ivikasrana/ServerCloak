using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ServerCloakService
{
    internal class RdpSniffer : BaseSniffer
    {
        protected override void OnReceive(IAsyncResult ar)
        {
            if (!this.aborted)
            {
                try
                {
                    int length = this.ipSocket.EndReceive(ar);
                    byte[] destinationArray = new byte[length];
                    Array.Copy(this.byteData, 0, destinationArray, 0, length);
                    IPHeader ipHeader = new IPHeader(destinationArray, length);
                    if (ipHeader.SourceAddress.Equals(this.IPAddress))
                        this.OnPacketSent(ipHeader);
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
            try
            {
                this.byteData = new byte[0x80];
                this.IPAddress = (System.Net.IPAddress)ipAddressToMonitor;
                this.ipSocket = new Socket(this.IPAddress.AddressFamily, SocketType.Raw, ProtocolType.IP);
                this.ipSocket.ExclusiveAddressUse = false;
                this.ipSocket.Bind(new IPEndPoint(this.IPAddress, this.TcpPort.HasValue ? this.TcpPort.Value : 0xd3d));
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
            catch (Exception exception)
            {
                LogTrace(exception);
            }
        }

        internal static void LogTrace(Exception ex)
        {
            //LogTrace(ex, @"\VikWare.RdpCloak.ErrorLog.txt");
        }
    }
}

