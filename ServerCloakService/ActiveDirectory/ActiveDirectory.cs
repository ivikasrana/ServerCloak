using ServerCloakData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Net;
using System.Net.Sockets;

namespace ServerCloakService
{
    public class ActiveDirectoryCredentialValidationSecurityCloak : BaseCloak
    {
        internal const string EVENT_LOG_QUERY_WINDOWS_LOGIN_DENIED = "<QueryList>\r\n                  <Query Id=\"0\" Path=\"Security\">\r\n                    <Select Path=\"Security\">\r\n                        *[System[(EventID=4776) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>";
        private EventLogQuery query;
        private EventLogWatcher watcher;

        internal override CloakEnum CloakName
        {
            get
            {
                return CloakEnum.ActiveDirectory;
            }
        }

        protected override void OnStart()
        {
            this.query = new EventLogQuery("Security", PathType.LogName, string.Format("<QueryList>\r\n                  <Query Id=\"0\" Path=\"Security\">\r\n                    <Select Path=\"Security\">\r\n                        *[System[(EventID=4776) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>", new object[0]));
            this.watcher = new EventLogWatcher(this.query);
            this.watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(this.watcher_EventRecordWritten);
            this.watcher.Enabled = true;
        }

        protected override void OnStop()
        {
            this.watcher.Enabled = false;
            this.watcher = null;
            this.query = null;
        }

        private string[] ResolveIp(string hostname)
        {
            List<string> list = new List<string>();
            foreach (IPAddress address in Dns.GetHostAddresses(hostname))
                if ((address.AddressFamily == AddressFamily.InterNetwork) || (address.AddressFamily == AddressFamily.InterNetworkV6))
                    list.Add(address.ToString());
            return list.ToArray();
        }

        private void watcher_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        {
            try
            {
                string[] propertyQueries = new string[] { "Event/EventData/Data[@Name=\"Workstation\"]" };
                EventLogPropertySelector propertySelector = new EventLogPropertySelector(propertyQueries);
                string hostname = ((EventLogRecord)e.EventRecord).GetPropertyValues(propertySelector)[0].ToString();
                foreach (string str2 in this.ResolveIp(hostname))
                {
                    AttackDetectedEventArgs data = new AttackDetectedEventArgs
                    {
                        CloakName = CloakName,
                        CreateDate = e.EventRecord.TimeCreated.Value,
                        EventId = e.EventRecord.Id,
                        IpAddress = str2
                    };
                    base.OnAttackDetected(this, data);
                }
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry("ServerCloak.ActiveDirectoryCredentialValidationSecurity", exception.Message);
            }
        }
    }
}
