using ServerCloakData;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ServerCloakService
{
    public class SqlCloak : BaseCloak
    {
        internal const string EVENT_LOG_QUERY_SQL_SERVER_LOGIN_DENIED = "<QueryList>\r\n                  <Query Id=\"18456\" Path=\"Application\">\r\n                    <Select Path=\"Application\">\r\n                        *[System[(EventID=18456) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 864000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>";
        private EventLogQuery query;
        private EventLogWatcher watcher;

        internal override CloakEnum CloakName
        {
            get
            {
                return CloakEnum.Sql;
            }
        }

        protected override void OnStart()
        {
            query = new EventLogQuery("Application", PathType.LogName, string.Format("<QueryList>\r\n                  <Query Id=\"18456\" Path=\"Application\">\r\n                    <Select Path=\"Application\">\r\n                        *[System[(EventID=18456) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 864000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>", new object[0]));
            watcher = new EventLogWatcher(query);
            watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(watcher_EventRecordWritten);
            watcher.Enabled = true;
        }

        protected override void OnStop()
        {
            watcher.Enabled = false;
            watcher = null;
            query = null;
        }

        private void watcher_EventRecordWritten(object sender, EventRecordWrittenEventArgs e)
        {
            try
            {
                AttackDetectedEventArgs data;
                if (base.EventRecordWrittenForWatcher(e, out data))
                    base.OnAttackDetected(this, data);

                //foreach (EventProperty property in e.EventRecord.Properties)
                //{
                //    if (Regex.IsMatch(property.Value.ToString(), "(?:[0-9]{1,3}.){3}[0-9]{1,3}"))
                //    {
                //        IPAddress address;
                //        Match match = Regex.Match(property.Value.ToString(), "(?:[0-9]{1,3}.){3}[0-9]{1,3}");
                //        NotificationEventArgs data = new NotificationEventArgs
                //        {
                //            CreateDate = e.EventRecord.TimeCreated.Value,
                //            EventId = e.EventRecord.Id,
                //            IpAddress = match.Value
                //        };
                //        if (IPAddress.TryParse(match.Value, out address) && ((address.AddressFamily == AddressFamily.InterNetwork) || (address.AddressFamily == AddressFamily.InterNetworkV6)))
                //        {
                //            base.OnAttackDetected(this, data);
                //        }
                //    }
                //}
            }
            catch (Exception exception)
            {
                WriteEntry("ServerCloak.Sql", exception.Message);
            }
        }
    }
}