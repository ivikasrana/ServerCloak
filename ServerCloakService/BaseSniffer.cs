using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ServerCloakService
{
    internal abstract class BaseSniffer
    {
        protected bool aborted;
        protected byte[] byteData;
        protected Socket ipSocket;

        internal event EventHandler IpPacketReceived;

        internal event EventHandler IpPacketSent;

        internal void Abort()
        {
            this.aborted = true;
        }

        internal void CloseSocket()
        {
            this.ipSocket.Close();
        }

        internal void Continue()
        {
            this.aborted = false;
        }

        protected void OnPacketReceived(IPHeader ipHeader)
        {
            if (this.IpPacketReceived != null)
                this.IpPacketReceived(ipHeader, EventArgs.Empty);
        }

        protected void OnPacketSent(IPHeader ipHeader)
        {
            if (this.IpPacketSent != null)
                this.IpPacketSent(ipHeader, EventArgs.Empty);
        }

        internal static void LogTrace(Exception ex, string fileName)
        {
            StreamWriter writer = null;
            try
            {
                writer = System.IO.File.AppendText(Path.GetTempPath() + fileName);
                writer.WriteLine(string.Format("{0}\n{1}", ex.Message, ex.StackTrace));
                writer.Flush();
            }
            catch { }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        internal System.Net.IPAddress IPAddress { get; set; }

        internal int? TcpPort { get; set; }

        protected abstract void OnReceive(IAsyncResult ar);

        internal abstract void WatchAddress(object ipAddressToMonitor);
    }
}
