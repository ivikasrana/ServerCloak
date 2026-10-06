using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace ServerCloakService
{
    internal class SmtpLayer
    {
        internal const string SMTP_REPLY_CODE_LOGIN_DENIED = "504";

        internal SmtpLayer(byte[] byBuffer, int nReceived)
        {
            try
            {
                MemoryStream input = new MemoryStream(byBuffer, 0, nReceived);
                char[] chArray = new BinaryReader(input).ReadChars(3);
                StringBuilder builder = new StringBuilder();
                if (chArray.Length == 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        builder.Append(chArray[i]);
                    }
                }
                this.SmtpReplyCode = builder.ToString();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

        internal string SmtpReplyCode { get; set; }
    }
}

