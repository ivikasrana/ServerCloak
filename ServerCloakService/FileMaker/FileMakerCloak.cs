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
    public class FileMakerCloak : BaseCloak
    {
        internal const string EVENT_LOG_QUERY_FILEMAKER_LOGIN_DENIED = "<QueryList>\r\n                  <Query Id=\"661\" Path=\"Application\">\r\n                    <Select Path=\"Application\">\r\n                        *[System[(EventID=661) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>";
        //internal const string EVENT_LOG_QUERY_FILEMAKER_LOGIN_DENIED = "<QueryList>\r\n                  <Query Id=\"4757\" Path=\"Application\">\r\n                    <Select Path=\"Application\">\r\n                        *[System[(EventID=4757) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>";
        private EventLogQuery query;
        private EventLogWatcher watcher;

        internal override CloakEnum CloakName
        {
            get
            {
                return CloakEnum.FileMaker;
            }
        }

        protected override void OnStart()
        {
            this.query = new EventLogQuery("Application", PathType.LogName, string.Format("<QueryList>\r\n                  <Query Id=\"661\" Path=\"Application\">\r\n                    <Select Path=\"Application\">\r\n                        *[System[(EventID=661) and\r\n                        TimeCreated[timediff(@SystemTime) &lt;= 86400000]]]\r\n                    </Select>\r\n                  </Query>\r\n                </QueryList>", new object[0]));
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
                //        IPAddress.TryParse(data.IpAddress, out address);
                //        if ((address != null) && (address.AddressFamily == AddressFamily.InterNetwork))
                //        {
                //            base.OnAttackDetected(this, data);
                //        }
                //    }
                //}
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry("ServerCloak.FileMakerCloak", exception.Message);
            }
        }
    }
}
