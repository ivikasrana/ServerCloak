using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ServerCloakService
{
    internal class RdpLayer
    {
        internal const byte CONTENT_TYPE_ENCRYPTED_ALERT = 0x15;
        internal const byte CONTENT_TYPE_HANDSHAKE = 0x16;
        internal const byte CONTENT_TYPE_SSL_APPLICATION_DATA = 0x17;
        internal TlsProtocolHeader TlsHeader = new TlsProtocolHeader();

        internal RdpLayer(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream input = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader reader = new BinaryReader(input);
                this.TlsHeader.ContentType = reader.ReadByte();
                this.TlsHeader.MajorVersion = reader.ReadByte();
                this.TlsHeader.MinorVersion = reader.ReadByte();
                this.TlsHeader.Length = reader.ReadUInt16();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct TlsProtocolHeader
        {
            internal byte ContentType;
            internal byte MajorVersion;
            internal byte MinorVersion;
            internal ushort Length;
        }
    }
}

