using System;
using System.IO;
using System.Net;

namespace ServerCloakService
{
    internal class TCPHeader
    {
        private byte byHeaderLength;
        private byte[] byTCPData = new byte[0x80];
        private short sChecksum = 0x22b;
        private uint uiAcknowledgementNumber = 0x22b;
        private uint uiSequenceNumber = 0x22b;
        private ushort usDataOffsetAndFlags = 0x22b;
        private ushort usDestinationPort;
        private ushort usMessageLength;
        private ushort usSourcePort;
        private ushort usUrgentPointer;
        private ushort usWindow = 0x22b;

        internal TCPHeader(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream input = new MemoryStream(byBuffer, 0, nReceived);
                BinaryReader reader = new BinaryReader(input);
                this.usSourcePort = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
                this.usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
                this.uiSequenceNumber = (uint)IPAddress.NetworkToHostOrder(reader.ReadInt32());
                this.uiAcknowledgementNumber = (uint)IPAddress.NetworkToHostOrder(reader.ReadInt32());
                this.usDataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
                this.usWindow = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
                this.sChecksum = IPAddress.NetworkToHostOrder(reader.ReadInt16());
                this.usUrgentPointer = (ushort)IPAddress.NetworkToHostOrder(reader.ReadInt16());
                this.byHeaderLength = (byte)(this.usDataOffsetAndFlags >> 12);
                this.byHeaderLength = (byte)(this.byHeaderLength * 4);
                this.usMessageLength = (ushort)(nReceived - this.byHeaderLength);
                Array.Copy(byBuffer, this.byHeaderLength, this.byTCPData, 0, nReceived - this.byHeaderLength);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        internal string AcknowledgementNumber
        {
            get
            {
                if ((this.usDataOffsetAndFlags & 0x10) != 0)
                {
                    return this.uiAcknowledgementNumber.ToString();
                }
                return "";
            }
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
                return this.byTCPData;
            }
        }

        internal string DestinationPort
        {
            get
            {
                return this.usDestinationPort.ToString();
            }
        }

        internal string Flags
        {
            get
            {
                int num = this.usDataOffsetAndFlags & 0x3f;
                string str = string.Format("0x{0:x2} (", num);
                if ((num & 1) != 0)
                {
                    str = str + "FIN, ";
                }
                if ((num & 2) != 0)
                {
                    str = str + "SYN, ";
                }
                if ((num & 4) != 0)
                {
                    str = str + "RST, ";
                }
                if ((num & 8) != 0)
                {
                    str = str + "PSH, ";
                }
                if ((num & 0x10) != 0)
                {
                    str = str + "ACK, ";
                }
                if ((num & 0x20) != 0)
                {
                    str = str + "URG";
                }
                str = str + ")";
                if (str.Contains("()"))
                {
                    return str.Remove(str.Length - 3);
                }
                if (str.Contains(", )"))
                {
                    str = str.Remove(str.Length - 3, 2);
                }
                return str;
            }
        }

        internal string HeaderLength
        {
            get
            {
                return this.byHeaderLength.ToString();
            }
        }

        internal ushort MessageLength
        {
            get
            {
                return this.usMessageLength;
            }
        }

        internal string SequenceNumber
        {
            get
            {
                return this.uiSequenceNumber.ToString();
            }
        }

        internal string SourcePort
        {
            get
            {
                return this.usSourcePort.ToString();
            }
        }

        internal string UrgentPointer
        {
            get
            {
                if ((this.usDataOffsetAndFlags & 0x20) != 0)
                {
                    return this.usUrgentPointer.ToString();
                }
                return "";
            }
        }

        internal string WindowSize
        {
            get
            {
                return this.usWindow.ToString();
            }
        }
    }
}

