using System;
using System.IO;
using System.Net;

namespace ServerCloakService
{
    internal class IPHeader
    {
        private byte byDifferentiatedServices;
        private byte byHeaderLength;
        private byte[] byIPData = new byte[0x80];
        private byte byProtocol;
        private byte byTTL;
        private byte byVersionAndHeaderLength;
        private short sChecksum;
        private uint uiDestinationIPAddress;
        private uint uiSourceIPAddress;
        private ushort usFlagsAndOffset;
        private ushort usIdentification;
        private ushort usTotalLength;

        internal IPHeader(byte[] byBuffer, int nReceived)
        {
            MemoryStream input = new MemoryStream(byBuffer, 0, nReceived);
            BinaryReader reader = new BinaryReader(input);
            this.byVersionAndHeaderLength = reader.ReadByte();
            this.byDifferentiatedServices = reader.ReadByte();
            this.usTotalLength = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
            this.usIdentification = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
            this.usFlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
            this.byTTL = reader.ReadByte();
            this.byProtocol = reader.ReadByte();
            this.sChecksum = IPAddress.NetworkToHostOrder(reader.ReadInt16());
            this.uiSourceIPAddress = (uint)reader.ReadInt32();
            this.uiDestinationIPAddress = (uint)reader.ReadInt32();
            this.byHeaderLength = this.byVersionAndHeaderLength;
            this.byHeaderLength = (byte)(this.byHeaderLength << 4);
            this.byHeaderLength = (byte)(this.byHeaderLength >> 4);
            this.byHeaderLength = (byte)(this.byHeaderLength * 4);
            Array.Copy(byBuffer, this.byHeaderLength, this.byIPData, 0, this.usTotalLength - this.byHeaderLength);
        }

        internal string Checksum
        {
            get
            {
                return string.Format("0x{0:x2}", this.sChecksum);
            }
        }

        internal byte[] Data
        {
            get
            {
                return this.byIPData;
            }
        }

        internal IPAddress DestinationAddress
        {
            get
            {
                return new IPAddress((long)this.uiDestinationIPAddress);
            }
        }

        internal string DifferentiatedServices
        {
            get
            {
                return string.Format("0x{0:x2} ({1})", this.byDifferentiatedServices, this.byDifferentiatedServices);
            }
        }

        internal string Flags
        {
            get
            {
                int num = this.usFlagsAndOffset >> 13;
                switch (num)
                {
                    case 2:
                        return "Don't fragment";

                    case 1:
                        return "More fragments to come";
                }
                return num.ToString();
            }
        }

        internal string FragmentationOffset
        {
            get
            {
                int num = this.usFlagsAndOffset << 3;
                num = num >> 3;
                return num.ToString();
            }
        }

        internal string HeaderLength
        {
            get
            {
                return this.byHeaderLength.ToString();
            }
        }

        internal string Identification
        {
            get
            {
                return this.usIdentification.ToString();
            }
        }

        internal ushort MessageLength
        {
            get
            {
                return (ushort)(this.usTotalLength - this.byHeaderLength);
            }
        }

        internal Protocol ProtocolType
        {
            get
            {
                switch (this.byProtocol)
                {
                    case 6:
                        return Protocol.Tcp;

                    case 0x11:
                        return Protocol.Udp;

                    case 0x38:
                        return Protocol.Tlsp;
                }
                return Protocol.Unknown;
            }
        }

        internal IPAddress SourceAddress
        {
            get
            {
                return new IPAddress((long)this.uiSourceIPAddress);
            }
        }

        internal string TotalLength
        {
            get
            {
                return this.usTotalLength.ToString();
            }
        }

        internal string TTL
        {
            get
            {
                return this.byTTL.ToString();
            }
        }

        internal string Version
        {
            get
            {
                if ((this.byVersionAndHeaderLength >> 4) == 4)
                {
                    return "IP v4";
                }
                if ((this.byVersionAndHeaderLength >> 4) == 6)
                {
                    return "IP v6";
                }
                return "Unknown";
            }
        }
    }
}

